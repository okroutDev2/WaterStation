using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

public partial class CustomerDetailsForm : Form
{
    private readonly Customer _customer;
    private readonly MeterService _meterService;
    private readonly BillingService _billingService;
    private CancellationTokenSource? _cts;
    private DataGridViewColumn? _colMeterDirection;
    private DataGridViewColumn? _colMeterStatus;
    private DataGridViewColumn? _colMeterInstallDate;
    private DataGridViewColumn? _colMeterInstallReading;
    private DataGridViewColumn? _colInvoiceBalance;
    private DataGridViewColumn? _colInvoiceDate;

    public CustomerDetailsForm(Customer customer, MeterService meterService, BillingService billingService)
    {
        _customer = customer ?? throw new ArgumentNullException(nameof(customer));
        _meterService = meterService ?? throw new ArgumentNullException(nameof(meterService));
        _billingService = billingService ?? throw new ArgumentNullException(nameof(billingService));

        InitializeComponent();
        ApplyUiTheme();
        ConfigureGrids();
        PopulateCustomerInfo();
        RegisterEventHandlers();

        _cts = new CancellationTokenSource();
    }

    private void ApplyUiTheme()
    {
        lblHeading.ForeColor = UiTheme.TextPrimary;
        lblMetersTitle.ForeColor = UiTheme.TextPrimary;
        lblInvoicesTitle.ForeColor = UiTheme.TextPrimary;
        lblFooter.ForeColor = UiTheme.TextSecondary;
        pnlButtons.BackColor = UiTheme.SurfaceAlt;
        UiTheme.ApplyGridDefaults(dgvMeters);
        UiTheme.ApplyGridDefaults(dgvInvoices);
        UiTheme.StylePrimaryButton(btnAddMeter);
        UiTheme.StyleSecondaryButton(btnOpenCollection);
        UiTheme.StyleSecondaryButton(btnBack);
    }

    private void ConfigureGrids()
    {
        dgvMeters.AutoGenerateColumns = false;
        dgvMeters.AccessibleName = "جدول عدادات العميل";
        dgvMeters.Columns.Clear();
        dgvMeters.Columns.Add(FillColumn(nameof(Meter.MeterNumber), "رقم العداد"));
        dgvMeters.Columns.Add(FillColumn(nameof(Meter.BranchName), "الفرع"));
        dgvMeters.Columns.Add(FillColumn(nameof(Meter.AreaName), "المنطقة"));
        dgvMeters.Columns.Add(FillColumn(nameof(Meter.MeterTypeName), "نوع العداد"));

        var colMeterDirection = ComputedColumn("الاتجاه", 60);
        dgvMeters.Columns.Add(colMeterDirection);
        _colMeterDirection = colMeterDirection;

        var colInstallDate = FillColumn(nameof(Meter.InstallationDate), "تاريخ التركيب");
        dgvMeters.Columns.Add(colInstallDate);
        _colMeterInstallDate = colInstallDate;

        var colInstallReading = FillColumn(nameof(Meter.InstallationReading), "قراءة التركيب");
        dgvMeters.Columns.Add(colInstallReading);
        _colMeterInstallReading = colInstallReading;

        var colMeterStatus = ComputedColumn("الحالة", 70);
        dgvMeters.Columns.Add(colMeterStatus);
        _colMeterStatus = colMeterStatus;

        dgvMeters.CellFormatting += OnMetersCellFormatting;

        dgvInvoices.AutoGenerateColumns = false;
        dgvInvoices.AccessibleName = "جدول الفواتير المفتوحة";
        dgvInvoices.Columns.Clear();
        dgvInvoices.Columns.Add(FillColumn(nameof(Invoice.InvoiceNumber), "رقم الفاتورة", 160));

        var colInvoiceDate = FillColumn(nameof(Invoice.InvoiceDate), "التاريخ", 110);
        dgvInvoices.Columns.Add(colInvoiceDate);
        _colInvoiceDate = colInvoiceDate;

        dgvInvoices.Columns.Add(FillColumn(nameof(Invoice.BillingYear), "السنة", 60, "0"));
        dgvInvoices.Columns.Add(FillColumn(nameof(Invoice.BillingMonth), "الشهر", 60, "0"));
        dgvInvoices.Columns.Add(FillColumn(nameof(Invoice.TotalAmount), "الإجمالي", 110, "N2"));
        dgvInvoices.Columns.Add(FillColumn(nameof(Invoice.PaidAmount), "المدفوع", 110, "N2"));

        var colInvoiceBalance = FillColumn(nameof(Invoice.BalanceAmount), "المتبقي", 110, "N2");
        dgvInvoices.Columns.Add(colInvoiceBalance);
        _colInvoiceBalance = colInvoiceBalance;

        dgvInvoices.Columns.Add(FillColumn(nameof(Invoice.StatusName), "الحالة", 130));
        dgvInvoices.CellFormatting += OnInvoicesCellFormatting;
    }

