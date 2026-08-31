using Microsoft.Data.SqlClient;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

/// <summary>
/// Central administrative reports screen. Each tab is a read-only report backed by
/// an existing database view; filtering is client-side only (no SQL is generated).
/// </summary>
public sealed partial class ReportsForm : Form
{
    private readonly BillingService _billingService;
    private readonly ReceiptService _receiptService;
    private readonly MeterService _meterService;

    private readonly List<IReportPage> _pages = new();

    public ReportsForm(BillingService billingService, ReceiptService receiptService, MeterService meterService)
    {
        _billingService = billingService ?? throw new ArgumentNullException(nameof(billingService));
        _receiptService = receiptService ?? throw new ArgumentNullException(nameof(receiptService));
        _meterService = meterService ?? throw new ArgumentNullException(nameof(meterService));

        InitializeComponent();
        UiTheme.ApplyFormSurface(this);
        lblTitle.ForeColor = UiTheme.PrimaryDark;

        _pages.Add(BuildOpenInvoicesPage());
        _pages.Add(BuildBalancesPage());
        _pages.Add(BuildReceiptsPage());
        _pages.Add(BuildReadingsPage());

        _pages[0].AttachTo(tabOpenInvoices);
        _pages[1].AttachTo(tabBalances);
        _pages[2].AttachTo(tabReceipts);
        _pages[3].AttachTo(tabReadings);

        btnExit.Click += (s, e) => ExitRequested?.Invoke();
        tabReports.SelectedIndexChanged += (s, e) => _pages[tabReports.SelectedIndex].EnsureInitialLoad();

        KeyDown += async (s, e) =>
        {
            if (e.KeyCode == Keys.F5)
            {
                e.Handled = true;
                await _pages[tabReports.SelectedIndex].RefreshAsync();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                ExitRequested?.Invoke();
            }
        };
    }

    /// <summary>Raised (on the UI thread) when the user asks to leave the reports screen.</summary>
    public event Action? ExitRequested;

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        var page = _pages[tabReports.SelectedIndex];
        if (page.HasLoadedOnce)
        {
            _ = page.RefreshAsync();
        }
        else
        {
            page.EnsureInitialLoad();
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        foreach (var page in _pages)
        {
            page.CancelPending();
        }

        base.OnFormClosing(e);
    }

