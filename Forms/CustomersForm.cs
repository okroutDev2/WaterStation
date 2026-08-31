using Microsoft.Data.SqlClient;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;
using WaterStation.Data;

namespace WaterStation.Forms;

/// <summary>
/// Customer management screen (read + create only).
/// Listing/search is read-only against Core.vw_CustomerMeters; creating a customer
/// goes through CustomerService (parameterized INSERT into Core.Customers).
/// Update/Delete are intentionally absent: no safe backend procedure exists for them.
/// </summary>
public sealed partial class CustomersForm : Form
{
    private readonly CustomerService _customerService;
    private readonly BillingService _billingService;

    private CancellationTokenSource? _cts;
    private bool _isSearching;
    private UiBusy? _busy;
    private int _colStatus = -1;
    private EmptyPanel? _emptyPanel;

    /// <summary>Raised (on the UI thread) when the user asks to leave this screen.</summary>
    public event Action? ExitRequested;

    public CustomersForm(CustomerService customerService) : this(customerService, new BillingService(new Database()))
    {
    }

    public CustomersForm(CustomerService customerService, BillingService billingService)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _billingService = billingService ?? throw new ArgumentNullException(nameof(billingService));
        InitializeComponent();
        ConfigureGrid();
        RegisterEventHandlers();
        ApplyUiTheme();
        tslblStatus.Text = "جاهز";
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.ApplyGridDefaults(dgvCustomers);
        UiTheme.StyleTertiaryButton(btnSearch);
        UiTheme.StyleTertiaryButton(btnClear);
        UiTheme.StyleTertiaryButton(btnRefresh);
        UiTheme.StylePrimaryButton(btnAdd);
        UiTheme.StylePrimaryButton(btnAddMeter);
        UiTheme.StyleSecondaryButton(btnDetails);
        UiTheme.StyleSecondaryButton(btnViewMeters);
        UiTheme.StylePrimaryButton(btnCollect);

        grpSummary.ForeColor = UiTheme.PrimaryDark;
        lblDetailNameValue.ForeColor = UiTheme.TextPrimary;
        lblDetailNumberValue.ForeColor = UiTheme.TextPrimary;
        lblDetailPhoneValue.ForeColor = UiTheme.TextSecondary;

        _emptyPanel = UiTheme.CreateEmptyPanel(
            UiTheme.EmptyCustomers,
            actionText: "إضافة عميل",
            action: () => _ = OpenCreateCustomerDialogAsync());
        _emptyPanel.Visible = false;
        Controls.Add(_emptyPanel);
        Controls.SetChildIndex(_emptyPanel, 0);
        _emptyPanel.BringToFront();

        UiTheme.StyleScreenHeaderLabel(lblScreenTitle);
        lblScreenTitle.Text = "إدارة العملاء";
        lblScreenTitle.AccessibleName = "إدارة العملاء";