    private static DataGridViewTextBoxColumn FillColumn(string dataPropertyName, string header, int fillWeight = 100, string? format = null)
    {
        var column = new DataGridViewTextBoxColumn
        {
            DataPropertyName = dataPropertyName,
            HeaderText = header,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = fillWeight,
            SortMode = DataGridViewColumnSortMode.Automatic
        };

        if (!string.IsNullOrWhiteSpace(format))
        {
            column.DefaultCellStyle.Format = format;
        }

        return column;
    }

    private static DataGridViewTextBoxColumn ComputedColumn(string header, int fillWeight)
    {
        return new DataGridViewTextBoxColumn
        {
            HeaderText = header,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = fillWeight,
            SortMode = DataGridViewColumnSortMode.NotSortable
        };
    }

    private void PopulateCustomerInfo()
    {
        lblHeading.Text = $"تفاصيل العميل — {_customer.FullName} ({_customer.CustomerNumber})";
        lblCustomerNumberValue.Text = _customer.CustomerNumber;
        lblFullNameValue.Text = _customer.FullName;
        lblPhoneValue.Text = string.IsNullOrWhiteSpace(_customer.Phone) ? "—" : _customer.Phone;
        lblAddressValue.Text = string.IsNullOrWhiteSpace(_customer.Address) ? "—" : _customer.Address;
        lblFamilyCountValue.Text = _customer.FamilyMembersCount.HasValue ? _customer.FamilyMembersCount.Value.ToString() : "—";
        lblStatusValue.Text = _customer.Status == 1 ? "نشط" : "غير نشط";
        lblStatusValue.ForeColor = _customer.Status == 1 ? UiTheme.Success : UiTheme.Danger;
        lblNotesValue.Text = string.IsNullOrWhiteSpace(_customer.Notes) ? "—" : _customer.Notes;
    }

