using System.Drawing;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

public partial class AddMeterForm : Form
{
    private readonly Customer? _contextCustomer;
    private readonly MeterService _meterService;
    private readonly CustomerService _customerService;
    private CancellationTokenSource? _cts;
    private bool _isProcessing;
    private bool _suppressComboEvents;
    private int _areaLoadGeneration;
    private bool _referenceDataLoaded;
    private bool _areasLoading;
    private bool _successShown;
    private Customer? _savedCustomer;
    private Panel? _pnlSuccess;
    private Label? _lblSuccessDetails;
    private Button? _btnSuccessViewMeters;
    private Button? _btnSuccessCollection;
    private Button? _btnSuccessClose;

    public AddMeterForm(Customer? customerContext, MeterService meterService)
    {
        _contextCustomer = customerContext;
        _meterService = meterService ?? throw new ArgumentNullException(nameof(meterService));
        _customerService = new CustomerService(new Database());

        InitializeComponent();
        ApplyUiTheme();
        InitializeState();
        RegisterEventHandlers();
        BuildSuccessPanel();
        UpdateSaveState();
    }

    private void ApplyUiTheme()
    {
        lblHeading.ForeColor = UiTheme.TextPrimary;
        reviewPanel.BackColor = UiTheme.SurfaceAlt;
        lblReviewTitle.ForeColor = UiTheme.TextPrimary;
        lblReviewSummary.ForeColor = UiTheme.TextSecondary;
        lblStatus.ForeColor = UiTheme.TextSecondary;
        lblReadingDirectionValue.AccessibleName = "اتجاه القراءة";
        pnlButtons.BackColor = UiTheme.SurfaceAlt;
        UiTheme.StylePrimaryButton(btnSave);
        UiTheme.StyleSecondaryButton(btnCancel);
    }

    private void InitializeState()
    {
        txtCustomerName.ReadOnly = true;
        txtCustomerName.BackColor = SystemColors.Control;

        if (_contextCustomer is not null)
        {
            txtCustomerNumber.Text = _contextCustomer.CustomerNumber;
            txtCustomerName.Text = _contextCustomer.FullName;
            txtCustomerNumber.ReadOnly = true;
            txtCustomerNumber.BackColor = SystemColors.Control;
        }

        txtNotes.MaxLength = 4000;
        dtpInstallationDate.MaxDate = DateTime.Today;
        dtpInstallationDate.Value = DateTime.Today;
        nudInstallationReading.Minimum = 0;
        nudInstallationReading.DecimalPlaces = 3;

        _cts = new CancellationTokenSource();
        UpdateReview();
    }

    private void RegisterEventHandlers()
    {
        cmbBranch.SelectedIndexChanged += async (_, _) =>
        {
            if (_suppressComboEvents)
            {
                return;
            }

            try
            {
                await OnBranchChangedAsync();
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    UiMessages.Error($"حدث خطأ غير متوقع: {ex.Message}");
                }
            }
        };

        cmbMeterType.SelectedIndexChanged += (_, _) =>
        {
            if (_suppressComboEvents)
            {
                return;
            }

            OnMeterTypeChanged();
        };

        cmbArea.SelectedIndexChanged += (_, _) =>
        {
            UpdateReview();
            UpdateSaveState();
        };
        txtCustomerNumber.TextChanged += (_, _) => UpdateReview();
        dtpInstallationDate.ValueChanged += (_, _) => UpdateReview();
        nudInstallationReading.ValueChanged += (_, _) => UpdateReview();
        txtNotes.TextChanged += (_, _) =>
        {
            lblNotesCount.Text = $"{txtNotes.TextLength}/4000";
            UpdateReview();
        };

        btnSave.Click += async (_, _) =>
        {
            if (_isProcessing)
            {
                return;
            }

            await SafeSaveAsync();
        };

