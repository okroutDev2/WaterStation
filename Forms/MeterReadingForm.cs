using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

public partial class MeterReadingForm : Form
{
    private readonly Meter _meter;
    private readonly MeterService _meterService;
    private CancellationTokenSource? _cts;
    private bool _isProcessing;

    public StoredProcedureExecutionResult? ExecutionResult { get; private set; }

    public MeterReadingForm(Meter meter) : this(meter, new MeterService(new Database()))
    {
    }

    public MeterReadingForm(Meter meter, MeterService meterService)
    {
        _meter = meter ?? throw new ArgumentNullException(nameof(meter));
        _meterService = meterService ?? throw new ArgumentNullException(nameof(meterService));

        InitializeComponent();
        ApplyUiTheme();
        PopulateMeterInfo();
        RegisterEventHandlers();
        ApplyAccessibleNames();
    }

    private void ApplyAccessibleNames()
    {
        dtpReadingDate.AccessibleName = "تاريخ القراءة";
        nudReadingValue.AccessibleName = "قيمة القراءة";
        txtNotes.AccessibleName = "ملاحظات القراءة";
        btnSave.AccessibleName = "حفظ القراءة";
        btnCancel.AccessibleName = "إلغاء";
        lblStatus.AccessibleName = "حالة حفظ القراءة";
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.StylePrimaryButton(btnSave);
        UiTheme.StyleTertiaryButton(btnCancel);
    }

    private void PopulateMeterInfo()
    {
        lblMeterNumVal.Text = _meter.MeterNumber;
        lblCustomerNumVal.Text = string.IsNullOrWhiteSpace(_meter.CustomerNumber) ? "—" : _meter.CustomerNumber;
        lblCustomerNameVal.Text = string.IsNullOrWhiteSpace(_meter.FullName) ? "—" : _meter.FullName;
        lblBranchVal.Text = string.IsNullOrWhiteSpace(_meter.BranchName) ? "—" : _meter.BranchName;
        lblAreaVal.Text = string.IsNullOrWhiteSpace(_meter.AreaName) ? "—" : _meter.AreaName;
        lblMeterTypeVal.Text = string.IsNullOrWhiteSpace(_meter.MeterTypeName) ? "—" : _meter.MeterTypeName;

        lblReadingDirectionVal.Text = !string.IsNullOrWhiteSpace(_meter.ReadingDirectionName)
            ? _meter.ReadingDirectionName
            : "—";

        lblInstallationReadingVal.Text = _meter.InstallationReading.ToString("N3");
        lblLastReadingDateVal.Text = _meter.LastReadingDate.HasValue ? _meter.LastReadingDate.Value.ToString("yyyy-MM-dd") : "—";
        lblLastReadingValueVal.Text = _meter.LastReadingValue.HasValue ? _meter.LastReadingValue.Value.ToString("N3") : "—";
        lblLastConsumptionVal.Text = _meter.LastConsumption.HasValue ? _meter.LastConsumption.Value.ToString("N3") : "—";
        lblLastIsReverseVal.Text = _meter.LastIsReverseMeter.HasValue ? (_meter.LastIsReverseMeter.Value ? "نعم" : "لا") : "—";
        lblLastIsReverseVal.ForeColor = _meter.LastIsReverseMeter is true ? UiTheme.Danger : UiTheme.Success;

        dtpReadingDate.Value = DateTime.Today;
    }

    private void RegisterEventHandlers()
    {
        btnSave.Click += async (s, e) => await SaveReadingAsync();
        btnCancel.Click += (s, e) => { if (!_isProcessing) { DialogResult = DialogResult.Cancel; Close(); } };

        txtNotes.TextChanged += (s, e) =>
        {
            var length = txtNotes.Text.Length;
            lblNotesCount.Text = $"{length} / 2000";
            lblNotesCount.ForeColor = length >= 2000 ? UiTheme.Danger : UiTheme.TextSecondary;
        };

        KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5 && !_isProcessing)
            {
                e.Handled = true;
                await SaveReadingAsync();
            }
            else if (e.KeyCode == Keys.Escape && !_isProcessing)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        };
    }

    private async Task SaveReadingAsync()
    {
        if (_isProcessing) return;

        var readingDate = dtpReadingDate.Value;
        if (readingDate == DateTime.MinValue)
        {
            ShowValidationError("يرجى تحديد تاريخ قراءة صالح.", dtpReadingDate);
            return;
        }

        var readingValue = nudReadingValue.Value;
        if (readingValue < 0)
        {
            ShowValidationError("قيمة القراءة يجب أن تكون أكبر من أو تساوي الصفر.", nudReadingValue);
            return;
        }

        var notes = txtNotes.Text.Trim();
        if (notes.Length > 2000)
        {
            ShowValidationError("الملاحظات يجب ألا تتجاوز 2000 حرف.", txtNotes);
            return;
        }

        var noteValue = string.IsNullOrWhiteSpace(notes) ? null : notes;

        SetProcessingState(true);
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            var result = await _meterService.CreateMeterReadingAsync(
                meterId: _meter.MeterId,
                readingDate: DateOnly.FromDateTime(readingDate),
                readingValue: readingValue,
                notes: noteValue,
                createdBy: null,
                cancellationToken: _cts.Token);

            ExecutionResult = result;

            UiMessages.Information(
                $"تم حفظ قراءة العداد بنجاح.\nالعداد: {_meter.MeterNumber}\nقيمة القراءة: {readingValue:N3}",
                "نجاح العملية");

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (SqlException sqlEx)
        {
            lblStatus.Text = "فشل حفظ القراءة.";
            UiMessages.Warning(
                $"حدث خطأ من قاعدة البيانات أثناء حفظ القراءة.\nErrorNumber: {sqlEx.Number}\n{sqlEx.Message}",
                "خطأ في قاعدة البيانات");
        }
        catch (OperationCanceledException)
        {
            lblStatus.Text = "تم إلغاء عملية الحفظ.";
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

        btnSave.Enabled = !isProcessing;
        btnCancel.Enabled = !isProcessing;
        dtpReadingDate.Enabled = !isProcessing;
        nudReadingValue.Enabled = !isProcessing;
        txtNotes.Enabled = !isProcessing;

        pbLoading.Visible = isProcessing;
        lblStatus.Text = isProcessing ? "جاري حفظ قراءة العداد في قاعدة البيانات..." : "جاهز لإدخال القراءة";
        lblStatus.ForeColor = UiTheme.TextSecondary;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isProcessing)
        {
            e.Cancel = true;
            UiMessages.Warning("لا يمكن إغلاق النافذة أثناء حفظ القراءة.", "تنبيه");
            return;
        }

        _cts?.Cancel();
        _cts?.Dispose();
        base.OnFormClosing(e);
    }
}