    private void RegisterEventHandlers()
    {
        btnBack.Click += (_, _) => Close();

        KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                Close();
            }
        };

        btnAddMeter.Click += async (_, _) =>
        {
            using var dlg = new AddMeterForm(_customer, _meterService);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                await RefreshDataAsync();
            }
        };

        btnOpenCollection.Click += async (_, _) =>
        {
            using var collection = new FieldCollectionForm();
            try
            {
                await collection.LoadByCustomerIdAsync(_customer.CustomerId, _cts?.Token ?? CancellationToken.None);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (SqlException ex)
            {
                UiMessages.Error($"تعذر فتح شاشة التحصيل: {ex.Message}", "خطأ في الاتصال بقاعدة البيانات");
                return;
            }
            catch (Exception ex)
            {
                UiMessages.Error($"تعذر فتح شاشة التحصيل: {ex.Message}");
                return;
            }

            collection.ShowDialog(this);
        };
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        _ = RefreshDataAsync();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _cts?.Cancel();
        base.OnFormClosing(e);
    }

    private async Task RefreshDataAsync()
    {
        lblFooter.Text = "جارٍ تحميل البيانات...";
        lblFooter.ForeColor = UiTheme.TextSecondary;

        try
        {
            var result = await _billingService.GetCustomerCollectionScreenAsync(
                _customer.CustomerId,
                null,
                null,
                _cts?.Token ?? CancellationToken.None);

            if (IsDisposed)
            {
                return;
            }

            var meters = result.Meters.ToList();
            var invoices = result.OpenInvoices.ToList();
            meters.Sort((x, y) => string.Compare(x.MeterNumber, y.MeterNumber, StringComparison.Ordinal));

            dgvMeters.DataSource = null;
            dgvMeters.DataSource = meters;

            dgvInvoices.DataSource = null;
            dgvInvoices.DataSource = invoices;

            lblMetersTitle.Text = $"عدادات العميل ({meters.Count})";

            var openBalance = invoices.Sum(i => i.BalanceAmount);
            lblInvoicesTitle.Text = $"الفواتير المفتوحة ({invoices.Count}) — المتبقي الإجمالي: {openBalance:N2} ر.س";
            lblInvoicesTitle.ForeColor = openBalance > 0m ? UiTheme.Danger : UiTheme.Success;

            lblFooter.Text = invoices.Count == 0
                ? meters.Count == 0 ? "لا توجد عدادات لهذا العميل بعد." : "لا توجد فواتير مفتوحة لهذا العميل."
                : string.Empty;
            lblFooter.ForeColor = UiTheme.TextSecondary;
        }
        catch (OperationCanceledException)
        {
            // Form closed while loading.
        }
        catch (SqlException ex)
        {
            if (!IsDisposed)
            {
                lblFooter.Text = $"تعذر التحميل: {ex.Message}";
                lblFooter.ForeColor = UiTheme.Danger;
            }
        }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                lblFooter.Text = $"خطأ غير متوقع: {ex.Message}";
                lblFooter.ForeColor = UiTheme.Danger;
            }
        }
    }

    private void OnMetersCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        if (e.ColumnIndex == _colMeterDirection?.Index)
        {
            var meter = dgvMeters.Rows[e.RowIndex].DataBoundItem as Meter;
            e.Value = DirectionName(meter?.ReadingDirection);
            e.FormattingApplied = true;
            return;
        }

        if (e.ColumnIndex == _colMeterStatus?.Index)
        {
            var meter = dgvMeters.Rows[e.RowIndex].DataBoundItem as Meter;
            var active = meter?.Status == 1;
            e.Value = active ? "نشط" : "غير نشط";
            if (e.CellStyle is { } style)
            {
                style.ForeColor = active ? UiTheme.Success : UiTheme.Danger;
            }
            e.FormattingApplied = true;
            return;
        }

        if (e.Value is null)
        {
            return;
        }

        if (e.ColumnIndex == _colMeterInstallDate?.Index)
        {
            if (e.Value is DateOnly date)
            {
                e.Value = date.ToString("yyyy-MM-dd");
                e.FormattingApplied = true;
            }
            return;
        }

        if (e.ColumnIndex == _colMeterInstallReading?.Index)
        {
            if (e.Value is decimal reading)
            {
                e.Value = reading.ToString("N3");
                e.FormattingApplied = true;
            }
        }
    }

    private void OnInvoicesCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.Value is null)
        {
            return;
        }

        if (e.ColumnIndex == _colInvoiceDate?.Index)
        {
            if (e.Value is DateOnly date)
            {
                e.Value = date.ToString("yyyy-MM-dd");
                e.FormattingApplied = true;
            }
            return;
        }

        if (e.ColumnIndex == _colInvoiceBalance?.Index)
        {
            var invoice = dgvInvoices.Rows[e.RowIndex].DataBoundItem as Invoice;
            var balance = invoice?.BalanceAmount ?? 0m;
            if (e.CellStyle is { } style)
            {
                style.ForeColor = balance > 0m ? UiTheme.Danger : UiTheme.Success;
            }
        }
    }

    private static string DirectionName(byte? direction) => direction switch
    {
        1 => "تصاعدي",
        2 => "تنازلي",
        _ => "—"
    };
}