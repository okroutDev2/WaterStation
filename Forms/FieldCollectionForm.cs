using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

/// <summary>
/// Unified, fast field-collection screen for collectors.
/// Orchestrates: find customer -> read meter -> create invoice -> pay / reverse.
/// All data is (re)loaded from Billing.GetCustomerCollectionScreenAsync; totals are
/// never computed locally (Billing.CreateInvoice is the only invoice-authority).
/// </summary>
public partial class FieldCollectionForm : Form
{
    private readonly BillingService _billingService;
    private CancellationTokenSource? _searchCts;
    private long _searchGeneration;
    private bool _isProcessing;

    private Customer? _currentCustomer;
    private IReadOnlyList<Meter> _currentMeters = Array.Empty<Meter>();
    private IReadOnlyList<Invoice> _currentOpenInvoices = Array.Empty<Invoice>();
    private IReadOnlyList<Payment> _currentPayments = Array.Empty<Payment>();

    private int? _lastCustomerId;
    private string? _lastCustomerNumber;
    private string? _lastMeterNumber;

    private int colInvStatus = -1;
    private int colInvBalance = -1;
    private int colPayReversed = -1;
    private int colPayReason = -1;

    public FieldCollectionForm() : this(new BillingService(new Database()))
    {
    }

    public FieldCollectionForm(BillingService billingService)
    {
        _billingService = billingService ?? throw new ArgumentNullException(nameof(billingService));
        InitializeComponent();
        ConfigureGrids();
        ApplyUiTheme();
        PopulateBillingMonths();
        RegisterEventHandlers();
        SetInitialState();
    }

    /// <summary>
    /// Public entry for callers/tests that already know a customer id
    /// (e.g. quickly opening the screen on a specific account).
    /// </summary>
    public Task LoadByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
    {
        if (customerId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(customerId), "يجب إدخال معرف عميل صحيح أكبر من الصفر.");
        }