        txtCustomerNumber.AccessibleName = "بحث برقم العميل";
        txtName.AccessibleName = "بحث باسم العميل";
        btnSearch.AccessibleName = "بحث في العملاء";
        btnClear.AccessibleName = "مسح";
        btnRefresh.AccessibleName = "تحديث";
        btnAdd.AccessibleName = "إضافة عميل جديد";
        btnDetails.AccessibleName = "عرض تفاصيل العميل المحدد";
        btnViewMeters.AccessibleName = "عرض عدادات العميل المحدد";
        btnAddMeter.AccessibleName = "تسجيل عداد جديد للعميل المحدد";
        btnCollect.AccessibleName = "فتح شاشة التحصيل للعميل المحدد";
        btnExit.AccessibleName = "خروج";
        dgvCustomers.AccessibleName = "جدول العملاء";
    }

    private void ConfigureGrid()
    {
        dgvCustomers.Columns.Clear();
        dgvCustomers.Columns.Add(Column(nameof(Customer.CustomerId), "المعرف", 45, "0"));
        dgvCustomers.Columns.Add(Column(nameof(Customer.CustomerNumber), "رقم العميل", 90));
        dgvCustomers.Columns.Add(Column(nameof(Customer.FullName), "اسم العميل", 170));
        dgvCustomers.Columns.Add(Column(nameof(Customer.Phone), "الهاتف", 100));
        dgvCustomers.Columns.Add(Column(nameof(Customer.Address), "العنوان", 150));
        dgvCustomers.Columns.Add(Column(nameof(Customer.FamilyMembersCount), "عدد الأفراد", 70, "0"));
        dgvCustomers.Columns.Add(Column(nameof(Customer.Status), "الحالة", 60));
        dgvCustomers.Columns.Add(Column(nameof(Customer.Notes), "ملاحظات", 140));

        _colStatus = dgvCustomers.Columns[nameof(Customer.Status)].Index;
        dgvCustomers.CellFormatting += ApplyStatusFormatting;
    }

    private void RegisterEventHandlers()
    {
        btnSearch.Click += async (s, e) => await PerformSearchAsync();
        btnRefresh.Click += async (s, e) => await PerformSearchAsync();
        btnClear.Click += (s, e) =>
        {
            txtCustomerNumber.Clear();
            txtName.Clear();
            _ = PerformSearchAsync();
        };
        btnAdd.Click += async (s, e) => await OpenCreateCustomerDialogAsync();
        btnExit.Click += (s, e) => ExitRequested?.Invoke();

        btnDetails.Click += (s, e) => OpenDetailsForSelected();
        btnViewMeters.Click += (s, e) => OpenMetersForSelected();
        btnAddMeter.Click += (s, e) => OpenAddMeterForSelected();
        btnCollect.Click += async (s, e) => await OpenCollectionForSelectedAsync();

        txtCustomerNumber.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await PerformSearchAsync();
            }
        };
        txtName.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await PerformSearchAsync();
            }
        };

        KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5 && !_isSearching)
            {
                e.Handled = true;
                await PerformSearchAsync();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                ExitRequested?.Invoke();
            }
        };

        dgvCustomers.SelectionChanged += (s, e) =>
        {
            UpdateActionButtons();
            UpdateSelectedSummary();
        };

        Shown += (s, e) => _ = PerformSearchAsync();
    }

    private static DataGridViewTextBoxColumn Column(string property, string header, int weight, string? format = null)
    {
        return new DataGridViewTextBoxColumn
        {
            Name = property,
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = weight,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = format is null or "0" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleRight,
                Format = format
            }
        };
    }

    private async Task PerformSearchAsync()
    {
        if (_isSearching)
        {
            return;
        }

        _busy ??= new UiBusy(
            new Control[] { btnSearch, btnClear, btnRefresh, btnAdd, btnExit, txtCustomerNumber, txtName },
            pbLoading,
            message => tslblStatus.Text = message,
            "جاهز");
        _busy.Begin("جاري تحميل العملاء من قاعدة البيانات...");

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        _isSearching = true;
        try
        {
            var customers = await _customerService.SearchCustomersAsync(
                txtCustomerNumber.Text.Trim(),
                txtName.Text.Trim(),
                token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            if (customers.Count > 0)
            {
                dgvCustomers.DataSource = customers.ToList();
                ShowEmptyState(false);
            }
            else
            {
                dgvCustomers.DataSource = null;
                ShowEmptyState(true);
            }

            tslblCount.Text = customers.Count == 1 ? "عميل واحد" : $"{customers.Count} عملاء";
            _busy.End(customers.Count > 0 ? "تم تحميل العملاء." : UiTheme.StatusEmpty);
        }
        catch (OperationCanceledException)
        {
        }
        catch (SqlException sqlEx)
        {
            dgvCustomers.DataSource = null;
            tslblCount.Text = string.Empty;
            ShowEmptyState(false);
            _busy.End("تعذر تحميل العملاء.");
            UiMessages.Warning($"حدث خطأ من قاعدة البيانات أثناء تحميل العملاء:\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
        }
        catch (Exception ex)
        {
            dgvCustomers.DataSource = null;
            tslblCount.Text = string.Empty;
            ShowEmptyState(false);
            _busy.End("تعذر تحميل العملاء.");
            UiMessages.Error($"حدث خطأ غير متوقع أثناء تحميل العملاء:\n{ex.Message}");
        }
        finally
        {
            _isSearching = false;
        }
    }

    private async Task OpenCreateCustomerDialogAsync()
    {
        if (_isSearching)
        {
            return;
        }

        using var dialog = new CustomerEditForm(_customerService);
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            tslblStatus.Text = "تمت إضافة العميل، جاري تحديث القائمة...";
            await PerformSearchAsync();
        }
    }

    private Customer? SelectedCustomer() =>
        dgvCustomers.CurrentRow?.DataBoundItem as Customer;

    private void UpdateActionButtons()
    {
        var c = SelectedCustomer();
        var has = c is not null;
        btnDetails.Enabled = has;
        btnViewMeters.Enabled = has;
        btnAddMeter.Enabled = has;
        btnCollect.Enabled = has;
    }

    private async void UpdateSelectedSummary()
    {
        var c = SelectedCustomer();
        if (c is null)
        {
            ClearSelectedSummary();
            return;
        }

        lblDetailNameValue.Text = string.IsNullOrWhiteSpace(c.FullName) ? "—" : c.FullName;
        lblDetailNumberValue.Text = string.IsNullOrWhiteSpace(c.CustomerNumber) ? "—" : $"رقم: {c.CustomerNumber}";
        lblDetailPhoneValue.Text = string.IsNullOrWhiteSpace(c.Phone) ? "" : $"هاتف: {c.Phone}";
        lblDetailStatusValue.Text = c.Status == 1 ? "نشط" : "غير نشط";
        lblDetailStatusValue.ForeColor = c.Status == 1 ? UiTheme.Success : UiTheme.Danger;
        lblDetailBalanceValue.Text = "0.00";
        lblDetailBalanceValue.ForeColor = UiTheme.TextPrimary;

        try
        {
            var screen = await _billingService.GetCustomerCollectionScreenAsync(customerId: c.CustomerId, cancellationToken: CancellationToken.None);
            if (SelectedCustomer()?.CustomerId != c.CustomerId)
            {
                return;
            }

            var balance = screen.OpenInvoices.Sum(i => i.BalanceAmount);
            lblDetailBalanceValue.Text = UiText.Currency(balance);
            lblDetailBalanceValue.ForeColor = balance > 0 ? UiTheme.Danger : UiTheme.Success;
            lblDetailBalanceValue.AccessibleDescription = balance > 0 ? $"الرصيد المستحق: {lblDetailBalanceValue.Text}" : "لا يوجد رصيد مستحق";
        }
        catch (Exception)
        {
            // Balance is best-effort on this screen; the grid and actions remain fully usable.
        }
    }

    private void ClearSelectedSummary()
    {
        lblDetailNameValue.Text = "—";
        lblDetailNumberValue.Text = "—";
        lblDetailPhoneValue.Text = "—";
        lblDetailStatusValue.Text = "—";
        lblDetailStatusValue.ForeColor = UiTheme.TextSecondary;
        lblDetailBalanceValue.Text = "0.00";
        lblDetailBalanceValue.ForeColor = UiTheme.TextPrimary;
        lblDetailBalanceValue.AccessibleDescription = null;
    }

    private void ShowEmptyState(bool show)
    {
        if (_emptyPanel is null)
        {
            return;
        }

        if (show)
        {
            var hasFilter = !string.IsNullOrWhiteSpace(txtCustomerNumber.Text) || !string.IsNullOrWhiteSpace(txtName.Text);
            _emptyPanel.SetMessage(hasFilter ? UiTheme.EmptyCustomersSearch : UiTheme.EmptyCustomers);
            _emptyPanel.Visible = true;
            _emptyPanel.BringToFront();
        }
        else
        {
            _emptyPanel.Visible = false;
        }
    }

    private async Task OpenCollectionForSelectedAsync()
    {
        var c = SelectedCustomer();
        if (c is null)
        {
            UiMessages.Warning("يرجى اختيار عميل من القائمة أولاً.", "تنبيه");
            return;
        }

        using var collection = new FieldCollectionForm(_billingService);
        _busy?.Begin("جاري تجهيز شاشة التحصيل...");
        try
        {
            await collection.LoadByCustomerIdAsync(c.CustomerId);
        }
        finally
        {
            _busy?.End();
        }
        collection.ShowDialog(this);
        _ = PerformSearchAsync();
    }

    private void OpenDetailsForSelected()
    {
        var c = SelectedCustomer();
        if (c is null)
        {
            UiMessages.Warning("يرجى اختيار عميل من القائمة أولاً.", "تنبيه");
            return;
        }

        using var details = new CustomerDetailsForm(c, new MeterService(new Database()), new BillingService(new Database()));
        details.ShowDialog(this);
        _ = PerformSearchAsync();
    }

    private void OpenMetersForSelected()
    {
        var c = SelectedCustomer();
        if (c is null)
        {
            UiMessages.Warning("يرجى اختيار عميل من القائمة أولاً.", "تنبيه");
            return;
        }

        var metersForm = new MetersManagementForm(new MeterService(new Database()));
        metersForm.ExitRequested += () => metersForm.Close();
        metersForm.ShowDialog(this);
        _ = PerformSearchAsync();
    }

    private void OpenAddMeterForSelected()
    {
        var c = SelectedCustomer();
        if (c is null)
        {
            UiMessages.Warning("يرجى اختيار عميل من القائمة أولاً.", "تنبيه");
            return;
        }

        using var dlg = new AddMeterForm(c, new MeterService(new Database()));
        dlg.ShowDialog(this);
        _ = PerformSearchAsync();
    }

    private void ApplyStatusFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex != _colStatus || e.Value is not byte status || e.CellStyle is null)
        {
            return;
        }

        e.Value = status == 1 ? "نشط" : status == 0 ? "غير نشط" : "غير معروف";
        e.CellStyle.ForeColor = status == 1 ? UiTheme.Success : UiTheme.Danger;
        e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
        e.FormattingApplied = true;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _busy?.Dispose();
        base.OnFormClosing(e);
    }
}