        btnCancel.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        KeyDown += OnFormKeyDown;
    }

    private void OnFormKeyDown(object? sender, KeyEventArgs e)
    {
        if (_successShown)
        {
            return;
        }

        if (e.KeyCode == Keys.Escape && !_isProcessing)
        {
            e.Handled = true;
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        if (e.KeyCode != Keys.F5)
        {
            return;
        }

        e.Handled = true;
        if (!_isProcessing)
        {
            _ = SafeSaveAsync();
        }
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (_contextCustomer is not null)
        {
            cmbBranch.Focus();
        }
        else
        {
            txtCustomerNumber.Focus();
        }

        _ = LoadReferenceDataAsync();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _cts?.Cancel();
        base.OnFormClosing(e);
    }

    private async Task LoadReferenceDataAsync()
    {
        lblStatus.Text = "جارٍ تحميل البيانات المرجعية...";
        lblStatus.ForeColor = UiTheme.TextSecondary;
        tspbSaving.Visible = true;

        try
        {
            var token = _cts?.Token ?? CancellationToken.None;
            var branchesTask = _meterService.GetBranchesAsync(token);
            var typesTask = _meterService.GetMeterTypesAsync(token);
            await Task.WhenAll(branchesTask, typesTask);

            var branches = await branchesTask;
            var meterTypes = await typesTask;

            if (IsDisposed)
            {
                return;
            }

            _suppressComboEvents = true;
            try
            {
                cmbBranch.Items.Clear();
                foreach (var branch in branches)
                {
                    cmbBranch.Items.Add(branch);
                }

                if (branches.Count > 0)
                {
                    cmbBranch.SelectedIndex = 0;
                }

                cmbMeterType.Items.Clear();
                foreach (var type in meterTypes)
                {
                    cmbMeterType.Items.Add(type);
                }

                if (meterTypes.Count > 0)
                {
                    cmbMeterType.SelectedIndex = 0;
                }
            }
            finally
            {
                _suppressComboEvents = false;
            }

            _referenceDataLoaded = true;

            if (branches.Count == 0 || meterTypes.Count == 0)
            {
                lblStatus.Text = branches.Count == 0
                    ? "لا توجد فروع نشطة."
                    : "لا توجد أنواع عدادات نشطة.";
                UpdateReview();
                return;
            }

            lblStatus.Text = string.Empty;
            OnMeterTypeChanged();
            await LoadAreasForBranchAsync((cmbBranch.SelectedItem as BranchLookup)?.BranchId);
        }
        catch (OperationCanceledException)
        {
            // Form closed while loading.
        }
        catch (SqlException ex)
        {
            if (!IsDisposed)
            {
                UiMessages.Error($"تعذر تحميل البيانات المرجعية: {ex.Message}", "خطأ في الاتصال بقاعدة البيانات");
            }
        }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                UiMessages.Error($"حدث خطأ غير متوقع أثناء التحميل: {ex.Message}");
            }
        }
        finally
        {
            tspbSaving.Visible = false;
            UpdateSaveState();
        }
    }

    private async Task OnBranchChangedAsync()
    {
        var branchId = (cmbBranch.SelectedItem as BranchLookup)?.BranchId;
        UpdateReview();
        await LoadAreasForBranchAsync(branchId);
    }

    private async Task LoadAreasForBranchAsync(int? branchId)
    {
        var generation = ++_areaLoadGeneration;

        cmbArea.Items.Clear();
        cmbArea.SelectedItem = null;
        _areasLoading = branchId is not null;
        UpdateSaveState();

        if (branchId is null)
        {
            UpdateReview();
            return;
        }

        cmbArea.Enabled = false;
        lblStatus.Text = "جارٍ تحميل المناطق...";
        lblStatus.ForeColor = UiTheme.TextSecondary;
        tspbSaving.Visible = true;

        try
        {
            var token = _cts?.Token ?? CancellationToken.None;
            var areas = await _meterService.GetAreasByBranchAsync(branchId.Value, token);

            if (generation != _areaLoadGeneration || IsDisposed)
            {
                return;
            }

            cmbArea.Items.Clear();
            foreach (var area in areas)
            {
                cmbArea.Items.Add(area);
            }

            if (areas.Count > 0)
            {
                cmbArea.SelectedIndex = 0;
                lblStatus.Text = string.Empty;
                lblStatus.ForeColor = UiTheme.TextSecondary;
            }
            else
            {
                lblStatus.Text = "لا توجد مناطق فعّالة مرتبطة بهذا الفرع.";
                lblStatus.ForeColor = UiTheme.Danger;
            }
        }
        catch (OperationCanceledException)
        {
            // A newer branch selection replaced this load, or the form closed.
        }
        catch (SqlException ex)
        {
            if (generation == _areaLoadGeneration && !IsDisposed)
            {
                UiMessages.Error($"تعذر تحميل المناطق: {ex.Message}", "خطأ في الاتصال بقاعدة البيانات");
            }
        }
        catch (Exception ex)
        {
            if (generation == _areaLoadGeneration && !IsDisposed)
            {
                UiMessages.Error($"حدث خطأ غير متوقع: {ex.Message}");
            }
        }
        finally
        {
            if (generation == _areaLoadGeneration && !IsDisposed)
            {
                cmbArea.Enabled = true;
                tspbSaving.Visible = false;
                _areasLoading = false;
                UpdateSaveState();
            }
        }
    }

    private void OnMeterTypeChanged()
    {
        var direction = (cmbMeterType.SelectedItem as MeterTypeLookup)?.ReadingDirection;
        lblReadingDirectionValue.Text = DirectionName(direction);
        UpdateReview();
    }

    private static string DirectionName(byte? direction) => direction switch
    {
        1 => "تصاعدي",
        2 => "تنازلي",
        _ => "—"
    };

    private async Task SafeSaveAsync()
    {
        try
        {
            await SaveAsync();
        }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                UiMessages.Error($"حدث خطأ غير متوقع أثناء الحفظ: {ex.Message}");
            }
        }
    }

    private async Task SaveAsync()
    {
        if (_isProcessing)
        {
            return;
        }

        SetProcessing(true);

        try
        {
            var customer = _contextCustomer;
            if (customer is null)
            {
                var customerNumber = txtCustomerNumber.Text.Trim();
                if (string.IsNullOrWhiteSpace(customerNumber))
                {
                    UiMessages.Warning("يرجى إدخال رقم العميل.", "بيانات ناقصة");
                    return;
                }

                Customer? resolved;
                try
                {
                    resolved = await _customerService.GetByCustomerNumberAsync(customerNumber, _cts?.Token ?? CancellationToken.None);
                }
                catch (SqlException ex)
                {
                    UiMessages.Error($"تعذر جلب بيانات العميل: {ex.Message}", "خطأ في الاتصال بقاعدة البيانات");
                    return;
                }

                if (resolved is null)
                {
                    UiMessages.Error($"لم يتم العثور على عميل بالرقم: {customerNumber}");
                    return;
                }

                customer = resolved;
                txtCustomerName.Text = customer.FullName;
            }

            if (!(cmbBranch.SelectedItem is BranchLookup branch))
            {
                UiMessages.Warning("يرجى اختيار الفرع.", "بيانات ناقصة");
                return;
            }

            if (!(cmbArea.SelectedItem is AreaLookup area))
            {
                UiMessages.Warning("يرجى اختيار المنطقة.", "بيانات ناقصة");
                return;
            }

            if (!(cmbMeterType.SelectedItem is MeterTypeLookup meterType))
            {
                UiMessages.Warning("يرجى اختيار نوع العداد.", "بيانات ناقصة");
                return;
            }

            var notes = txtNotes.Text.Trim();
            if (notes.Length > 4000)
            {
                UiMessages.Warning("الملاحظات تتجاوز الحد المسموح (4000 حرف).", "بيانات غير صالحة");
                return;
            }

            lblStatus.Text = "جارٍ حفظ العداد...";
            lblStatus.ForeColor = UiTheme.TextSecondary;

            StoredProcedureExecutionResult result;
            try
            {
                result = await _meterService.AddMeterAsync(
                    customer.CustomerId,
                    branch.BranchId,
                    area.AreaId,
                    meterType.MeterTypeId,
                    DateOnly.FromDateTime(dtpInstallationDate.Value),
                    Math.Round(nudInstallationReading.Value, 3),
                    string.IsNullOrWhiteSpace(notes) ? null : notes,
                    createdBy: null,
                    cancellationToken: _cts?.Token ?? CancellationToken.None);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (SqlException ex)
            {
                UiMessages.Error($"تعذر حفظ العداد: {ex.Message}", "خطأ في الاتصال بقاعدة البيانات");
                return;
            }

            var meterId = result.GetOutputValue<int>("@MeterId");
            var meterNumber = result.GetOutputValue<long>("@MeterNumber");

            if (meterNumber <= 0)
            {
                UiMessages.Warning(
                    "تم تنفيذ العملية، لكن لا يمكن التأكد من رقم العداد. يرجى مراجعة سجل العدادات للتأكد.",
                    "يحتاج إلى تأكيد");
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            ShowSuccess(meterId, meterNumber, customer, branch, area, meterType);
        }
        finally
        {
            if (!IsDisposed)
            {
                SetProcessing(false);
            }
        }
    }

    private void UpdateReview()
    {
        if (lblReviewSummary is null)
        {
            return;
        }

        var customer = _contextCustomer is not null
            ? $"{_contextCustomer.CustomerNumber} - {_contextCustomer.FullName}"
            : string.IsNullOrWhiteSpace(txtCustomerNumber.Text) ? "—" : txtCustomerNumber.Text.Trim();

        var branch = (cmbBranch.SelectedItem as BranchLookup) is { } b ? b.ToString() : "—";
        var area = (cmbArea.SelectedItem as AreaLookup) is { } a ? a.ToString() : "—";
        var type = (cmbMeterType.SelectedItem as MeterTypeLookup) is { } t ? t.ToString() : "—";
        var direction = DirectionName((cmbMeterType.SelectedItem as MeterTypeLookup)?.ReadingDirection);

        lblReviewSummary.Text = string.Join(Environment.NewLine,
            $"العميل: {customer}",
            $"الفرع: {branch}    |    المنطقة: {area}",
            $"نوع العداد: {type}    |    الاتجاه: {direction}",
            $"تاريخ التركيب: {UiText.Date(DateOnly.FromDateTime(dtpInstallationDate.Value))}    |    قراءة التركيب: {UiText.Amount3(nudInstallationReading.Value)}",
            $"الملاحظات: {(string.IsNullOrWhiteSpace(txtNotes.Text) ? "—" : txtNotes.Text)}");
    }

    private void SetProcessing(bool isBusy)
    {
        _isProcessing = isBusy;
        cmbBranch.Enabled = !isBusy;
        cmbArea.Enabled = !isBusy;
        cmbMeterType.Enabled = !isBusy;
        txtCustomerNumber.Enabled = !isBusy;
        txtNotes.Enabled = !isBusy;
        dtpInstallationDate.Enabled = !isBusy;
        nudInstallationReading.Enabled = !isBusy;
        btnCancel.Enabled = !isBusy;
        tspbSaving.Visible = isBusy;

        if (isBusy)
        {
            lblStatus.Text = "جارٍ حفظ العداد...";
            lblStatus.ForeColor = UiTheme.TextSecondary;
        }
        else
        {
            lblStatus.Text = string.Empty;
            RestoreAreaStatus();
        }

        UpdateSaveState();
    }

    private void RestoreAreaStatus()
    {
        if (_successShown || _areasLoading)
        {
            return;
        }

        if (cmbBranch.SelectedItem is BranchLookup && cmbArea.Items.Count == 0)
        {
            lblStatus.Text = "لا توجد مناطق فعّالة مرتبطة بهذا الفرع.";
            lblStatus.ForeColor = UiTheme.Danger;
        }
    }

    private void UpdateSaveState()
    {
        if (_successShown || _isProcessing || !_referenceDataLoaded || _areasLoading)
        {
            btnSave.Enabled = false;
            return;
        }

        btnSave.Enabled = cmbBranch.SelectedItem is BranchLookup
            && cmbArea.SelectedItem is AreaLookup
            && cmbMeterType.SelectedItem is MeterTypeLookup;
    }

    private void BuildSuccessPanel()
    {
        var flow = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 56,
            Padding = new Padding(0, 8, 0, 0),
            RightToLeft = RightToLeft.Yes,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };

        _btnSuccessClose = new Button { Text = "إغلاق", AutoSize = false, Size = new Size(110, 36), Margin = new Padding(6) };
        UiTheme.StyleSecondaryButton(_btnSuccessClose);
        _btnSuccessClose.Click += (_, _) =>
        {
            DialogResult = DialogResult.OK;
            Close();
        };

        _btnSuccessCollection = new Button { Text = "فتح شاشة التحصيل", AutoSize = false, Size = new Size(160, 36), Margin = new Padding(6) };
        UiTheme.StylePrimaryButton(_btnSuccessCollection);
        _btnSuccessCollection.Click += async (_, _) => await OpenCollectionAsync();

        _btnSuccessViewMeters = new Button { Text = "عرض العدادات", AutoSize = false, Size = new Size(140, 36), Margin = new Padding(6) };
        UiTheme.StyleSecondaryButton(_btnSuccessViewMeters);
        _btnSuccessViewMeters.Click += (_, _) => OpenMeters();

        flow.Controls.Add(_btnSuccessViewMeters);
        flow.Controls.Add(_btnSuccessCollection);
        flow.Controls.Add(_btnSuccessClose);

        _lblSuccessDetails = new Label
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = UiTheme.BodyFont(10F),
            ForeColor = UiTheme.TextSecondary,
            BackColor = UiTheme.SurfaceAlt,
            Padding = new Padding(24)
        };

        var title = new Label
        {
            Dock = DockStyle.Top,
            Height = 90,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = UiTheme.TitleFont(16F),
            ForeColor = UiTheme.Success,
            Text = "تم حفظ العداد بنجاح"
        };

        _pnlSuccess = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = UiTheme.Surface,
            Padding = new Padding(32),
            Visible = false,
            RightToLeft = RightToLeft.Yes
        };

        _pnlSuccess.Controls.Add(_lblSuccessDetails);
        _pnlSuccess.Controls.Add(title);
        _pnlSuccess.Controls.Add(flow);

        Controls.Add(_pnlSuccess);
    }

    private void ShowSuccess(int meterId, long meterNumber, Customer customer, BranchLookup branch, AreaLookup area, MeterTypeLookup meterType)
    {
        _successShown = true;
        _savedCustomer = customer;

        fieldsPanel.Visible = false;
        reviewPanel.Visible = false;
        pnlButtons.Visible = false;

        _lblSuccessDetails!.Text = string.Join(Environment.NewLine,
            $"رقم العداد: {meterNumber:N0}",
            $"معرف العداد: {meterId:N0}",
            $"العميل: {customer.CustomerNumber} — {customer.FullName}",
            $"الفرع: {branch}    |    المنطقة: {area}",
            $"نوع العداد: {meterType}    |    الاتجاه: {DirectionName(meterType.ReadingDirection)}",
            $"تاريخ التركيب: {UiText.Date(DateOnly.FromDateTime(dtpInstallationDate.Value))}    |    قراءة التركيب: {UiText.Amount3(nudInstallationReading.Value)}");

        _pnlSuccess!.Visible = true;
        _pnlSuccess.BringToFront();

        AcceptButton = _btnSuccessClose;
        CancelButton = _btnSuccessClose;
        _btnSuccessClose!.Focus();

        UpdateSaveState();
    }

    private void OpenMeters()
    {
        using var metersForm = new MetersManagementForm(_meterService);
        metersForm.ExitRequested += () => metersForm.Close();
        metersForm.ShowDialog(this);
    }

    private async Task OpenCollectionAsync()
    {
        if (_savedCustomer is null)
        {
            return;
        }

        try
        {
            using var collection = new FieldCollectionForm();
            await collection.LoadByCustomerIdAsync(_savedCustomer.CustomerId, _cts?.Token ?? CancellationToken.None);

            if (IsDisposed)
            {
                return;
            }

            collection.ShowDialog(this);
        }
        catch (OperationCanceledException)
        {
            // Form closed while loading.
        }
        catch (SqlException ex)
        {
            if (!IsDisposed)
            {
                UiMessages.Error($"تعذر فتح شاشة التحصيل: {ex.Message}", "خطأ في الاتصال بقاعدة البيانات");
            }
        }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                UiMessages.Error($"حدث خطأ غير متوقع: {ex.Message}");
            }
        }
    }
}