    private IReportPage BuildOpenInvoicesPage()
    {
        int balanceColumn = 0;

        return new ReportPage<Invoice>(
            "الفواتير المفتوحة",
            "البحث برقم الفاتورة أو رقم/اسم العميل أو رقم العداد:",
            "رقم الفاتورة / رقم العميل / اسم العميل / رقم العداد",
            grid =>
            {
                grid.Columns.Add(Column(nameof(Invoice.InvoiceNumber), "رقم الفاتورة", 110));
                grid.Columns.Add(Column(nameof(Invoice.CustomerNumber), "رقم العميل", 90));
                grid.Columns.Add(Column(nameof(Invoice.FullName), "اسم العميل", 150));
                grid.Columns.Add(Column(nameof(Invoice.MeterNumber), "رقم العداد", 90));
                grid.Columns.Add(Column(nameof(Invoice.BillingYear), "السنة", 50, "0"));
                grid.Columns.Add(Column(nameof(Invoice.BillingMonth), "الشهر", 50, "0"));
                grid.Columns.Add(Column(nameof(Invoice.UnitsConsumed), "الاستهلاك", 70, "N3"));
                grid.Columns.Add(Column(nameof(Invoice.WaterAmount), "قيمة المياه", 75, "N2"));
                grid.Columns.Add(Column(nameof(Invoice.SubscriptionAmount), "الاشتراك", 70, "N2"));
                grid.Columns.Add(Column(nameof(Invoice.PenaltyAmount), "الغرامة", 65, "N2"));
                grid.Columns.Add(Column(nameof(Invoice.ArrearsAmount), "المتأخرات", 70, "N2"));
                grid.Columns.Add(Column(nameof(Invoice.TotalAmount), "الإجمالي", 80, "N2"));
                grid.Columns.Add(Column(nameof(Invoice.PaidAmount), "المدفوع", 75, "N2"));
                grid.Columns.Add(Column(nameof(Invoice.BalanceAmount), "المتبقي", 80, "N2"));
                grid.Columns.Add(Column(nameof(Invoice.StatusName), "الحالة", 80));
                grid.Columns.Add(Column(nameof(Invoice.InvoiceDate), "تاريخ الفاتورة", 85, "yyyy-MM-dd"));

                balanceColumn = grid.Columns[nameof(Invoice.BalanceAmount)].Index;
                grid.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == balanceColumn && e.Value is decimal balance && e.CellStyle is not null)
                    {
                        bool hasBalance = balance > 0m;
                        e.CellStyle.ForeColor = hasBalance ? UiTheme.Danger : UiTheme.Success;
                        e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
                    }
                };
            },
            token => _billingService.GetOpenInvoicesAsync(token),
            (invoice, filter) => Contains(invoice.InvoiceNumber, filter)
                || Contains(invoice.CustomerNumber, filter)
                || Contains(invoice.FullName, filter)
                || Contains(invoice.MeterNumber, filter));
    }

    private IReportPage BuildBalancesPage()
    {
        int statusColumn = 0;
        int transferColumn = 0;

        return new ReportPage<InvoiceBalance>(
            "أرصدة الفواتير",
            "البحث برقم الفاتورة أو رقم العميل أو رقم العداد:",
            "رقم الفاتورة / رقم العميل / رقم العداد",
            grid =>
            {
                grid.Columns.Add(Column(nameof(InvoiceBalance.InvoiceNumber), "رقم الفاتورة", 115));
                grid.Columns.Add(Column(nameof(InvoiceBalance.CustomerId), "رقم العميل", 75, "0"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.MeterId), "رقم العداد", 75, "0"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.BillingYear), "السنة", 50, "0"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.BillingMonth), "الشهر", 50, "0"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.TotalAmount), "الإجمالي", 80, "N2"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.PaidAmount), "المدفوع", 75, "N2"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.BalanceAmount), "المتبقي", 80, "N2"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.TransferredAmount), "المحوّل", 75, "N2"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.TransferredByHistory), "المحوّل سابقاً", 85, "N2"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.OutstandingAmount), "المستحق المتبقي", 85, "N2"));
                grid.Columns.Add(Column(nameof(InvoiceBalance.IsTransferred), "محوّل؟", 65));
                grid.Columns.Add(Column(nameof(InvoiceBalance.Status), "الحالة", 70));

                statusColumn = grid.Columns[nameof(InvoiceBalance.Status)].Index;
                transferColumn = grid.Columns[nameof(InvoiceBalance.IsTransferred)].Index;
                grid.CellFormatting += (s, e) =>
                {
                    if (e.CellStyle is null)
                    {
                        return;
                    }

                    if (e.ColumnIndex == statusColumn && e.Value is byte status)
                    {
                        e.Value = UiText.InvoiceStatusName(status);
                        e.CellStyle.ForeColor = status switch
                        {
                            3 => UiTheme.Success,
                            4 => UiTheme.Danger,
                            _ => UiTheme.Accent
                        };
                        e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
                        e.FormattingApplied = true;
                    }
                    else if (e.ColumnIndex == transferColumn && e.Value is bool transferred)
                    {
                        e.Value = transferred ? "محوّل" : "غير محوّل";
                        e.CellStyle.ForeColor = transferred ? UiTheme.Danger : UiTheme.Success;
                        e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
                        e.FormattingApplied = true;
                    }
                };
            },
            token => _billingService.GetInvoiceBalancesAsync(token),
            (balance, filter) => Contains(balance.InvoiceNumber, filter)
                || balance.CustomerId.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)
                || balance.MeterId.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase));
    }

    private IReportPage BuildReceiptsPage()
    {
        int reversedColumn = 0;

        return new ReportPage<Receipt>(
            "الإيصالات",
            "البحث برقم الإيصال أو رقم/اسم العميل أو رقم الفاتورة أو رقم العداد:",
            "رقم الإيصال / رقم العميل / اسم العميل / رقم الفاتورة / رقم العداد",
            grid =>
            {
                grid.Columns.Add(Column(nameof(Receipt.ReceiptNumber), "رقم الإيصال", 95));
                grid.Columns.Add(Column(nameof(Receipt.ReceiptDate), "تاريخ الإيصال", 110, "yyyy-MM-dd HH:mm"));
                grid.Columns.Add(Column(nameof(Receipt.PaymentId), "رقم الدفعة", 70, "0"));
                grid.Columns.Add(Column(nameof(Receipt.InvoiceId), "معرّف الفاتورة", 85, "0"));
                grid.Columns.Add(Column(nameof(Receipt.InvoiceNumber), "رقم الفاتورة", 95));
                grid.Columns.Add(Column(nameof(Receipt.CustomerNumber), "رقم العميل", 80));
                grid.Columns.Add(Column(nameof(Receipt.FullName), "اسم العميل", 140));
                grid.Columns.Add(Column(nameof(Receipt.MeterNumber), "رقم العداد", 80));
                grid.Columns.Add(Column(nameof(Receipt.PaymentDate), "تاريخ الدفعة", 110, "yyyy-MM-dd HH:mm"));
                grid.Columns.Add(Column(nameof(Receipt.Amount), "مبلغ الدفعة", 80, "N2"));
                grid.Columns.Add(Column(nameof(Receipt.PaymentMethodName), "طريقة الدفع", 85));
                grid.Columns.Add(Column(nameof(Receipt.ReferenceNumber), "رقم المرجع", 85));
                grid.Columns.Add(Column(nameof(Receipt.StatusName), "حالة الفاتورة", 85));
                grid.Columns.Add(Column(nameof(Receipt.IsReversed), "معكوسة؟", 65));
                grid.Columns.Add(Column(nameof(Receipt.ReversalDate), "تاريخ العكس", 110, "yyyy-MM-dd HH:mm"));
                grid.Columns.Add(Column(nameof(Receipt.ReversedAmount), "المبلغ المعكوس", 85, "N2"));
                grid.Columns.Add(Column(nameof(Receipt.ReversalReason), "سبب العكس", 130));

                reversedColumn = grid.Columns[nameof(Receipt.IsReversed)].Index;
                grid.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == reversedColumn && e.Value is bool isReversed && e.CellStyle is not null)
                    {
                        e.Value = isReversed ? "نعم" : "لا";
                        e.CellStyle.ForeColor = isReversed ? UiTheme.Danger : UiTheme.Success;
                        e.CellStyle.Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold);
                        e.FormattingApplied = true;
                    }
                };
            },
            token => _receiptService.GetReceiptsAsync(token),
            (receipt, filter) => Contains(receipt.ReceiptNumber, filter)
                || Contains(receipt.CustomerNumber, filter)
                || Contains(receipt.FullName, filter)
                || Contains(receipt.InvoiceNumber, filter)
                || Contains(receipt.MeterNumber, filter)
                || Contains(receipt.ReferenceNumber, filter));
    }

    private IReportPage BuildReadingsPage()
    {
        return new ReportPage<MeterReading>(
            "قراءات العدادات",
            "البحث برقم العداد:",
            "رقم العداد (مثال: 100002)",
            grid =>
            {
                grid.Columns.Add(Column(nameof(MeterReading.MeterReadingId), "رقم القراءة", 85, "0"));
                grid.Columns.Add(Column(nameof(MeterReading.MeterNumber), "رقم العداد", 95));
                grid.Columns.Add(Column(nameof(MeterReading.ReadingDate), "تاريخ القراءة", 100, "yyyy-MM-dd"));
                grid.Columns.Add(Column(nameof(MeterReading.ReadingValue), "قيمة القراءة", 90, "N3"));
                grid.Columns.Add(Column(nameof(MeterReading.PreviousReading), "القراءة السابقة", 90, "N3"));
                grid.Columns.Add(Column(nameof(MeterReading.Consumption), "الاستهلاك", 80, "N3"));
                grid.Columns.Add(Column(nameof(MeterReading.Notes), "ملاحظات", 150));
                grid.Columns.Add(Column(nameof(MeterReading.CreatedAt), "تاريخ الإدخال", 120, "yyyy-MM-dd HH:mm"));
            },
            token => _meterService.GetMeterReadingsAsync(token),
            (reading, filter) => Contains(reading.MeterNumber, filter));
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
                Alignment = format is null || format == "0" || format.StartsWith("yyyy-MM-dd", StringComparison.Ordinal)
                    ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleRight,
                Format = format
            }
        };
    }

    private static bool Contains(string? value, string filter) =>
        !string.IsNullOrWhiteSpace(value) && value.Contains(filter, StringComparison.OrdinalIgnoreCase);

    private interface IReportPage
    {
        void AttachTo(Control host);
        void EnsureInitialLoad();
        Task RefreshAsync();
        void CancelPending();
        bool HasLoadedOnce { get; }
    }

    private sealed class ReportPage<T> : IReportPage where T : class
    {
        private readonly Panel _root;
        private readonly GroupBox _searchBox;
        private readonly Label _lblCaption;
        private readonly TextBox _txtSearch;
        private readonly Button _btnSearch;
        private readonly Button _btnClear;
        private readonly Button _btnRefresh;
        private readonly ProgressBar _pbLoading;
        private readonly DataGridView _grid;
        private readonly StatusStrip _statusStrip;
        private readonly ToolStripStatusLabel _tslblStatus;
        private readonly ToolStripStatusLabel _tslblCount;

        private readonly Func<CancellationToken, Task<IReadOnlyList<T>>> _loader;
        private readonly Func<T, string, bool> _match;
        private CancellationTokenSource? _cts;
        private bool _isSearching;
        private bool _loadedOnce;

        public ReportPage(
            string title,
            string searchCaption,
            string searchPlaceholder,
            Action<DataGridView> configureGrid,
            Func<CancellationToken, Task<IReadOnlyList<T>>> loader,
            Func<T, string, bool> match)
        {
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
            _match = match ?? throw new ArgumentNullException(nameof(match));

            _root = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6) };

            _searchBox = new GroupBox
            {
                Dock = DockStyle.Fill,
                Font = new Font(UiTheme.DefaultFontFamily, 9.5F, FontStyle.Bold),
                Text = title,
                TabStop = false,
                RightToLeft = RightToLeft.Yes
            };

            _lblCaption = new Label
            {
                AutoSize = true,
                Font = UiTheme.BodyFont(9F),
                Location = new Point(10, 26),
                Text = searchCaption
            };

            _txtSearch = new TextBox
            {
                Font = UiTheme.BodyFont(9.5F),
                Location = new Point(438, 24),
                Size = new Size(430, 24),
                PlaceholderText = searchPlaceholder,
                AccessibleName = "بحث"
            };

            _txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    _ = PerformSearchAsync();
                }
            };

            _btnSearch = StyleActionButton("بحث");
            _btnSearch.Location = new Point(338, 22);
            _btnSearch.Click += async (s, e) => await PerformSearchAsync();

            _btnClear = StyleNeutralButton("مسح");
            _btnClear.Location = new Point(238, 22);
            _btnClear.Click += (s, e) => { _txtSearch.Clear(); _ = PerformSearchAsync(); };

            _btnRefresh = StylePrimaryButton("تحديث");
            _btnRefresh.Location = new Point(138, 22);
            _btnRefresh.Click += async (s, e) => await PerformSearchAsync();

            _txtSearch.TabIndex = 0;
            _btnSearch.TabIndex = 1;
            _btnClear.TabIndex = 2;
            _btnRefresh.TabIndex = 3;

            _searchBox.Controls.Add(_lblCaption);
            _searchBox.Controls.Add(_txtSearch);
            _searchBox.Controls.Add(_btnSearch);
            _searchBox.Controls.Add(_btnClear);
            _searchBox.Controls.Add(_btnRefresh);

            var toolbar = new Panel { Dock = DockStyle.Top, Padding = new Padding(6), Height = 74 };
            toolbar.Controls.Add(_searchBox);

            _pbLoading = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 4,
                Style = ProgressBarStyle.Marquee,
                Visible = false
            };

            var gridHeader = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = UiTheme.SurfaceAlt,
                Font = new Font(UiTheme.DefaultFontFamily, 9F, FontStyle.Bold),
                ForeColor = UiTheme.TextPrimary,
                WrapMode = DataGridViewTriState.False
            };

            _grid = new DataGridView
            {
                AutoGenerateColumns = false,
                ColumnHeadersDefaultCellStyle = gridHeader,
                ColumnHeadersHeight = 34,
                Dock = DockStyle.Fill,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RightToLeft = RightToLeft.Yes
            };
            configureGrid(_grid);
            _grid.AccessibleName = $"جدول التقرير: {title}";
            UiTheme.ApplyGridDefaults(_grid);

            _statusStrip = new StatusStrip { RightToLeft = RightToLeft.Yes, SizingGrip = false };
            _tslblStatus = new ToolStripStatusLabel { Text = "جاهز", Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            _tslblCount = new ToolStripStatusLabel { Text = string.Empty, TextAlign = ContentAlignment.MiddleRight };
            _statusStrip.Items.Add(_tslblStatus);
            _statusStrip.Items.Add(_tslblCount);

            _root.Controls.Add(_grid);
            _root.Controls.Add(toolbar);
            _root.Controls.Add(_pbLoading);
            _root.Controls.Add(_statusStrip);
        }

        public void AttachTo(Control host)
        {
            host.Controls.Add(_root);
            _root.BringToFront();
        }

        public void EnsureInitialLoad()
        {
            if (_loadedOnce || _isSearching)
            {
                return;
            }

            _loadedOnce = true;
            _ = PerformSearchAsync();
        }

        public bool HasLoadedOnce => _loadedOnce;

        public Task RefreshAsync() => PerformSearchAsync();

        public void CancelPending()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async Task PerformSearchAsync()
        {
            if (_isSearching)
            {
                return;
            }

            CancelPending();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            SetBusy(true);
            _isSearching = true;

            try
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                var all = await _loader(token);
                stopwatch.Stop();

                if (token.IsCancellationRequested)
                {
                    return;
                }

                var filter = _txtSearch.Text.Trim();
                List<T> rows;
                if (string.IsNullOrWhiteSpace(filter))
                {
                    rows = all.ToList();
                }
                else
                {
                    rows = all.Where(item => _match(item, filter)).ToList();
                }

                if (rows.Count > 0)
                {
                    _grid.DataSource = rows;
                }
                else
                {
                    _grid.DataSource = null;
                }

                _tslblCount.Text = BuildCountText(rows.Count, all.Count, filter);
                _tslblStatus.ForeColor = UiTheme.TextSecondary;
                if (all.Count == 0)
                {
                    _tslblStatus.Text = UiTheme.StatusEmpty;
                }
                else if (rows.Count == 0)
                {
                    _tslblStatus.Text = "لا توجد نتائج مطابقة للبحث.";
                }
                else
                {
                    _tslblStatus.Text = $"تم التحميل خلال {stopwatch.ElapsedMilliseconds} ملي ثانية.";
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (SqlException sqlEx)
            {
                _grid.DataSource = null;
                _tslblCount.Text = string.Empty;
                _tslblStatus.ForeColor = UiTheme.Danger;
                _tslblStatus.Text = UiTheme.StatusError;
                UiMessages.Warning($"حدث خطأ من قاعدة البيانات أثناء التحميل:\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
            }
            catch (Exception ex)
            {
                _grid.DataSource = null;
                _tslblCount.Text = string.Empty;
                _tslblStatus.ForeColor = UiTheme.Danger;
                _tslblStatus.Text = UiTheme.StatusError;
                UiMessages.Error($"حدث خطأ غير متوقع أثناء التحميل:\n{ex.Message}");
            }
            finally
            {
                _isSearching = false;
                SetBusy(false);
            }
        }

        private static string BuildCountText(int visible, int total, string filter) =>
            string.IsNullOrWhiteSpace(filter)
                ? (total == 1 ? "سجل واحد" : $"{total} سجلات")
                : $"عرض {visible} من {total}";

        private void SetBusy(bool isBusy)
        {
            _btnSearch.Enabled = !isBusy;
            _btnClear.Enabled = !isBusy;
            _btnRefresh.Enabled = !isBusy;
            _txtSearch.Enabled = !isBusy;
            _pbLoading.Visible = isBusy;
            if (isBusy)
            {
                _tslblStatus.ForeColor = UiTheme.TextSecondary;
                _tslblStatus.Text = UiTheme.StatusLoading;
            }
        }

        private static Button StylePrimaryButton(string text) => StyleToolbarButton(text, 95);

        private static Button StyleActionButton(string text) => StyleToolbarButton(text, 90);

        private static Button StyleNeutralButton(string text) => StyleToolbarButton(text, 90);

        private static Button StyleToolbarButton(string text, int width)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(width, UiTheme.TertiaryButtonHeight),
                TextAlign = ContentAlignment.MiddleCenter,
                AccessibleName = text
            };
            UiTheme.StyleTertiaryButton(button, 9);
            return button;
        }
    }
}