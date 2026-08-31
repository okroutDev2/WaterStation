using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

public partial class CustomerCollectionForm : Form
{
    private readonly BillingService _billingService;
    private CancellationTokenSource? _searchCts;
    private long _searchGeneration;
    private IReadOnlyList<Invoice> _currentOpenInvoices = Array.Empty<Invoice>();
    private IReadOnlyList<Meter> _currentMeters = Array.Empty<Meter>();
    private IReadOnlyList<Payment> _currentPayments = Array.Empty<Payment>();
    private Customer? _currentCustomer;

    private Label? _lblNoInvoices;
    private Label? _lblNoMeters;

    public CustomerCollectionForm() : this(new BillingService(new Database()))
    {
    }

    public CustomerCollectionForm(BillingService billingService)
    {
        _billingService = billingService ?? throw new ArgumentNullException(nameof(billingService));
        InitializeComponent();
        ConfigureGrids();
        ApplyUiTheme();
        RegisterEventHandlers();
    }

    private void ConfigureGrids()
    {
        // 1. Open Invoices Grid
        dgvOpenInvoices.Columns.Clear();
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.InvoiceNumber),
            HeaderText = "رقم الفاتورة",
            FillWeight = 90
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.BillingYear),
            HeaderText = "السنة",
            FillWeight = 50,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.BillingMonth),
            HeaderText = "الشهر",
            FillWeight = 45,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.InvoiceDate),
            HeaderText = "تاريخ الفاتورة",
            FillWeight = 85,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.PreviousReading),
            HeaderText = "القراءة السابقة",
            FillWeight = 80,
            DefaultCellStyle = { Format = "N3", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.CurrentReading),
            HeaderText = "القراءة الحالية",
            FillWeight = 80,
            DefaultCellStyle = { Format = "N3", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.UnitsConsumed),
            HeaderText = "الاستهلاك",
            FillWeight = 70,
            DefaultCellStyle = { Format = "N3", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.WaterAmount),
            HeaderText = "قيمة المياه",
            FillWeight = 75,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.SubscriptionAmount),
            HeaderText = "الاشتراك",
            FillWeight = 70,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.PenaltyAmount),
            HeaderText = "الغرامة",
            FillWeight = 65,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.ArrearsAmount),
            HeaderText = "المتأخرات",
            FillWeight = 75,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.TotalAmount),
            HeaderText = "الإجمالي",
            FillWeight = 85,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font(UiTheme.DefaultFontFamily, 9.5F, FontStyle.Bold) }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.PaidAmount),
            HeaderText = "المدفوع",
            FillWeight = 80,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = UiTheme.Success }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.BalanceAmount),
            HeaderText = "المتبقي",
            FillWeight = 85,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = UiTheme.Danger, Font = new Font(UiTheme.DefaultFontFamily, 9.5F, FontStyle.Bold) }
        });
        dgvOpenInvoices.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Invoice.StatusName),
            HeaderText = "الحالة",
            FillWeight = 80,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        // 2. Meters Grid
        dgvMeters.Columns.Clear();
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.MeterNumber),
            HeaderText = "رقم العداد",
            FillWeight = 100
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.BranchName),
            HeaderText = "الفرع",
            FillWeight = 90
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.AreaName),
            HeaderText = "المنطقة",
            FillWeight = 90
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.MeterTypeName),
            HeaderText = "نوع العداد",
            FillWeight = 90
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.ReadingDirectionName),
            HeaderText = "اتجاه القراءة",
            FillWeight = 85,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.LastReadingDate),
            HeaderText = "تاريخ آخر قراءة",
            FillWeight = 95,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.LastReadingValue),
            HeaderText = "آخر قراءة",
            FillWeight = 85,
            DefaultCellStyle = { Format = "N3", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.LastConsumption),
            HeaderText = "آخر استهلاك",
            FillWeight = 85,
            DefaultCellStyle = { Format = "N3", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvMeters.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Meter.CumulativeConsumption),
            HeaderText = "الاستهلاك التراكمي",
            FillWeight = 105,
            DefaultCellStyle = { Format = "N3", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvMeters.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = nameof(Meter.LastIsReverseMeter),
            HeaderText = "عداد معكوس؟",
            FillWeight = 80,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        // 3. Payments Grid
        dgvPayments.Columns.Clear();
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.PaymentDate),
            HeaderText = "تاريخ الدفعة",
            FillWeight = 110,
            DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm", Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.Amount),
            HeaderText = "مبلغ الدفعة",
            FillWeight = 90,
            DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font(UiTheme.DefaultFontFamily, 9.5F, FontStyle.Bold), ForeColor = UiTheme.Success }
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.PaymentMethodName),
            HeaderText = "طريقة الدفع",
            FillWeight = 90
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.ReferenceNumber),
            HeaderText = "رقم المرجع",
            FillWeight = 95
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.InvoiceNumber),
            HeaderText = "رقم الفاتورة",
            FillWeight = 90
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.ReceiptNumber),
            HeaderText = "رقم الإيصال",
            FillWeight = 90
        });
        dgvPayments.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = nameof(Payment.IsReversed),
            HeaderText = "معكوسة؟",
            FillWeight = 65,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.ReversalDate),
            HeaderText = "تاريخ العكس",
            FillWeight = 110,
            DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm", Alignment = DataGridViewContentAlignment.MiddleCenter }
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.ReversalReason),
            HeaderText = "سبب العكس",
            FillWeight = 120
        });
        dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Payment.Notes),
            HeaderText = "ملاحظات",
            FillWeight = 110
        });
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.ApplyGridDefaults(dgvOpenInvoices);
        UiTheme.ApplyGridDefaults(dgvMeters);
        UiTheme.ApplyGridDefaults(dgvPayments);
        UiTheme.StyleTertiaryButton(btnSearch);
        UiTheme.StyleTertiaryButton(btnClear);
        UiTheme.StylePrimaryButton(btnPayInvoice);
        UiTheme.StylePrimaryButton(btnEnterReading);
        UiTheme.StyleDangerButton(btnReversePayment);

        txtCustomerNumber.AccessibleName = "البحث برقم العميل";
        txtMeterNumber.AccessibleName = "البحث برقم العداد";
        txtCustomerId.AccessibleName = "البحث بمعرف العميل";
        btnSearch.AccessibleName = "بحث";
        btnClear.AccessibleName = "مسح";
        btnPayInvoice.AccessibleName = "سداد الفاتورة المحددة";
        btnEnterReading.AccessibleName = "إدخال قراءة للعداد المحدد";
        btnReversePayment.AccessibleName = "عكس الدفعة المحددة";
        dgvOpenInvoices.AccessibleName = "جدول الفواتير المفتوحة";
        dgvMeters.AccessibleName = "جدول عدادات العميل";
        dgvPayments.AccessibleName = "جدول الدفعات والإيصالات";

        _lblNoInvoices = BuildTabEmptyLabel("لا توجد فواتير مفتوحة للعميل.");
        _lblNoMeters = BuildTabEmptyLabel("لا توجد عدادات مسجلة للعميل.");
        tabOpenInvoices.Controls.Add(_lblNoInvoices);
        tabMeters.Controls.Add(_lblNoMeters);
        _lblNoInvoices.BringToFront();
        _lblNoMeters.BringToFront();
        UpdateTabEmptyStates();
    }

    private static Label BuildTabEmptyLabel(string message)
    {
        return new Label
        {
            Dock = DockStyle.Top,
            Height = 26,
            Font = new Font(UiTheme.DefaultFontFamily, 9.5F, FontStyle.Bold),
            ForeColor = UiTheme.TextSecondary,
            TextAlign = ContentAlignment.MiddleCenter,
            Text = message,
            Visible = false,
            AutoSize = false
        };
    }

    private void UpdateTabEmptyStates()
    {
        if (_lblNoInvoices is null || _lblNoMeters is null)
        {
            return;
        }

        _lblNoInvoices.Visible = _currentOpenInvoices.Count == 0;
        _lblNoMeters.Visible = _currentMeters.Count == 0;
    }

    private void RegisterEventHandlers()
    {
        btnSearch.Click += async (s, e) => await SearchCustomerAsync();
        btnClear.Click += (s, e) => ClearAll();

        txtCustomerNumber.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await SearchCustomerAsync(); } };
        txtMeterNumber.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await SearchCustomerAsync(); } };
        txtCustomerId.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await SearchCustomerAsync(); } };

        KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5)
            {
                e.Handled = true;
                await SearchCustomerAsync();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ClearAll();
                e.Handled = true;
            }
        };


        dgvOpenInvoices.SelectionChanged += (s, e) => UpdateSelectedInvoiceSummary();
        dgvOpenInvoices.CellDoubleClick += async (s, e) =>
        {
            if (e.RowIndex >= 0) await OpenPaymentFormAsync();
        };

        btnPayInvoice.Click += async (s, e) => await OpenPaymentFormAsync();

        dgvPayments.SelectionChanged += (s, e) => UpdateReversePaymentButtonState();
        btnReversePayment.Click += async (s, e) => await OpenReversePaymentFormAsync();

        dgvMeters.SelectionChanged += (s, e) => UpdateMeterReadingButtonState();
        btnEnterReading.Click += async (s, e) => await OpenMeterReadingFormAsync();
        dgvMeters.CellDoubleClick += async (s, e) =>
        {
            if (e.RowIndex >= 0) await OpenMeterReadingFormAsync();
        };
    }

    private void UpdateMeterReadingButtonState()
    {
        btnEnterReading.Visible =
            dgvMeters.CurrentRow?.DataBoundItem is Meter selectedMeter &&
            selectedMeter.MeterId > 0;
    }

    private async Task OpenMeterReadingFormAsync()
    {
        if (dgvMeters.CurrentRow?.DataBoundItem is not Meter selectedMeter)
        {
            MessageBox.Show("يرجى تحديد عداد صالح من جدول عدادات العميل أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        using var meterReadingForm = new MeterReadingForm(meterForForm);
        if (meterReadingForm.ShowDialog(this) == DialogResult.OK)
        {
            // Refresh customer collection data from the service after a successful reading save
            await SearchCustomerAsync();
        }
    }

    private void UpdateReversePaymentButtonState()
    {
        btnReversePayment.Visible =
            dgvPayments.CurrentRow?.DataBoundItem is Payment selectedPayment &&
            selectedPayment.PaymentId > 0 &&
            !selectedPayment.IsReversed;
    }

    private async Task OpenReversePaymentFormAsync()
    {
        if (dgvPayments.CurrentRow?.DataBoundItem is not Payment selectedPayment)
        {
            MessageBox.Show("يرجى تحديد دفعة صالحة من جدول سجل المدفوعات أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (selectedPayment.IsReversed)
        {
            MessageBox.Show("هذه الدفعة معكوسة مسبقًا ولا يمكن عكسها مرة ثانية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var reversePaymentForm = new ReversePaymentForm(selectedPayment);
        if (reversePaymentForm.ShowDialog(this) == DialogResult.OK)
        {
            var reversalResult = reversePaymentForm.ExecutionResult;
            if (reversalResult is not null)
            {
                // Refresh customer collection data after a successful reversal (reload from service)
                await SearchCustomerAsync();
            }
        }
    }

    private async Task OpenPaymentFormAsync()
    {
        if (dgvOpenInvoices.CurrentRow?.DataBoundItem is not Invoice selectedInvoice)
        {
            MessageBox.Show("يرجى تحديد فاتورة مفتوحة من الجدول أولاً لإجراء عملية السداد.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var paymentForm = new PaymentForm(selectedInvoice);
        if (paymentForm.ShowDialog(this) == DialogResult.OK)
        {
            // Refresh customer collection data after successful payment
            await SearchCustomerAsync();
        }
    }

    private async Task SearchCustomerAsync()

    {
        var custNumText = txtCustomerNumber.Text.Trim();
        var meterNumText = txtMeterNumber.Text.Trim();
        var custIdText = txtCustomerId.Text.Trim();

        int? customerId = null;
        if (!string.IsNullOrWhiteSpace(custIdText))
        {
            if (int.TryParse(custIdText, out var parsedId) && parsedId > 0)
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

        string? customerNumber = string.IsNullOrWhiteSpace(custNumText) ? null : custNumText;
        string? meterNumber = string.IsNullOrWhiteSpace(meterNumText) ? null : meterNumText;

        if (!customerId.HasValue && customerNumber is null && meterNumber is null)
        {
            tslblStatus.Text = "يرجى إدخال رقم العميل أو رقم العداد أو معرف العميل للبحث.";
            txtCustomerNumber.Focus();
            return;
        }

        var searchGeneration = Interlocked.Increment(ref _searchGeneration);
        var searchCts = new CancellationTokenSource();
        var previousSearchCts = Interlocked.Exchange(ref _searchCts, searchCts);
        previousSearchCts?.Cancel();
        previousSearchCts?.Dispose();

        SetLoadingState(true);

        try
        {
            var result = await _billingService.GetCustomerCollectionScreenAsync(
                customerId,
                customerNumber,
                meterNumber,
                searchCts.Token);

            if (IsCurrentSearch(searchGeneration, searchCts))
            {
                DisplayResult(result);
            }
        }
        catch (OperationCanceledException)
        {
            if (IsCurrentSearch(searchGeneration, searchCts))
            {
                tslblStatus.Text = "تم إلغاء عملية البحث.";
            }
        }
        catch (SqlException sqlEx)
        {
            if (IsCurrentSearch(searchGeneration, searchCts))
            {
                ClearDisplay();
                tslblStatus.Text = "حدث خطأ أثناء البحث.";
                MessageBox.Show(sqlEx.Message, "تنبيه من قاعدة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            if (IsCurrentSearch(searchGeneration, searchCts))
            {
                ClearDisplay();
                tslblStatus.Text = "حدث خطأ غير متوقع.";
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        finally
        {
            if (IsCurrentSearch(searchGeneration, searchCts))
            {
                Interlocked.CompareExchange(ref _searchCts, null, searchCts);
                searchCts.Dispose();
                SetLoadingState(false);
            }
        }
    }

    private bool IsCurrentSearch(long searchGeneration, CancellationTokenSource searchCts) =>
        searchGeneration == Volatile.Read(ref _searchGeneration) &&
        ReferenceEquals(searchCts, Volatile.Read(ref _searchCts)) &&
        !IsDisposed &&
        !Disposing;

    private void DisplayResult(CustomerCollectionScreenResult result)
    {
        if (result.Customer is null)
        {
            ClearDisplay();
            tslblStatus.Text = "لم يتم العثور على العميل.";
            return;
        }

        // 1. Customer Info
        _currentCustomer = result.Customer;
        lblCustNumValue.Text = result.Customer.CustomerNumber;
        lblCustNameValue.Text = result.Customer.FullName;
        lblCustPhoneValue.Text = string.IsNullOrWhiteSpace(result.Customer.Phone) ? "—" : result.Customer.Phone;
        lblCustAddressValue.Text = string.IsNullOrWhiteSpace(result.Customer.Address) ? "—" : result.Customer.Address;
        lblCustStatusValue.Text = result.Customer.Status == 1 ? "نشط" : "غير نشط";
        lblCustStatusValue.ForeColor = result.Customer.Status == 1 ? UiTheme.Success : UiTheme.Danger;

        // 2. Open Invoices
        _currentOpenInvoices = result.OpenInvoices;
        dgvOpenInvoices.DataSource = _currentOpenInvoices.ToList();
        tabOpenInvoices.Text = $"الفواتير المفتوحة ({_currentOpenInvoices.Count})";

        // 3. Meters
        _currentMeters = result.Meters;
        dgvMeters.DataSource = _currentMeters.ToList();
        tabMeters.Text = $"عدادات العميل ({_currentMeters.Count})";

        // 4. Payments & Receipts
        _currentPayments = result.Payments;
        dgvPayments.DataSource = _currentPayments.ToList();
        tabPayments.Text = $"سجل المدفوعات والإيصالات والعكس ({_currentPayments.Count})";

        // Status bar & Selected Summary
        tslblStatus.Text = "تم جلب البيانات بنجاح.";
        tslblCounts.Text = $"العدادات: {_currentMeters.Count} | الفواتير المفتوحة: {_currentOpenInvoices.Count} | المدفوعات: {_currentPayments.Count}";

        UpdateSelectedInvoiceSummary();
        UpdateReversePaymentButtonState();
        UpdateMeterReadingButtonState();
        UpdateTabEmptyStates();
    }

    private void UpdateSelectedInvoiceSummary()
    {
        if (dgvOpenInvoices.CurrentRow?.DataBoundItem is Invoice selectedInvoice)
        {
            lblSummaryTotalVal.Text = UiText.Currency(selectedInvoice.TotalAmount);
            lblSummaryPaidVal.Text = UiText.Currency(selectedInvoice.PaidAmount);
            lblSummaryBalanceVal.Text = UiText.Currency(selectedInvoice.BalanceAmount);
            lblSummaryStatusVal.Text = string.IsNullOrWhiteSpace(selectedInvoice.StatusName)
                ? (selectedInvoice.Status == 1 ? "غير مسددة" : selectedInvoice.Status == 2 ? "مسددة جزئياً" : selectedInvoice.Status == 3 ? "مسددة بالكامل" : "ملغاة")
                : selectedInvoice.StatusName;
        }
        else if (_currentOpenInvoices.Count > 0)
        {
            var first = _currentOpenInvoices[0];
            lblSummaryTotalVal.Text = UiText.Currency(first.TotalAmount);
            lblSummaryPaidVal.Text = UiText.Currency(first.PaidAmount);
            lblSummaryBalanceVal.Text = UiText.Currency(first.BalanceAmount);
            lblSummaryStatusVal.Text = string.IsNullOrWhiteSpace(first.StatusName) ? "غير مسددة" : first.StatusName;
        }
        else
        {
            lblSummaryTotalVal.Text = "0.00 ر.س";
            lblSummaryPaidVal.Text = "0.00 ر.س";
            lblSummaryBalanceVal.Text = "0.00 ر.س";
            lblSummaryStatusVal.Text = "—";
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
        ClearDisplay();
        tslblStatus.Text = "جاهز";
        tslblCounts.Text = string.Empty;
        txtCustomerNumber.Focus();
    }

    private void ClearDisplay()
    {
        lblCustNumValue.Text = "—";
        lblCustNameValue.Text = "—";
        lblCustPhoneValue.Text = "—";
        lblCustAddressValue.Text = "—";
        lblCustStatusValue.Text = "—";
        lblCustStatusValue.ForeColor = UiTheme.TextSecondary;

        _currentCustomer = null;
        _currentOpenInvoices = Array.Empty<Invoice>();
        _currentMeters = Array.Empty<Meter>();
        _currentPayments = Array.Empty<Payment>();

        dgvOpenInvoices.DataSource = null;
        dgvMeters.DataSource = null;
        dgvPayments.DataSource = null;

        tabOpenInvoices.Text = "الفواتير المفتوحة (0)";
        tabMeters.Text = "عدادات العميل (0)";
        tabPayments.Text = "سجل المدفوعات والإيصالات والعكس (0)";

        UpdateSelectedInvoiceSummary();
        UpdateReversePaymentButtonState();
        UpdateMeterReadingButtonState();
        UpdateTabEmptyStates();
    }

    private void SetLoadingState(bool isLoading)
    {
        btnSearch.Enabled = !isLoading;
        btnClear.Enabled = !isLoading;
        txtCustomerNumber.Enabled = !isLoading;
        txtMeterNumber.Enabled = !isLoading;
        txtCustomerId.Enabled = !isLoading;

        tspbProgress.Visible = isLoading;
        if (isLoading)
        {
            tslblStatus.Text = "جاري البحث في قاعدة البيانات...";
        }
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