        return LoadCollectionAsync(customerId, null, null, cancellationToken);
    }

    private void ConfigureGrids()
    {
        // 1. Open Invoices Grid
        dgvOpenInvoices.Columns.Clear();
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.InvoiceNumber), "رقم الفاتورة", 95));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.BillingYear), "السنة", 45, "0", center: true));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.BillingMonth), "الشهر", 45, "0", center: true));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.InvoiceDate), "تاريخ الفاتورة", 80, "yyyy-MM-dd", center: true));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.PreviousReading), "القراءة السابقة", 70, "N3"));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.CurrentReading), "القراءة الحالية", 70, "N3"));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.UnitsConsumed), "الاستهلاك", 65, "N3"));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.WaterAmount), "قيمة المياه", 70, "N2"));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.SubscriptionAmount), "الاشتراك", 65, "N2"));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.PenaltyAmount), "الغرامة", 60, "N2"));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.ArrearsAmount), "المتأخرات", 65, "N2"));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.TotalAmount), "الإجمالي", 80, "N2", bold: true));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.PaidAmount), "المدفوع", 70, "N2", foreColor: UiTheme.Success));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.BalanceAmount), "المتبقي", 80, "N2", foreColor: UiTheme.Danger, bold: true));
        dgvOpenInvoices.Columns.Add(InvColumn(nameof(Invoice.StatusName), "الحالة", 75, center: true));
        colInvStatus = dgvOpenInvoices.Columns[nameof(Invoice.StatusName)].Index;
        colInvBalance = dgvOpenInvoices.Columns[nameof(Invoice.BalanceAmount)].Index;

        // 2. Payments Grid
        dgvPayments.Columns.Clear();
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.PaymentDate), "تاريخ الدفعة", 110, "yyyy-MM-dd HH:mm", center: true));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.Amount), "مبلغ الدفعة", 80, "N2", foreColor: UiTheme.Success, bold: true));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.PaymentMethodName), "طريقة الدفع", 85));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.ReferenceNumber), "رقم المرجع", 85));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.InvoiceNumber), "رقم الفاتورة", 85));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.ReceiptNumber), "رقم الإيصال", 85));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.IsReversed), "معكوسة؟", 65, center: true));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.ReversalDate), "تاريخ العكس", 110, "yyyy-MM-dd HH:mm", center: true));
        dgvPayments.Columns.Add(PayColumn(nameof(Payment.ReversalReason), "سبب العكس", 130));
        colPayReversed = dgvPayments.Columns[nameof(Payment.IsReversed)].Index;
        colPayReason = dgvPayments.Columns[nameof(Payment.ReversalReason)].Index;
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.ApplyGridDefaults(dgvOpenInvoices);
        UiTheme.ApplyGridDefaults(dgvPayments);
        UiTheme.StyleTertiaryButton(btnSearch);
        UiTheme.StyleTertiaryButton(btnClear);
        UiTheme.StylePrimaryButton(btnCreateReading);
        UiTheme.StylePrimaryButton(btnCreateInvoice);
        UiTheme.StyleSecondaryButton(btnPayInvoice);
        UiTheme.StyleDangerButton(btnReversePayment);

        pnlHeader.BackColor = UiTheme.PrimaryDark;
        pnlSearch.BackColor = UiTheme.SurfaceAlt;
        pnlActions.BackColor = UiTheme.SurfaceAlt;

        lblSearchHint.ForeColor = UiTheme.TextSecondary;
        lblMeterNotice.ForeColor = UiTheme.Danger;
        lblNoInvoices.ForeColor = UiTheme.TextSecondary;
        lblNoPayments.ForeColor = UiTheme.TextSecondary;
        lblInvCurrencyNote.ForeColor = UiTheme.TextSecondary;
        lblPayCurrencyNote.ForeColor = UiTheme.TextSecondary;

        txtCustomerNumber.AccessibleName = "البحث برقم العميل";
        txtMeterNumber.AccessibleName = "البحث برقم العداد";
        txtCustomerId.AccessibleName = "البحث بمعرف العميل";
        btnSearch.AccessibleName = "بحث";
        btnClear.AccessibleName = "مسح";
        cmbMeters.AccessibleName = "قائمة عدادات العميل";
        nudBillingYear.AccessibleName = "سنة الفاتورة";
        cmbBillingMonth.AccessibleName = "شهر الفاتورة";
        btnCreateReading.AccessibleName = "إدخال قراءة العداد";
        btnCreateInvoice.AccessibleName = "إنشاء فاتورة";
        btnPayInvoice.AccessibleName = "سداد الفاتورة المحددة";
        btnReversePayment.AccessibleName = "عكس الدفعة المحددة";
        dgvOpenInvoices.AccessibleName = "جدول الفواتير المفتوحة";
        dgvPayments.AccessibleName = "جدول الدفعات";
    }

    private static DataGridViewTextBoxColumn InvColumn(
        string property, string header, int weight, string? format = null,
        bool center = false, Color? foreColor = null, bool bold = false)
    {
        var column = new DataGridViewTextBoxColumn
        {
            Name = property,
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = weight,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = center ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleRight,
                ForeColor = foreColor ?? UiTheme.TextPrimary,
                Font = bold ? new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold) : null,
                Format = format
            }
        };
        return column;
    }

    private static DataGridViewTextBoxColumn PayColumn(
        string property, string header, int weight, string? format = null,
        bool center = false, Color? foreColor = null, bool bold = false)
    {
        var column = new DataGridViewTextBoxColumn
        {
            Name = property,
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = weight,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = center ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleRight,
                ForeColor = foreColor ?? UiTheme.TextPrimary,
                Font = bold ? new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold) : null,
                Format = format
            }
        };
        return column;
    }

    private void PopulateBillingMonths()
    {
        var gregorianArabic = new System.Globalization.CultureInfo("ar-EG");
        for (var month = 1; month <= 12; month++)
        {
            cmbBillingMonth.Items.Add($"{month} — {gregorianArabic.DateTimeFormat.GetMonthName(month)}");
        }
    }

    private void RegisterEventHandlers()
    {
        Shown += (s, e) =>
        {
            if (!_isProcessing)
            {
                txtCustomerNumber.Focus();
            }
        };

        btnSearch.Click += async (s, e) => { if (!_isProcessing) await SearchCustomerAsync(); };
        btnClear.Click += (s, e) => { if (!_isProcessing) ClearAll(); };

        txtCustomerNumber.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await TriggerSearchWithGuard(); } };
        txtMeterNumber.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await TriggerSearchWithGuard(); } };
        txtCustomerId.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await TriggerSearchWithGuard(); } };

        KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5)
            {
                e.Handled = true;
                await TriggerSearchWithGuard();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                if (!_isProcessing)
                {
                    ClearAll();
                }
            }
        };

        cmbMeters.SelectedIndexChanged += (s, e) =>
        {
            UpdateMeterCard();
            UpdateActionButtons();
        };
        cmbMeters.Format += (s, e) =>
        {
            if (e.ListItem is Meter meter)
            {
                e.Value = $"{meter.MeterNumber} — {meter.BranchName} — {meter.AreaName}";
            }
        };

        dgvOpenInvoices.SelectionChanged += (s, e) =>
        {
            UpdateSelectedInvoiceSummary();
            UpdateActionButtons();
        };
        dgvOpenInvoices.CellDoubleClick += async (s, e) => { if (e.RowIndex >= 0) await OpenPaymentFormAsync(); };
        dgvOpenInvoices.CellFormatting += (s, e) => FormatInvoiceCell(e);
        dgvOpenInvoices.RowPrePaint += (s, e) => PaintInvoiceRow(e);

        dgvPayments.SelectionChanged += (s, e) =>
        {
            UpdateSelectedPaymentSummary();
            UpdateActionButtons();
        };
        dgvPayments.CellFormatting += (s, e) => FormatPaymentCell(e);
        dgvPayments.RowPrePaint += (s, e) => PaintPaymentRow(e);

        btnCreateReading.Click += async (s, e) => await OpenMeterReadingFormAsync();
        btnCreateInvoice.Click += async (s, e) => await CreateInvoiceAsync();
        btnPayInvoice.Click += async (s, e) => await OpenPaymentFormAsync();
        btnReversePayment.Click += async (s, e) => await OpenReversePaymentFormAsync();

        nudBillingYear.ValueChanged += (s, e) => ApplyBillingYearToMonths();
    }

    private async Task TriggerSearchWithGuard()
    {
        if (_isProcessing)
        {
            return;
        }
        await SearchCustomerAsync();
    }

    private Meter? CurrentMeter => cmbMeters.SelectedItem as Meter;

    private Invoice? CurrentInvoice => dgvOpenInvoices.CurrentRow?.DataBoundItem as Invoice;

    private Payment? CurrentPayment => dgvPayments.CurrentRow?.DataBoundItem as Payment;

    private async Task SearchCustomerAsync()
    {
        var customerNumber = txtCustomerNumber.Text.Trim();
        var meterNumber = txtMeterNumber.Text.Trim();
        var customerIdText = txtCustomerId.Text.Trim();

        int? customerId = null;
        if (!string.IsNullOrWhiteSpace(customerIdText))
        {
            if (int.TryParse(customerIdText, out var parsedId) && parsedId > 0)
            {
                customerId = parsedId;
            }
            else
            {
                tslblStatus.Text = "يرجى إدخال معرف عميل صحيح (أرقام فقط).";
                txtCustomerId.Focus();
                return;
            }
        }

        var normalizedCustomerNumber = string.IsNullOrWhiteSpace(customerNumber) ? null : customerNumber;
        var normalizedMeterNumber = string.IsNullOrWhiteSpace(meterNumber) ? null : meterNumber;

        if (!customerId.HasValue && normalizedCustomerNumber is null && normalizedMeterNumber is null)
        {
            tslblStatus.Text = "يرجى إدخال رقم العميل أو رقم العداد أو معرف العميل للبحث.";
            txtCustomerNumber.Focus();
            return;
        }

        await LoadCollectionAsync(customerId, normalizedCustomerNumber, normalizedMeterNumber);
    }

    private async Task LoadCollectionAsync(int? customerId, string? customerNumber, string? meterNumber, CancellationToken externalToken = default)
    {
        var generation = Interlocked.Increment(ref _searchGeneration);
        var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(externalToken);
        var previousCts = Interlocked.Exchange(ref _searchCts, tokenSource);
        previousCts?.Cancel();
        previousCts?.Dispose();

        _lastCustomerId = customerId;
        _lastCustomerNumber = customerNumber;
        _lastMeterNumber = meterNumber;

        SetProcessingState(true, "جاري البحث في قاعدة البيانات...");

        try
        {
            var result = await _billingService.GetCustomerCollectionScreenAsync(
                customerId,
                customerNumber,
                meterNumber,
                tokenSource.Token);

            if (IsCurrentSearch(generation, tokenSource))
            {
                DisplayResult(result);
            }
        }
        catch (OperationCanceledException)
        {
            if (IsCurrentSearch(generation, tokenSource))
            {
                tslblStatus.Text = "تم إلغاء البحث.";
            }
        }
        catch (SqlException sqlEx)
        {
            if (IsCurrentSearch(generation, tokenSource))
            {
                ClearResult();
                tslblStatus.Text = "حدث خطأ أثناء البحث.";
                ShowSqlError(sqlEx, "البحث في بيانات التحصيل");
            }
        }
        catch (Exception ex)
        {
            if (IsCurrentSearch(generation, tokenSource))
            {
                ClearResult();
                tslblStatus.Text = "حدث خطأ غير متوقع.";
                UiMessages.Error($"حدث خطأ غير متوقع أثناء البحث:\n{ex.Message}");
            }
        }
        finally
        {
            if (IsCurrentSearch(generation, tokenSource))
            {
                Interlocked.CompareExchange(ref _searchCts, null, tokenSource);
                tokenSource.Dispose();
                SetProcessingState(false, "جاهز");
                FocusAfterLoad();
            }
        }
    }

    private bool IsCurrentSearch(long generation, CancellationTokenSource tokenSource) =>
        generation == Volatile.Read(ref _searchGeneration) &&
        ReferenceEquals(tokenSource, Volatile.Read(ref _searchCts)) &&
        !IsDisposed &&
        !Disposing;

    private void DisplayResult(CustomerCollectionScreenResult result)
    {
        if (result.Customer is null)
        {
            ClearResult();
            tslblStatus.Text = "لم يتم العثور على بيانات.";
            return;
        }

        _currentCustomer = result.Customer;

        lblCNumV.Text = _currentCustomer.CustomerNumber;
        lblCNameV.Text = _currentCustomer.FullName;
        lblCPhoneV.Text = string.IsNullOrWhiteSpace(_currentCustomer.Phone) ? "—" : _currentCustomer.Phone;
        lblCAddrV.Text = string.IsNullOrWhiteSpace(_currentCustomer.Address) ? "—" : _currentCustomer.Address;
        lblCStatusV.Text = _currentCustomer.Status == 1 ? "نشط" : "غير نشط";
        lblCStatusV.ForeColor = _currentCustomer.Status == 1 ? UiTheme.Success : UiTheme.Danger;

        // Meters
        _currentMeters = result.Meters;
        cmbMeters.DataSource = _currentMeters.ToList();
        cmbMeters.SelectedIndex = _currentMeters.Count > 0 ? 0 : -1;
        UpdateMeterCard();

        // Open invoices
        _currentOpenInvoices = result.OpenInvoices;
        dgvOpenInvoices.DataSource = _currentOpenInvoices.ToList();
        lblNoInvoices.Visible = _currentOpenInvoices.Count == 0;
        if (dgvOpenInvoices.Rows.Count > 0)
        {
            dgvOpenInvoices.ClearSelection();
            dgvOpenInvoices.Rows[0].Selected = true;
            dgvOpenInvoices.CurrentCell = dgvOpenInvoices.Rows[0].Cells[0];
        }
        UpdateSelectedInvoiceSummary();

        // Payments
        _currentPayments = result.Payments;
        dgvPayments.DataSource = _currentPayments.ToList();
        lblNoPayments.Visible = _currentPayments.Count == 0;
        if (dgvPayments.Rows.Count > 0)
        {
            dgvPayments.ClearSelection();
            dgvPayments.Rows[0].Selected = true;
            dgvPayments.CurrentCell = dgvPayments.Rows[0].Cells[0];
        }
        UpdateSelectedPaymentSummary();

        tslblStatus.Text = "تم جلب البيانات بنجاح.";
        tslblCounts.Text = $"العدادات: {_currentMeters.Count} | الفواتير المفتوحة: {_currentOpenInvoices.Count} | الدفعات: {_currentPayments.Count}";

        UpdateActionButtons();
    }

    private void FocusAfterLoad()
    {
        if (IsDisposed || !Visible)
        {
            return;
        }

        if (_currentMeters.Count > 0 && cmbMeters.Enabled && cmbMeters.CanFocus)
        {
            cmbMeters.Focus();
        }
        else if (txtCustomerNumber.CanFocus)
        {
            txtCustomerNumber.Focus();
        }
    }

    private void UpdateMeterCard()
    {
        var meter = CurrentMeter;

        lblMNumV.Text = meter is null ? "—" : meter.MeterNumber;
        lblMTypeV.Text = meter is null || string.IsNullOrWhiteSpace(meter.MeterTypeName) ? "—" : meter.MeterTypeName;
        lblMDirV.Text = meter is null || string.IsNullOrWhiteSpace(meter.ReadingDirectionName) ? "—" : meter.ReadingDirectionName;
        lblMBranchV.Text = meter is null || string.IsNullOrWhiteSpace(meter.BranchName) ? "—" : meter.BranchName;
        lblMAreaV.Text = meter is null || string.IsNullOrWhiteSpace(meter.AreaName) ? "—" : meter.AreaName;
        lblMLastReadV.Text = meter?.LastReadingValue.HasValue == true ? UiText.Amount3(meter.LastReadingValue.Value) : "—";
        lblMLastReadDateV.Text = meter?.LastReadingDate.HasValue == true ? UiText.Date(meter.LastReadingDate.Value) : "—";
        lblMLastConsV.Text = meter?.LastConsumption.HasValue == true ? UiText.Amount3(meter.LastConsumption.Value) : "—";
        lblMCumulV.Text = meter?.CumulativeConsumption.HasValue == true ? UiText.Amount3(meter.CumulativeConsumption.Value) : "—";

        if (_currentCustomer is null)
        {
            lblMeterNotice.Visible = false;
            lblMeterNotice.Text = string.Empty;
        }
        else if (meter is null)
        {
            lblMeterNotice.Visible = true;
            lblMeterNotice.Text = _currentMeters.Count == 0
                ? "لا توجد عدادات مسجلة لهذا العميل."
                : "يرجى اختيار عداد من القائمة.";
        }
        else if (meter.Status != 1)
        {
            lblMeterNotice.Visible = true;
            lblMeterNotice.Text = "هذا العداد غير نشط — لا يمكن إدخال قراءة أو إنشاء فاتورة له.";
        }
        else
        {
            lblMeterNotice.Visible = false;
            lblMeterNotice.Text = string.Empty;
        }

        UpdateActionButtons();
    }

    private void UpdateSelectedInvoiceSummary()
    {
        lblInvTotalV.Text = "0.00 ر.س";
        lblInvPaidV.Text = "0.00 ر.س";
        lblInvBalV.Text = "0.00 ر.س";
        lblInvStatusV.Text = "—";

        var invoice = CurrentInvoice ?? (_currentOpenInvoices.Count > 0 ? _currentOpenInvoices[0] : null);
        if (invoice is null)
        {
            return;
        }

        lblInvTotalV.Text = UiText.Currency(invoice.TotalAmount);
        lblInvPaidV.Text = UiText.Currency(invoice.PaidAmount);
        lblInvBalV.Text = UiText.Currency(invoice.BalanceAmount);
        lblInvStatusV.Text = UiText.InvoiceStatusName(invoice.Status);
        lblInvStatusV.ForeColor = invoice.Status switch
        {
            2 => UiTheme.Warning,
            3 => UiTheme.Success,
            4 => UiTheme.Danger,
            _ => UiTheme.Accent
        };
    }

    private void UpdateSelectedPaymentSummary()
    {
        lblPayAmtV.Text = "—";
        lblPayMethodV.Text = "—";
        lblPayReceiptV.Text = "—";
        lblPayRevV.Text = "—";
        lblPayReasonV.Text = "—";
        lblPayRevV.ForeColor = UiTheme.Danger;

        var payment = CurrentPayment;
        if (payment is null)
        {
            return;
        }

        lblPayAmtV.Text = UiText.Currency(payment.Amount);
        lblPayMethodV.Text = string.IsNullOrWhiteSpace(payment.PaymentMethodName) ? "—" : payment.PaymentMethodName;
        lblPayReceiptV.Text = string.IsNullOrWhiteSpace(payment.ReceiptNumber) ? "—" : payment.ReceiptNumber;
        lblPayRevV.Text = payment.IsReversed ? "نعم — معكوسة" : "لا";
        lblPayRevV.ForeColor = payment.IsReversed ? UiTheme.Danger : UiTheme.Success;
        lblPayReasonV.Text = string.IsNullOrWhiteSpace(payment.ReversalReason) ? "—" : payment.ReversalReason;
    }

    private void FormatInvoiceCell(DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= dgvOpenInvoices.Rows.Count ||
            dgvOpenInvoices.Rows[e.RowIndex].DataBoundItem is not Invoice invoice ||
            e.CellStyle is null)
        {
            return;
        }

        if (e.ColumnIndex == colInvStatus &&
            !string.IsNullOrWhiteSpace(invoice.StatusName))
        {
            e.Value = invoice.StatusName;
            e.CellStyle.ForeColor = invoice.Status switch
            {
                3 => UiTheme.Success,
                4 => UiTheme.Danger,
                _ => UiTheme.Accent
            };
            e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
            e.FormattingApplied = true;
        }

        if (e.ColumnIndex == colInvBalance)
        {
            e.CellStyle.ForeColor = invoice.BalanceAmount > 0m ? UiTheme.Danger : UiTheme.Success;
            e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
        }
    }

    private void FormatPaymentCell(DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
            e.RowIndex >= dgvPayments.Rows.Count ||
            dgvPayments.Rows[e.RowIndex].DataBoundItem is not Payment payment ||
            e.CellStyle is null)
        {
            return;
        }

        if (e.ColumnIndex == colPayReversed)
        {
            e.Value = payment.IsReversed ? "نعم" : "لا";
            e.CellStyle.ForeColor = payment.IsReversed ? UiTheme.Danger : UiTheme.Success;
            e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
            e.FormattingApplied = true;
        }

        if (e.ColumnIndex == colPayReason && string.IsNullOrWhiteSpace(payment.ReversalReason))
        {
            e.Value = "—";
            e.FormattingApplied = true;
        }
    }

    private void PaintInvoiceRow(DataGridViewRowPrePaintEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvOpenInvoices.Rows.Count ||
            dgvOpenInvoices.Rows[e.RowIndex].DataBoundItem is not Invoice invoice)
        {
            return;
        }

        if (invoice.Status == 3)
        {
            dgvOpenInvoices.Rows[e.RowIndex].DefaultCellStyle.BackColor = UiTheme.SuccessTint;
        }
        else if (invoice.Status == 4)
        {
            dgvOpenInvoices.Rows[e.RowIndex].DefaultCellStyle.BackColor = UiTheme.DangerTint;
        }
        else
        {
            dgvOpenInvoices.Rows[e.RowIndex].DefaultCellStyle.BackColor = UiTheme.Surface;
        }
    }

    private void PaintPaymentRow(DataGridViewRowPrePaintEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvPayments.Rows.Count ||
            dgvPayments.Rows[e.RowIndex].DataBoundItem is not Payment payment)
        {
            return;
        }

        if (payment.IsReversed)
        {
            dgvPayments.Rows[e.RowIndex].DefaultCellStyle.BackColor = UiTheme.WarningTint;
            dgvPayments.Rows[e.RowIndex].DefaultCellStyle.ForeColor = UiTheme.TextSecondary;
        }
        else
        {
            dgvPayments.Rows[e.RowIndex].DefaultCellStyle.BackColor = UiTheme.Surface;
            dgvPayments.Rows[e.RowIndex].DefaultCellStyle.ForeColor = UiTheme.TextPrimary;
        }
    }

    private void UpdateActionButtons()
    {
        var meter = CurrentMeter;
        var customerLoaded = _currentCustomer is not null;
        var meterReady = meter is not null && meter.Status == 1;

        btnCreateReading.Enabled = !_isProcessing && meterReady;
        btnCreateInvoice.Enabled = !_isProcessing && customerLoaded && meterReady && nudBillingYear.Value >= 2015m;

        var invoice = CurrentInvoice;
        btnPayInvoice.Enabled = !_isProcessing && invoice is not null && invoice.BalanceAmount > 0m && invoice.Status != 4;

        var payment = CurrentPayment;
        btnReversePayment.Enabled = !_isProcessing && payment is not null && !payment.IsReversed;

        nudBillingYear.Enabled = !_isProcessing && customerLoaded && meterReady;
        cmbBillingMonth.Enabled = !_isProcessing && customerLoaded && meterReady;
    }

    private async Task OpenMeterReadingFormAsync()
    {
        if (_isProcessing)
        {
            return;
        }

        var selectedMeter = CurrentMeter;
        if (selectedMeter is null)
        {
            UiMessages.Warning("يرجى تحديد عداد من قائمة العدادات أولاً.", "تنبيه");
            return;
        }

        var meterForForm = selectedMeter;
        if (_currentCustomer is not null &&
            (string.IsNullOrWhiteSpace(meterForForm.CustomerNumber) || string.IsNullOrWhiteSpace(meterForForm.FullName)))
        {
            meterForForm = new Meter
            {
                MeterId = selectedMeter.MeterId,
                MeterNumber = selectedMeter.MeterNumber,
                CustomerId = _currentCustomer.CustomerId,
                CustomerNumber = string.IsNullOrWhiteSpace(selectedMeter.CustomerNumber) ? _currentCustomer.CustomerNumber : selectedMeter.CustomerNumber,
                FullName = string.IsNullOrWhiteSpace(selectedMeter.FullName) ? _currentCustomer.FullName : selectedMeter.FullName,
                BranchId = selectedMeter.BranchId,
                BranchCode = selectedMeter.BranchCode,
                BranchName = selectedMeter.BranchName,
                AreaId = selectedMeter.AreaId,
                AreaCode = selectedMeter.AreaCode,
                AreaName = selectedMeter.AreaName,
                MeterTypeId = selectedMeter.MeterTypeId,
                MeterTypeCode = selectedMeter.MeterTypeCode,
                MeterTypeName = selectedMeter.MeterTypeName,
                ReadingDirection = selectedMeter.ReadingDirection,
                ReadingDirectionName = selectedMeter.ReadingDirectionName,
                InstallationDate = selectedMeter.InstallationDate,
                InstallationReading = selectedMeter.InstallationReading,
                Status = selectedMeter.Status,
                LastReadingDate = selectedMeter.LastReadingDate,
                LastReadingValue = selectedMeter.LastReadingValue,
                LastConsumption = selectedMeter.LastConsumption,
                CumulativeConsumption = selectedMeter.CumulativeConsumption,
                LastIsReverseMeter = selectedMeter.LastIsReverseMeter
            };
        }

        using var readingForm = new MeterReadingForm(meterForForm);
        if (readingForm.ShowDialog(this) == DialogResult.OK)
        {
            tslblStatus.Text = "تم حفظ القراءة، جاري إعادة تحميل البيانات...";
            await ReloadScreenAsync();
        }
    }

    private async Task CreateInvoiceAsync()
    {
        if (_isProcessing)
        {
            return;
        }

        var meter = CurrentMeter;
        var customer = _currentCustomer;
        if (customer is null || meter is null)
        {
            UiMessages.Warning("يرجى البحث عن عميل وتحديد عداد قبل إنشاء الفاتورة.", "تنبيه");
            return;
        }

        var year = (short)nudBillingYear.Value;
        var monthIndex = cmbBillingMonth.SelectedIndex;
        if (year < 2015 || monthIndex < 0)
        {
            UiMessages.Warning("يرجى تحديد سنة وشهر فاتورة صحيحين.", "تنبيه");
            return;
        }
        var month = (byte)(monthIndex + 1);

        var confirmation = UiMessages.Confirm(
            $"سيتم إنشاء فاتورة للعداد {meter.MeterNumber} للفترة {month:00}/{year}.\n" +
            "تُحسب إجماليات الفاتورة في قاعدة البيانات (وليس محليًا).\n\nهل تريد المتابعة؟",
            "تأكيد إنشاء الفاتورة");
        if (!confirmation)
        {
            return;
        }

        SetProcessingState(true, "جاري إنشاء الفاتورة في قاعدة البيانات...");
        var created = false;
        try
        {
            var result = await _billingService.CreateInvoiceAsync(
                customerId: customer.CustomerId,
                meterId: meter.MeterId,
                billingYear: year,
                billingMonth: month,
                createdBy: null);

            created = true;
            UiMessages.Information(
                $"تم إنشاء الفاتورة بنجاح.\nالعداد: {meter.MeterNumber}\nالفترة: {month:00}/{year}",
                "نجاح العملية");
            _ = result;
        }
        catch (OperationCanceledException)
        {
            tslblStatus.Text = "تم إلغاء إنشاء الفاتورة.";
        }
        catch (SqlException sqlEx)
        {
            tslblStatus.Text = "فشل إنشاء الفاتورة.";
            ShowSqlError(sqlEx, "إنشاء الفاتورة");
        }
        catch (Exception ex)
        {
            tslblStatus.Text = "حدث خطأ غير متوقع.";
            UiMessages.Error($"حدث خطأ غير متوقع أثناء إنشاء الفاتورة:\n{ex.Message}");
        }
        finally
        {
            SetProcessingState(false, created ? "تم إنشاء الفاتورة." : "جاهز");
        }

        if (created)
        {
            await ReloadScreenAsync();
        }
    }

    private async Task OpenPaymentFormAsync()
    {
        if (_isProcessing)
        {
            return;
        }

        var invoice = CurrentInvoice;
        if (invoice is null)
        {
            UiMessages.Warning("يرجى تحديد فاتورة من الجدول أولاً.", "تنبيه");
            return;
        }

        if (invoice.BalanceAmount <= 0m)
        {
            UiMessages.Information("لا يوجد رصيد متبقي لهذه الفاتورة.", "معلومات");
            return;
        }

        using var paymentForm = new PaymentForm(invoice);
        if (paymentForm.ShowDialog(this) == DialogResult.OK)
        {
            tslblStatus.Text = "تم تسجيل السداد، جاري إعادة تحميل البيانات...";
            await ReloadScreenAsync();
        }
    }

    private async Task OpenReversePaymentFormAsync()
    {
        if (_isProcessing)
        {
            return;
        }

        var payment = CurrentPayment;
        if (payment is null)
        {
            UiMessages.Warning("يرجى تحديد دفعة من سجل المدفوعات أولاً.", "تنبيه");
            return;
        }

        if (payment.IsReversed)
        {
            UiMessages.Information("هذه الدفعة معكوسة مسبقًا ولا يمكن عكسها مرة ثانية.", "معلومات");
            return;
        }

        using var reverseForm = new ReversePaymentForm(payment);
        if (reverseForm.ShowDialog(this) == DialogResult.OK && reverseForm.ExecutionResult is not null)
        {
            tslblStatus.Text = "تم عكس الدفعة، جاري إعادة تحميل البيانات...";
            await ReloadScreenAsync();
        }
    }

    private async Task ReloadScreenAsync()
    {
        await LoadCollectionAsync(_lastCustomerId, _lastCustomerNumber, _lastMeterNumber);
    }

    private void ShowSqlError(SqlException sqlEx, string operation)
    {
        UiMessages.Warning(
            $"تعذر تنفيذ «{operation}» بسبب خطأ من قاعدة البيانات:\n" +
            $"{sqlEx.Message}\n\n" +
            $"رقم الخطأ التقني: {sqlEx.Number}",
            "خطأ في قاعدة البيانات");
    }

    /// <summary>Muted hint shown in the status bar until the user starts searching.</summary>
    private const string InitialSearchHint = "ابحث عن عميل أو أدخل رقم العداد للبدء.";

    private void SetInitialState()
    {
        nudBillingYear.Value = DateTime.Today.Year;
        ApplyBillingYearToMonths();
        ClearResult();
        tslblStatus.Text = InitialSearchHint;
        txtCustomerNumber.Focus();
    }

    private void ApplyBillingYearToMonths()
    {
        if (cmbBillingMonth.Items.Count == 0)
        {
            return;
        }

        var currentMonth = DateTime.Today.Month - 1;
        if ((short)nudBillingYear.Value == DateTime.Today.Year)
        {
            cmbBillingMonth.SelectedIndex = currentMonth;
        }
        else
        {
            cmbBillingMonth.SelectedIndex = 0;
        }
    }

    private void ClearAll()
    {
        Interlocked.Increment(ref _searchGeneration);
        var searchCts = Interlocked.Exchange(ref _searchCts, null);
        searchCts?.Cancel();
        searchCts?.Dispose();

        txtCustomerNumber.Clear();
        txtMeterNumber.Clear();
        txtCustomerId.Clear();
        _lastCustomerId = null;
        _lastCustomerNumber = null;
        _lastMeterNumber = null;
        ClearResult();
        tslblStatus.Text = InitialSearchHint;
        tslblCounts.Text = string.Empty;
        txtCustomerNumber.Focus();
    }

    private void ClearResult()
    {
        _currentCustomer = null;
        _currentMeters = Array.Empty<Meter>();
        _currentOpenInvoices = Array.Empty<Invoice>();
        _currentPayments = Array.Empty<Payment>();

        lblCNumV.Text = "—";
        lblCNameV.Text = "—";
        lblCPhoneV.Text = "—";
        lblCAddrV.Text = "—";
        lblCStatusV.Text = "—";
        lblCStatusV.ForeColor = UiTheme.TextSecondary;

        cmbMeters.DataSource = null;
        UpdateMeterCard();

        dgvOpenInvoices.DataSource = null;
        lblNoInvoices.Visible = false;
        dgvPayments.DataSource = null;
        lblNoPayments.Visible = false;

        UpdateSelectedInvoiceSummary();
        UpdateSelectedPaymentSummary();
        UpdateActionButtons();
    }

    private void SetProcessingState(bool isProcessing, string message)
    {
        _isProcessing = isProcessing;

        btnSearch.Enabled = !isProcessing;
        btnClear.Enabled = !isProcessing;
        txtCustomerNumber.Enabled = !isProcessing;
        txtMeterNumber.Enabled = !isProcessing;
        txtCustomerId.Enabled = !isProcessing;
        cmbMeters.Enabled = !isProcessing && _currentMeters.Count > 0;

        tspbProgress.Visible = isProcessing;
        if (isProcessing)
        {
            tslblStatus.Text = message;
        }

        UpdateActionButtons();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        Interlocked.Increment(ref _searchGeneration);
        var searchCts = Interlocked.Exchange(ref _searchCts, null);
        searchCts?.Cancel();
        searchCts?.Dispose();
        base.OnFormClosing(e);
    }
}