using Microsoft.Data.SqlClient;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

/// <summary>
/// Meters management screen (read-only listing).
/// Provides shortcuts to enter a reading on the selected meter (MeterReadingForm)
/// and to open the customer's collection screen (FieldCollectionForm).
/// Meter add/edit/delete are intentionally absent: no safe backend procedure exists
/// for them beyond Core.AddMeter, which requires an explicit entry workflow.
/// </summary>
public sealed partial class MetersManagementForm : Form
{
    private readonly MeterService _meterService;

    private CancellationTokenSource? _cts;
    private bool _isSearching;
    private UiBusy? _busy;
    private int _colCustomer = -1;
    private int _colLastReading = -1;
    private int _colLastReadingDate = -1;
    private int _colStatus = -1;
    private IReadOnlyList<Meter> _allMeters = Array.Empty<Meter>();
    private EmptyPanel? _emptyPanel;

    /// <summary>Raised (on the UI thread) when the user asks to leave this screen.</summary>
    public event Action? ExitRequested;

    public MetersManagementForm(MeterService meterService)
    {
        _meterService = meterService ?? throw new ArgumentNullException(nameof(meterService));
        InitializeComponent();
        ConfigureGrid();
        RegisterEventHandlers();
        ApplyUiTheme();
        UpdateActionButtons();
        tslblStatus.Text = "جاهز";
    }

    private void ConfigureGrid()
    {
        dgvMeters.Columns.Clear();
        dgvMeters.Columns.Add(Column(nameof(Meter.MeterNumber), "رقم العداد", 90));
        var customerColumn = new DataGridViewTextBoxColumn
        {
            Name = "CustomerDisplay",
            HeaderText = "العميل",
            FillWeight = 180,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        };
        dgvMeters.Columns.Add(customerColumn);
        dgvMeters.Columns.Add(Column(nameof(Meter.BranchName), "الفرع", 100));
        dgvMeters.Columns.Add(Column(nameof(Meter.AreaName), "المنطقة", 100));
        dgvMeters.Columns.Add(Column(nameof(Meter.MeterTypeName), "نوع العداد", 90));
        dgvMeters.Columns.Add(Column(nameof(Meter.LastReadingValue), "آخر قراءة", 80, "N3"));
        dgvMeters.Columns.Add(Column(nameof(Meter.LastReadingDate), "تاريخ آخر قراءة", 100, "yyyy-MM-dd"));
        dgvMeters.Columns.Add(Column(nameof(Meter.Status), "الحالة", 60));

        _colCustomer = dgvMeters.Columns["CustomerDisplay"].Index;
        _colLastReading = dgvMeters.Columns[nameof(Meter.LastReadingValue)].Index;
        _colLastReadingDate = dgvMeters.Columns[nameof(Meter.LastReadingDate)].Index;
        _colStatus = dgvMeters.Columns[nameof(Meter.Status)].Index;
        dgvMeters.CellFormatting += ApplyCellFormatting;
    }

