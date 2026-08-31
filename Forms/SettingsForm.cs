using System.Globalization;
using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

public sealed partial class SettingsForm : Form
{
    private readonly ConnectionService _connectionService;

    private CancellationTokenSource? _testCts;
    private bool _testing;

    private readonly Button btnTestConnection = null!;
    private readonly ProgressBar pbTest = null!;
    private readonly Label lblTestResult = null!;
    private readonly Label lblTestTime = null!;
    private readonly TextBox txtTestDetail = null!;

    public event Action? ExitRequested;

    public SettingsForm(ConnectionService connectionService)
    {
        _connectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));

        InitializeComponent();
        UiTheme.ApplyFormSurface(this);
        lblTitle.ForeColor = UiTheme.PrimaryDark;

        btnTestConnection = new Button();
        pbTest = new ProgressBar();
        lblTestResult = new Label();
        lblTestTime = new Label();
        txtTestDetail = new TextBox();

        PopulateSections();

        btnExit.Click += (_, _) => ExitRequested?.Invoke();
        btnTestConnection.Click += async (_, _) => await TestConnectionAsync();
        Shown += (_, _) =>
        {
            if (btnTestConnection.CanFocus)
            {
                btnTestConnection.Focus();
            }
        };
        KeyDown += OnFormKeyDown;
        FormClosing += (_, _) =>
        {
            _testCts?.Cancel();
            _testCts?.Dispose();
            _testCts = null;
        };
    }

    private void PopulateSections()
    {
        ConnectionInfo info = _connectionService.GetConnectionInfo();
        AddGroup(BuildConnectionGroup(info));       // أ) معلومات الاتصال
        AddGroup(BuildTestGroup());                 // ب) اختبار الاتصال
        AddGroup(BuildSystemGroup(info.Database));  // ج) معلومات النظام
        AddGroup(BuildUiGroup());                   // د) الواجهة
    }

    private void OnFormKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Escape)
        {
            return;
        }

        e.Handled = true;
        ExitRequested?.Invoke();
    }

    private async Task TestConnectionAsync()
    {
        if (_testing)
        {
            return;
        }

        _testCts?.Cancel();
        _testCts?.Dispose();
        _testCts = new CancellationTokenSource();

        SetTestingState(true);
        try
        {
            ConnectionTestResult result = await _connectionService.TestConnectionAsync(_testCts.Token);

            lblTestResult.Text = result.IsSuccess ? "متصل" : "فشل الاتصال";
            lblTestResult.ForeColor = result.IsSuccess ? UiTheme.Success : UiTheme.Danger;
            lblTestTime.Text = $"{result.ElapsedMilliseconds} ms";

            txtTestDetail.Text = result.IsSuccess
                ? $"تم فتح الاتصال بقاعدة البيانات بنجاح خلال {result.ElapsedMilliseconds} ms."
                : result.ErrorMessage ?? "تعذر الاتصال بدون تفاصيل إضافية.";
        }
        catch (OperationCanceledException)
        {
            lblTestResult.Text = "تم الإلغاء";
            lblTestResult.ForeColor = UiTheme.TextSecondary;
            lblTestTime.Text = "—";
            txtTestDetail.Text = "تم إلغاء فحص الاتصال.";
        }
        finally
        {
            SetTestingState(false);
        }
    }

    private void SetTestingState(bool testing)
    {
        _testing = testing;
        btnTestConnection.Enabled = !testing;
        btnTestConnection.Text = testing ? "جارٍ الفحص..." : "اختبار الاتصال";
        pbTest.Visible = testing;
        pbTest.Style = testing ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;

        if (testing)
        {
            lblTestResult.Text = "جارٍ الفحص...";
            lblTestResult.ForeColor = UiTheme.Warning;
            lblTestTime.Text = "—";
            txtTestDetail.Text = "جاري فتح اتصال آمن بقاعدة البيانات — قد يستغرق بضع ثوانٍ.";
        }
    }

    private void AddGroup(GroupBox group)
    {
        pnlBody.Controls.Add(group);
        pnlBody.Controls.SetChildIndex(group, 0);
    }

    private GroupBox BuildConnectionGroup(ConnectionInfo info)
    {
        var table = CreateTable(5);
        AddValueRow(table, 0, "الخادم", info.Server);
        AddValueRow(table, 1, "قاعدة البيانات", info.Database);
        AddValueRow(table, 2, "وضع المصادقة", info.AuthenticationMode);
        AddValueRow(table, 3, "صلاحية التعديل", "قراءة فقط — لا يمكن تغيير الاتصال من هذه الشاشة");
        AddValueRow(table, 4, "كلمات المرور / الأسرار", "لا تُعرض في هذه الشاشة");
        return CreateGroup("معلومات الاتصال", table);
    }

    private GroupBox BuildTestGroup()
    {
        var table = CreateTable(4, lastRowHeight: 64);

        btnTestConnection.Text = "اختبار الاتصال";
        btnTestConnection.AutoSize = true;
        btnTestConnection.Padding = new Padding(12, 6, 12, 6);
        UiTheme.StylePrimaryButton(btnTestConnection);
        pbTest.Height = 12;
        pbTest.Visible = false;

        lblTestResult.Text = "—";
        lblTestResult.AutoSize = true;
        lblTestResult.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

        lblTestTime.Text = "—";
        lblTestTime.AutoSize = true;

        txtTestDetail.ReadOnly = true;
        txtTestDetail.Multiline = true;
        txtTestDetail.ScrollBars = ScrollBars.Vertical;
        txtTestDetail.TabStop = false;
        txtTestDetail.BackColor = UiTheme.Surface;
        txtTestDetail.ForeColor = UiTheme.TextSecondary;
        txtTestDetail.Font = UiTheme.MonoFont(8.5F);
        txtTestDetail.Dock = DockStyle.Fill;

        btnTestConnection.AccessibleName = "اختبار الاتصال بقاعدة البيانات";
        lblTestResult.AccessibleName = "نتيجة اختبار الاتصال";
        lblTestTime.AccessibleName = "زمن الاستجابة بالمللي ثانية";

        var panelTest = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };
        panelTest.Controls.Add(btnTestConnection);
        panelTest.Controls.Add(pbTest);
        table.Controls.Add(panelTest, 0, 0);
        table.SetColumnSpan(panelTest, 2);

        AddValueRow(table, 1, "الحالة", lblTestResult);
        AddValueRow(table, 2, "زمن الاستجابة", lblTestTime);
        AddCaptionAndControl(table, 3, "التفاصيل", txtTestDetail);

        return CreateGroup("اختبار الاتصال", table, extraHeight: 4);
    }

    private GroupBox BuildSystemGroup(string databaseName)
    {
        var table = CreateTable(6);
        AddValueRow(table, 0, "اسم التطبيق", ApplicationInfo.AppName);
        AddValueRow(table, 1, "الإصدار", $"الإصدار {ApplicationInfo.Version}");
        AddValueRow(table, 2, ".NET Runtime", ApplicationInfo.DotNetRuntime);
        AddValueRow(table, 3, "الجهاز", ApplicationInfo.MachineName);
        AddValueRow(table, 4, "البيئة", ApplicationInfo.BuildFlavor);
        AddValueRow(table, 5, "نظام التشغيل", ApplicationInfo.OperatingSystem);
        return CreateGroup("معلومات النظام", table);
    }

    private GroupBox BuildUiGroup()
    {
        var culture = CultureInfo.CurrentCulture;
        var dateTime = culture.DateTimeFormat;
        var numbers = culture.NumberFormat;
        string calendarName = dateTime.Calendar.GetType().Name.Replace("Calendar", string.Empty);

        var table = CreateTable(4);
        AddValueRow(table, 0, "اللغة", $"العربية ({ApplicationInfo.UICulture})");
        AddValueRow(table, 1, "اتجاه الواجهة", "من اليمين إلى اليسار — مفعّل");
        AddValueRow(table, 2, "صيغة التاريخ", $"{dateTime.ShortDatePattern} (التقويم: {calendarName}) — العرض في النظام: سنة-شهر-يوم HH:mm");
        AddValueRow(table, 3, "صيغة العملة", $"{numbers.CurrencySymbol} — منزلتان عشريتان. مثال: {1234.5.ToString("N2", culture)}");
        return CreateGroup("الواجهة", table);
    }

    private static GroupBox CreateGroup(string title, TableLayoutPanel table, int extraHeight = 0)
    {
        var group = new GroupBox
        {
            Text = title,
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            Height = 40 + table.Height + extraHeight,
            Padding = new Padding(10, 6, 10, 10)
        };
        group.Controls.Add(table);
        return group;
    }

    private static TableLayoutPanel CreateTable(int rows, int lastRowHeight = 34)
    {
        var table = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = ((rows - 1) * 34) + lastRowHeight,
            ColumnCount = 2,
            RowCount = rows
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
        for (int i = 0; i < rows; i++)
        {
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, i == rows - 1 ? lastRowHeight : 34));
        }

        return table;
    }

    private static Label AddValueRow(TableLayoutPanel table, int row, string caption, string value)
    {
        var lblCaption = new Label
        {
            Dock = DockStyle.Fill,
            Text = caption,
            Font = new Font("Segoe UI", 9F),
            ForeColor = UiTheme.TextSecondary,
            TextAlign = ContentAlignment.MiddleRight
        };
        var lblValue = new Label
        {
            Dock = DockStyle.Fill,
            Text = value,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            ForeColor = UiTheme.TextPrimary,
            TextAlign = ContentAlignment.MiddleLeft,
            AutoEllipsis = true
        };
        table.Controls.Add(lblCaption, 0, row);
        table.Controls.Add(lblValue, 1, row);
        return lblValue;
    }

    private static void AddValueRow(TableLayoutPanel table, int row, string caption, Control control)
    {
        AddCaptionAndControl(table, row, caption, control);
    }

    private static void AddCaptionAndControl(TableLayoutPanel table, int row, string caption, Control control)
    {
        var lblCaption = new Label
        {
            Dock = DockStyle.Fill,
            Text = caption,
            Font = new Font("Segoe UI", 9F),
            ForeColor = UiTheme.TextSecondary,
            TextAlign = ContentAlignment.MiddleRight
        };
        table.Controls.Add(lblCaption, 0, row);
        table.Controls.Add(control, 1, row);
    }
}