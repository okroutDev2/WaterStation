using System.Diagnostics;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;

namespace WaterStation.Forms;

/// <summary>
/// Base class for the read-only records screens (invoices, meters, readings,
/// receipts). Handles search, busy state, grid refresh, and status reporting.
/// </summary>
public abstract partial class RecordsViewForm<T> : Form where T : class
{
    private CancellationTokenSource? _cts;
    private bool _isSearching;
    private UiBusy? _busy;

    protected RecordsViewForm()
    {
        Database = new Database();
        InitializeComponent();
        Text = ScreenTitle;
        lblScreenTitle.Text = ScreenTitle.Replace(" - WaterStation", "", StringComparison.Ordinal);
        lblSearchCaption.Text = SearchCaption;
        txtSearch.PlaceholderText = SearchPlaceholder;
        ConfigureGrid();
        RegisterBaseEventHandlers();

        if (!string.IsNullOrWhiteSpace(ActionButtonText))
        {
            btnAction.Text = ActionButtonText;
            btnAction.Visible = true;
        }

        ApplyUiTheme();
    }

    /// <summary>
    /// Builds a themed grid column. Alignment follows data type conventions:
    /// unformatted text and Status columns are centered, dates are centered,
    /// numeric/amount columns are right-aligned.
    /// </summary>
    protected static DataGridViewTextBoxColumn Column(string property, string header, int weight, string? format = null)
    {
        var column = new DataGridViewTextBoxColumn
        {
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = weight,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = ColumnAlignment(format),
                Format = format
            }
        };
        return column;
    }

    private static DataGridViewContentAlignment ColumnAlignment(string? format) => format switch
    {
        null => DataGridViewContentAlignment.MiddleCenter,
        "0" => DataGridViewContentAlignment.MiddleCenter,
        var f when f.StartsWith("yyyy-MM-dd", StringComparison.Ordinal) => DataGridViewContentAlignment.MiddleCenter,
        _ => DataGridViewContentAlignment.MiddleRight
    };

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.ApplyGridDefaults(dgvList);
        UiTheme.StyleScreenHeaderLabel(lblScreenTitle);
        UiTheme.StyleTertiaryButton(btnSearch);
        UiTheme.StyleTertiaryButton(btnClear);
        if (btnAction.Visible)
        {
            UiTheme.StylePrimaryButton(btnAction);
        }

        txtSearch.AccessibleName = "البحث في السجلات";
        btnSearch.AccessibleName = "بحث";
        btnClear.AccessibleName = "مسح";
        btnAction.AccessibleName = ActionButtonText;
        dgvList.AccessibleName = "جدول النتائج";
    }

    protected Database Database { get; }

    protected abstract string ScreenTitle { get; }

    protected abstract string SearchCaption { get; }

    protected abstract string SearchPlaceholder { get; }

    protected virtual string EmptyStatus => UiTheme.StatusEmpty;

    protected virtual string? ActionButtonText => null;

    /// <summary>Adds the grid columns expected by the concrete screen.</summary>
    protected abstract void ConfigureGrid();

    /// <summary>Loads the records; <paramref name="filter"/> is null when "show all".</summary>
    protected abstract Task<IReadOnlyList<T>> LoadCoreAsync(string? filter, CancellationToken cancellationToken);

    /// <summary>Invoked when the optional action button is clicked (wire via <see cref="OnActionClickedAsync"/>).</summary>
    protected virtual Task OnActionClickedAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void RegisterBaseEventHandlers()
    {
        btnSearch.Click += async (s, e) => await PerformSearchAsync();
        btnClear.Click += (s, e) =>
        {
            txtSearch.Clear();
            _ = PerformSearchAsync();
        };
        btnAction.Click += async (s, e) =>
        {
            try
            {
                await OnActionClickedAsync(CancellationToken.None);
            }
            catch (SqlException sqlEx)
            {
                UiMessages.Warning($"حدث خطأ من قاعدة البيانات:\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
            }
            catch (Exception ex)
            {
                UiMessages.Error($"حدث خطأ غير متوقع:\n{ex.Message}");
            }
        };

        txtSearch.KeyDown += async (s, e) =>
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
                txtSearch.Clear();
                _ = PerformSearchAsync();
            }
        };
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        _ = PerformSearchAsync();
    }

    protected async Task PerformSearchAsync()
    {
        if (_isSearching)
        {
            return;
        }

        var filterText = txtSearch.Text.Trim();
        var filter = string.IsNullOrWhiteSpace(filterText) ? null : filterText;

        _busy ??= new UiBusy(new Control[] { btnSearch, btnClear, txtSearch, btnAction }, pbLoading, message => tslblStatus.Text = message, "جاهز");
        _busy.Begin(UiTheme.StatusLoading);

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        _isSearching = true;
        try
        {
            var stopwatch = Stopwatch.StartNew();
            var items = await LoadCoreAsync(filter, token);
            stopwatch.Stop();

            if (token.IsCancellationRequested)
            {
                return;
            }

            if (items.Count > 0)
            {
                dgvList.DataSource = items.ToList();
            }
            else
            {
                dgvList.DataSource = null;
            }

            tslblCount.Text = items.Count == 1 ? "سجل واحد" : $"{items.Count} سجلات";
            _busy.End(items.Count == 0 ? UiTheme.StatusEmpty : $"تم التحميل خلال {stopwatch.ElapsedMilliseconds} ملي ثانية.");
        }
        catch (OperationCanceledException)
        {
        }
        catch (SqlException sqlEx)
        {
            dgvList.DataSource = null;
            tslblCount.Text = string.Empty;
            _busy.End(UiTheme.StatusError);
            UiMessages.Warning($"حدث خطأ من قاعدة البيانات أثناء التحميل.\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
        }
        catch (Exception ex)
        {
            dgvList.DataSource = null;
            tslblCount.Text = string.Empty;
            _busy.End(UiTheme.StatusError);
            UiMessages.Error($"حدث خطأ غير متوقع أثناء التحميل:\n{ex.Message}");
        }
        finally
        {
            _isSearching = false;
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        base.OnFormClosing(e);
    }
}