    private void RegisterEventHandlers()
    {
        btnSearch.Click += async (s, e) => await PerformSearchAsync();
        btnRefresh.Click += async (s, e) => await PerformSearchAsync();
        btnClear.Click += (s, e) =>
        {
            txtSearch.Clear();
            ResetFilterCombos();
            ApplyFilters();
        };
        btnReading.Click += async (s, e) => await OpenReadingForSelectedAsync();
        btnCollection.Click += async (s, e) => await OpenCollectionForSelectedAsync();
        btnAddMeter.Click += (s, e) => OpenAddMeter();
        btnExit.Click += (s, e) => ExitRequested?.Invoke();

        txtSearch.KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await PerformSearchAsync();
            }
        };

        cmbBranch.SelectedIndexChanged += (s, e) => ApplyFilters();
        cmbArea.SelectedIndexChanged += (s, e) => ApplyFilters();
        cmbMeterType.SelectedIndexChanged += (s, e) => ApplyFilters();

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

        dgvMeters.SelectionChanged += (s, e) => UpdateActionButtons();
        dgvMeters.DataSourceChanged += (s, e) => UpdateActionButtons();

        Shown += (s, e) => _ = PerformSearchAsync();
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.ApplyGridDefaults(dgvMeters);
        UiTheme.StyleTertiaryButton(btnSearch);
        UiTheme.StyleTertiaryButton(btnClear);
        UiTheme.StyleTertiaryButton(btnRefresh);
        UiTheme.StylePrimaryButton(btnAddMeter);
        UiTheme.StylePrimaryButton(btnReading);
        UiTheme.StyleSecondaryButton(btnCollection);
        UiTheme.StyleTertiaryButton(btnExit);

        txtSearch.AccessibleName = "البحث في العدادات";
        btnSearch.AccessibleName = "بحث في العدادات";
        btnClear.AccessibleName = "مسح";
        btnRefresh.AccessibleName = "تحديث";
        btnAddMeter.AccessibleName = "إضافة عداد جديد";
        btnReading.AccessibleName = "إدخال قراءة للعداد المحدد";
        btnCollection.AccessibleName = "فتح شاشة تحصيل العميل";
        btnExit.AccessibleName = "خروج";
        cmbBranch.AccessibleName = "تصفية حسب الفرع";
        cmbArea.AccessibleName = "تصفية حسب المنطقة";
        cmbMeterType.AccessibleName = "تصفية حسب نوع العداد";
        dgvMeters.AccessibleName = "جدول العدادات";

        _emptyPanel = UiTheme.CreateEmptyPanel(
            UiTheme.EmptyMeters,
            actionText: "إضافة عداد",
            action: OpenAddMeter);
        _emptyPanel.Visible = false;
        Controls.Add(_emptyPanel);
        Controls.SetChildIndex(_emptyPanel, 0);
        _emptyPanel.BringToFront();
    }

    private static DataGridViewTextBoxColumn Column(string property, string header, int weight, string? format = null)
    {
        var column = new DataGridViewTextBoxColumn
        {
            Name = property,
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = weight,
            DefaultCellStyle = { Alignment = format is null || format == "0" || format.StartsWith("yyyy-MM-dd", StringComparison.Ordinal)
                ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleRight }
        };

        if (format is not null)
        {
            column.DefaultCellStyle.Format = format;
        }

        return column;
    }

    private async Task PerformSearchAsync()
    {
        if (_isSearching)
        {
            return;
        }

        _busy ??= new UiBusy(
            new Control[] { btnSearch, btnClear, btnRefresh, btnAddMeter, btnReading, btnCollection, btnExit, txtSearch, cmbBranch, cmbArea, cmbMeterType },
            pbLoading,
            message => tslblStatus.Text = message,
            "جاهز");
        _busy.Begin(UiTheme.StatusLoading);

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        _isSearching = true;
        try
        {
            var meters = await _meterService.GetActiveMetersAsync(token);
            if (token.IsCancellationRequested)
            {
                return;
            }

            _allMeters = meters;
            RepopulateFilterCombos();
            ApplyFilters();
        }
        catch (OperationCanceledException)
        {
        }
        catch (SqlException sqlEx)
        {
            dgvMeters.DataSource = null;
            tslblCount.Text = string.Empty;
            ShowEmptyState(false);
            _busy.End(UiTheme.StatusError);
            UiMessages.Warning($"حدث خطأ من قاعدة البيانات أثناء تحميل العدادات:\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
        }
        catch (Exception ex)
        {
            dgvMeters.DataSource = null;
            tslblCount.Text = string.Empty;
            ShowEmptyState(false);
            _busy.End(UiTheme.StatusError);
            UiMessages.Error($"حدث خطأ غير متوقع أثناء تحميل العدادات:\n{ex.Message}");
        }
        finally
        {
            _isSearching = false;
            UpdateActionButtons();
        }
    }

    private void RepopulateFilterCombos()
    {
        var branch = BranchFilter;
        var area = AreaFilter;
        var type = MeterTypeFilter;

        var branches = _allMeters.Select(m => m.BranchName).Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => v!).Distinct().OrderBy(v => v, StringComparer.CurrentCulture).ToList();
        var areas = _allMeters.Select(m => m.AreaName).Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => v!).Distinct().OrderBy(v => v, StringComparer.CurrentCulture).ToList();
        var types = _allMeters.Select(m => m.MeterTypeName).Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => v!).Distinct().OrderBy(v => v, StringComparer.CurrentCulture).ToList();

        FillFilter(cmbBranch, branches, branch);
        FillFilter(cmbArea, areas, area);
        FillFilter(cmbMeterType, types, type);
    }

    private static void FillFilter(ComboBox combo, List<string> values, string? preserved)
    {
        combo.BeginUpdate();
        try
        {
            combo.Items.Clear();
            combo.Items.Add(AllFilterText);
            foreach (var value in values)
            {
                combo.Items.Add(value);
            }

            var index = combo.Items.IndexOf(preserved is null ? AllFilterText : preserved);
            combo.SelectedIndex = index >= 0 ? index : 0;
        }
        finally
        {
            combo.EndUpdate();
        }
    }

    private void ResetFilterCombos()
    {
        if (cmbBranch.Items.Count > 0) cmbBranch.SelectedIndex = 0;
        if (cmbArea.Items.Count > 0) cmbArea.SelectedIndex = 0;
        if (cmbMeterType.Items.Count > 0) cmbMeterType.SelectedIndex = 0;
    }

    private void ApplyFilters()
    {
        var filter = txtSearch.Text.Trim();
        var branch = BranchFilter;
        var area = AreaFilter;
        var type = MeterTypeFilter;

        var filtered = _allMeters
            .Where(m => string.IsNullOrWhiteSpace(filter)
                        || Contains(m.MeterNumber, filter)
                        || Contains(m.CustomerNumber, filter)
                        || Contains(m.FullName, filter))
            .Where(m => branch is null or "" || string.Equals(m.BranchName, branch, StringComparison.Ordinal))
            .Where(m => area is null or "" || string.Equals(m.AreaName, area, StringComparison.Ordinal))
            .Where(m => type is null or "" || string.Equals(m.MeterTypeName, type, StringComparison.Ordinal))
            .ToList();

        if (filtered.Count > 0)
        {
            dgvMeters.DataSource = filtered.ToList();
            ShowEmptyState(false);
        }
        else
        {
            dgvMeters.DataSource = null;
            ShowEmptyState(true);
        }

        tslblCount.Text = filtered.Count == 1 ? "عداد واحد" : $"{filtered.Count} عدادات";
        _busy?.End();
        UpdateActionButtons();
    }

    private void ShowEmptyState(bool show)
    {
        if (_emptyPanel is null)
        {
            return;
        }

        if (show)
        {
            var hasFilter = !string.IsNullOrWhiteSpace(txtSearch.Text)
                            || (cmbBranch.SelectedItem as string is not null && cmbBranch.SelectedItem as string != AllFilterText)
                            || (cmbArea.SelectedItem as string is not null && cmbArea.SelectedItem as string != AllFilterText)
                            || (cmbMeterType.SelectedItem as string is not null && cmbMeterType.SelectedItem as string != AllFilterText);
            _emptyPanel.SetMessage(hasFilter ? UiTheme.EmptyMetersSearch : UiTheme.EmptyMeters);
            _emptyPanel.Visible = true;
            _emptyPanel.BringToFront();
        }
        else
        {
            _emptyPanel.Visible = false;
        }
    }

    private static readonly string AllFilterText = "الكل";

    private string? BranchFilter => cmbBranch.SelectedItem as string != AllFilterText ? cmbBranch.SelectedItem as string : null;
    private string? AreaFilter => cmbArea.SelectedItem as string != AllFilterText ? cmbArea.SelectedItem as string : null;
    private string? MeterTypeFilter => cmbMeterType.SelectedItem as string != AllFilterText ? cmbMeterType.SelectedItem as string : null;

    private Meter? SelectedMeter() =>
        dgvMeters.CurrentRow?.DataBoundItem as Meter;

    private void UpdateActionButtons()
    {
        var meter = SelectedMeter();
        var canAct = meter is not null && meter.Status == 1;
        btnReading.Enabled = canAct;
        btnCollection.Enabled = meter is not null;
        btnAddMeter.Enabled = true;
    }

    private void OpenAddMeter()
    {
        using var dialog = new AddMeterForm(null, _meterService);
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            tslblStatus.Text = UiTheme.StatusSaved;
            _ = PerformSearchAsync();
        }
    }

    private async Task OpenReadingForSelectedAsync()
    {
        var meter = SelectedMeter();
        if (meter is null || meter.Status != 1)
        {
            UiMessages.Warning("يرجى تحديد عداد نشط أولاً لإدخال قراءة.", "لم يتم تحديد عداد");
            return;
        }

        using var readingForm = new MeterReadingForm(meter);
        if (readingForm.ShowDialog(this) == DialogResult.OK)
        {
            tslblStatus.Text = "تم حفظ القراءة، جارٍ تحديث العدادات...";
            await PerformSearchAsync();
        }
    }

    private Task OpenCollectionForSelectedAsync()
    {
        var meter = SelectedMeter();
        if (meter is null || meter.CustomerId <= 0)
        {
            UiMessages.Warning("يرجى تحديد عداد مرتبط بعميل أولاً.", "لم يتم تحديد عداد");
            return Task.CompletedTask;
        }

        var customerId = meter.CustomerId;
        using var collectionForm = new FieldCollectionForm();
        var loaded = false;
        collectionForm.Shown += async (s, e) =>
        {
            if (loaded)
            {
                return;
            }

            loaded = true;
            try
            {
                await collectionForm.LoadByCustomerIdAsync(customerId);
            }
            catch (OperationCanceledException)
            {
            }
            catch (SqlException sqlEx)
            {
                UiMessages.Warning($"تعذر تحميل بيانات تحصيل العميل:\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
            }
            catch (Exception ex)
            {
                UiMessages.Error($"حدث خطأ غير متوقع أثناء تحميل التحصيل:\n{ex.Message}");
            }
        };

        collectionForm.ShowDialog(this);
        UpdateActionButtons();
        return Task.CompletedTask;
    }

    private void ApplyCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.CellStyle is null)
        {
            return;
        }

        if (e.RowIndex < dgvMeters.Rows.Count && dgvMeters.Rows[e.RowIndex].DataBoundItem is Meter meter)
        {
            if (e.ColumnIndex == _colCustomer)
            {
                var fullName = string.IsNullOrWhiteSpace(meter.FullName) ? null : meter.FullName;
                var number = string.IsNullOrWhiteSpace(meter.CustomerNumber) ? null : meter.CustomerNumber;
                e.Value = fullName is null && number is null
                    ? "—"
                    : fullName is null
                        ? number
                        : number is null
                            ? fullName
                            : $"{fullName} ({number})";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == _colLastReading && meter.LastReadingValue.HasValue)
            {
                e.Value = UiText.Amount3(meter.LastReadingValue.Value);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == _colLastReading && !meter.LastReadingValue.HasValue)
            {
                e.Value = "—";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == _colLastReadingDate && meter.LastReadingDate.HasValue)
            {
                e.Value = meter.LastReadingDate.Value.ToString("yyyy-MM-dd");
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
            }
            else if (e.ColumnIndex == _colLastReadingDate && !meter.LastReadingDate.HasValue)
            {
                e.Value = "—";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
            }
        }

        if (e.ColumnIndex == _colStatus && e.Value is byte status)
        {
            e.Value = status switch
            {
                1 => "نشط",
                0 => "غير نشط",
                _ => "غير معروف"
            };
            e.CellStyle.ForeColor = status == 1 ? UiTheme.Success : UiTheme.Danger;
            e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
            e.FormattingApplied = true;
        }
    }

    private static bool Contains(string? value, string filter) =>
        !string.IsNullOrWhiteSpace(value) && value.Contains(filter, StringComparison.OrdinalIgnoreCase);

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _busy?.Dispose();
        base.OnFormClosing(e);
    }
}