using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

public partial class PaymentForm : Form
{
    private readonly Invoice _invoice;
    private readonly PaymentService _paymentService;
    private CancellationTokenSource? _cts;
    private bool _isProcessing;

    public sealed record PaymentMethodOption(int Id, string Name);

    public PaymentForm(Invoice invoice) : this(invoice, new PaymentService(new Database()))
    {
    }

    public PaymentForm(Invoice invoice, PaymentService paymentService)
    {
        _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
        _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));

        InitializeComponent();
        ApplyUiTheme();
        PopulateInvoiceInfo();
        PopulatePaymentMethods();
        RegisterEventHandlers();
        ApplyAccessibleNames();
        UpdateButtonState();
    }

    private void ApplyAccessibleNames()
    {
        nudAmount.AccessibleName = "المبلغ المسدد";
        cmbPaymentMethod.AccessibleName = "طريقة الدفع";
        dtpPaymentDate.AccessibleName = "تاريخ الدفع";
        txtReferenceNumber.AccessibleName = "رقم المرجع";
        txtNotes.AccessibleName = "ملاحظات السداد";
        btnPay.AccessibleName = "تسجيل السداد";
        btnCancel.AccessibleName = "إلغاء";
        lblStatus.AccessibleName = "حالة عملية السداد";
    }

    private void UpdateButtonState()
    {
        var validAmount = nudAmount.Value > 0 && nudAmount.Value <= _invoice.BalanceAmount;
        var hasMethod = cmbPaymentMethod.SelectedValue is int methodId && methodId > 0;
        btnPay.Enabled = !_isProcessing && validAmount && hasMethod;
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.StylePrimaryButton(btnPay);
        UiTheme.StyleTertiaryButton(btnCancel);

        lblInvoiceNumVal.ForeColor = UiTheme.TextPrimary;
        lblPaidVal.ForeColor = UiTheme.Success;
        lblBalanceTitle.ForeColor = UiTheme.Danger;
        lblBalanceVal.ForeColor = UiTheme.Danger;
        lblStatusVal.ForeColor = UiTheme.Accent;
    }

    private void PopulateInvoiceInfo()
    {
        lblInvoiceNumVal.Text = _invoice.InvoiceNumber;
        lblPeriodVal.Text = $"{_invoice.BillingYear} / {_invoice.BillingMonth}";
        lblTotalVal.Text = UiText.Currency(_invoice.TotalAmount);
        lblPaidVal.Text = UiText.Currency(_invoice.PaidAmount);
        lblBalanceVal.Text = UiText.Currency(_invoice.BalanceAmount);
        lblStatusVal.Text = string.IsNullOrWhiteSpace(_invoice.StatusName)
            ? (_invoice.Status == 1 ? "غير مسددة" : _invoice.Status == 2 ? "مسددة جزئياً" : _invoice.Status == 3 ? "مسددة بالكامل" : "ملغاة")
            : _invoice.StatusName;

        // Default amount suggestion to balance amount if greater than 0
        nudAmount.Value = _invoice.BalanceAmount > 0 ? _invoice.BalanceAmount : 0.00m;
        dtpPaymentDate.Value = DateTime.Now;
    }

    private void PopulatePaymentMethods()
    {
        var methods = new List<PaymentMethodOption>
        {
            new(1, "نقدي"),
            new(2, "تحويل بنكي"),
            new(3, "طريقة أخرى")
        };

        cmbPaymentMethod.DataSource = methods;
        cmbPaymentMethod.DisplayMember = nameof(PaymentMethodOption.Name);
        cmbPaymentMethod.ValueMember = nameof(PaymentMethodOption.Id);
        cmbPaymentMethod.SelectedIndex = 0;
    }

    private void RegisterEventHandlers()
    {
        btnPay.Click += async (s, e) => await ExecutePaymentAsync();
        btnCancel.Click += (s, e) => { if (!_isProcessing) { DialogResult = DialogResult.Cancel; Close(); } };

        nudAmount.ValueChanged += (s, e) => UpdateButtonState();
        cmbPaymentMethod.SelectedIndexChanged += (s, e) => UpdateButtonState();

        KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5 && !_isProcessing)
            {
                e.Handled = true;
                await ExecutePaymentAsync();
            }
            else if (e.KeyCode == Keys.Escape && !_isProcessing)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        };
    }

    private async Task ExecutePaymentAsync()
    {
        if (_isProcessing) return;

        // 1. Validation (UI only)
        var amount = nudAmount.Value;
        if (amount <= 0)
        {
            ShowValidationError("يرجى إدخال مبلغ سداد صحيح أكبر من الصفر.", nudAmount);
            return;
        }

        if (amount > _invoice.BalanceAmount)
        {
            ShowValidationError("لا يمكن أن يتجاوز مبلغ السداد الرصيد المتبقي للفاتورة.", nudAmount);
            return;
        }

        if (cmbPaymentMethod.SelectedValue is not int paymentMethodId || paymentMethodId <= 0)
        {
            ShowValidationError("يرجى اختيار طريقة الدفع.", cmbPaymentMethod);
            return;
        }

        var refNum = txtReferenceNumber.Text.Trim();
        if (refNum.Length > 100)
        {
            ShowValidationError("رقم المرجع يجب ألا يتجاوز 100 حرف.", txtReferenceNumber);
            return;
        }

        var notes = txtNotes.Text.Trim();
        if (notes.Length > 1000)
        {
            ShowValidationError("الملاحظات يجب ألا تتجاوز 1000 حرف.", txtNotes);
            return;
        }

        var paymentDate = dtpPaymentDate.Value;
        string? referenceNumber = string.IsNullOrWhiteSpace(refNum) ? null : refNum;
        string? paymentNotes = string.IsNullOrWhiteSpace(notes) ? null : notes;

        // 2. State & Token
        SetProcessingState(true);
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            var result = await _paymentService.PayInvoiceAsync(
                invoiceId: _invoice.InvoiceId,
                amount: amount,
                paymentMethodId: paymentMethodId,
                paymentDate: paymentDate,
                referenceNumber: referenceNumber,
                notes: paymentNotes,
                createdBy: null,
                cancellationToken: _cts.Token);

            UiMessages.Information(
                $"تم تسجيل عملية السداد بنجاح.\n" +
                $"رقم الفاتورة: {_invoice.InvoiceNumber}\n" +
                $"المبلغ المسدد: {UiText.Currency(amount)}",
                "نجاح العملية");

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (OperationCanceledException)
        {
            lblStatus.Text = "تم إلغاء عملية السداد.";
        }
        catch (SqlException sqlEx)
        {
            lblStatus.Text = "فشلت عملية السداد.";
            UiMessages.Warning(
                $"حدث خطأ من قاعدة البيانات أثناء تنفيذ السداد:\n{sqlEx.Message}",
                "خطأ في قاعدة البيانات");
        }
        catch (Exception ex)
        {
            lblStatus.Text = "حدث خطأ غير متوقع.";
            UiMessages.Error($"حدث خطأ غير متوقع:\n{ex.Message}");
        }
        finally
        {
            SetProcessingState(false);
        }
    }

    private void ShowValidationError(string message, Control target)
    {
        lblStatus.ForeColor = UiTheme.Danger;
        lblStatus.Text = message;
        target.Focus();
    }

    private void SetProcessingState(bool isProcessing)
    {
        _isProcessing = isProcessing;

        btnCancel.Enabled = !isProcessing;
        nudAmount.Enabled = !isProcessing;
        cmbPaymentMethod.Enabled = !isProcessing;
        dtpPaymentDate.Enabled = !isProcessing;
        txtReferenceNumber.Enabled = !isProcessing;
        txtNotes.Enabled = !isProcessing;

        pbLoading.Visible = isProcessing;
        lblStatus.Text = isProcessing ? "جاري تنفيذ السداد في قاعدة البيانات..." : "جاهز للسداد";
        lblStatus.ForeColor = UiTheme.TextSecondary;
        UpdateButtonState();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isProcessing)
        {
            e.Cancel = true;
            UiMessages.Warning("لا يمكن إغلاق النافذة أثناء تنفيذ عملية السداد.", "تنبيه");
            return;
        }

        _cts?.Cancel();
        _cts?.Dispose();
        base.OnFormClosing(e);
    }
}
