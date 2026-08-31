using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

public partial class ReversePaymentForm : Form
{
    private readonly Payment _payment;
    private readonly PaymentService _paymentService;
    private CancellationTokenSource? _cts;
    private bool _isProcessing;

    public StoredProcedureExecutionResult? ExecutionResult { get; private set; }

    public ReversePaymentForm(Payment payment) : this(payment, new PaymentService(new Database()))
    {
    }

    public ReversePaymentForm(Payment payment, PaymentService paymentService)
    {
        _payment = payment ?? throw new ArgumentNullException(nameof(payment));
        _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));

        if (payment.IsReversed)
        {
            throw new InvalidOperationException("لا يمكن عكس دفعة معكوسة مسبقًا.");
        }

        InitializeComponent();
        ApplyUiTheme();
        PopulatePaymentInfo();
        RegisterEventHandlers();
        ApplyAccessibleNames();
        UpdateButtonState();
    }

    private void ApplyAccessibleNames()
    {
        txtReason.AccessibleName = "سبب عكس الدفعة";
        btnConfirm.AccessibleName = "تأكيد عكس الدفعة";
        btnCancel.AccessibleName = "إلغاء";
        lblStatus.AccessibleName = "حالة عملية العكس";
    }

    private void UpdateButtonState()
    {
        btnConfirm.Enabled = !_isProcessing && !string.IsNullOrWhiteSpace(txtReason.Text);
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.StyleDangerButton(btnConfirm);
        UiTheme.StyleTertiaryButton(btnCancel);

        grpWarning.ForeColor = UiTheme.Danger;
        lblWarning.ForeColor = UiTheme.Danger;
    }

    private void PopulatePaymentInfo()
    {
        lblPaymentIdVal.Text = _payment.PaymentId.ToString();
        lblInvoiceNumVal.Text = string.IsNullOrWhiteSpace(_payment.InvoiceNumber) ? "—" : _payment.InvoiceNumber;
        lblPaymentDateVal.Text = _payment.PaymentDate.ToString("yyyy-MM-dd HH:mm");
        lblPaymentAmountVal.Text = UiText.Currency(_payment.Amount);
        lblPaymentMethodVal.Text = string.IsNullOrWhiteSpace(_payment.PaymentMethodName) ? "—" : _payment.PaymentMethodName;
        lblRefNumVal.Text = string.IsNullOrWhiteSpace(_payment.ReferenceNumber) ? "—" : _payment.ReferenceNumber;
        lblIsReversedVal.Text = _payment.IsReversed ? "معكوسة" : "غير معكوسة";
        lblIsReversedVal.ForeColor = _payment.IsReversed ? UiTheme.Danger : UiTheme.Success;
        lblIsReversedVal.Font = new Font(UiTheme.DefaultFontFamily, 10.5F, FontStyle.Bold);
    }

    private void RegisterEventHandlers()
    {
        btnConfirm.Click += async (s, e) => await ExecuteReversalAsync();
        btnCancel.Click += (s, e) => { if (!_isProcessing) { DialogResult = DialogResult.Cancel; Close(); } };

        txtReason.TextChanged += (s, e) =>
        {
            var length = txtReason.Text.Length;
            lblCharCount.Text = $"{length} / 1000";
            lblCharCount.ForeColor = length >= 1000 ? UiTheme.Danger : UiTheme.TextSecondary;
            UpdateButtonState();
        };

        KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5 && !_isProcessing)
            {
                e.Handled = true;
                await ExecuteReversalAsync();
            }
            else if (e.KeyCode == Keys.Escape && !_isProcessing)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        };
    }

    private async Task ExecuteReversalAsync()
    {
        if (_isProcessing) return;

        // Local validation only: the reason is required. Do NOT re-implement the
        // reversal rules that already exist in the Billing.ReversePayment stored procedure.
        var reason = txtReason.Text.Trim();
        if (string.IsNullOrWhiteSpace(reason))
        {
            lblStatus.ForeColor = UiTheme.Danger;
            lblStatus.Text = "يرجى إدخال سبب عكس الدفعة قبل المتابعة (الحقل إلزامي).";
            txtReason.Focus();
            return;
        }

        // Second confirmation before the real execution
        var confirmation = UiMessages.Confirm(
            "تأكيد نهائي: سيتم عكس الدفعة المحددة بالكامل، وسيتم إعادة احتساب حالة الفاتورة ورصيدها.\n" +
            "لا يمكن تنفيذ العكس مرة ثانية لنفس الدفعة بعد نجاح العملية.\n\n" +
            "هل تريد المتابعة؟",
            "تأكيد عكس الدفعة");

        if (!confirmation)
        {
            return;
        }

        SetProcessingState(true);
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            var result = await _paymentService.ReversePaymentAsync(
                paymentId: _payment.PaymentId,
                reason: reason,
                createdBy: null,
                cancellationToken: _cts.Token);

            ExecutionResult = result;

            UiMessages.Information(
                $"تم عكس الدفعة بنجاح.\nرقم الدفعة: {_payment.PaymentId}\nالمبلغ المعكوس: {_payment.Amount:N2} ر.س",
                "نجاح العملية");

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (OperationCanceledException)
        {
            lblStatus.Text = "تم إلغاء عملية العكس.";
        }
        catch (SqlException sqlEx)
        {
            lblStatus.Text = "فشلت عملية العكس.";
            UiMessages.Warning(
                $"حدث خطأ من قاعدة البيانات أثناء تنفيذ العكس:\n{sqlEx.Message}",
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

    private void SetProcessingState(bool isProcessing)
    {
        _isProcessing = isProcessing;

        btnCancel.Enabled = !isProcessing;
        txtReason.Enabled = !isProcessing;

        pbLoading.Visible = isProcessing;
        lblStatus.Text = isProcessing ? "جاري تنفيذ العكس في قاعدة البيانات..." : "جاهز";
        lblStatus.ForeColor = UiTheme.TextSecondary;
        UpdateButtonState();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isProcessing)
        {
            e.Cancel = true;
            UiMessages.Warning("لا يمكن إغلاق النافذة أثناء تنفيذ عملية العكس.", "تنبيه");
            return;
        }

        _cts?.Cancel();
        _cts?.Dispose();
        base.OnFormClosing(e);
    }
}