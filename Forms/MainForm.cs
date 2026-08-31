using System.ComponentModel;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Services;

namespace WaterStation.Forms;

public partial class MainForm : Form
{
    private readonly OverviewService _overviewService = new(new Database());

    private readonly Dictionary<string, Form> _screens = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Control> _placeholders = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Button> _navButtons = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, Panel> _navIndicators = new(StringComparer.OrdinalIgnoreCase);

    private CancellationTokenSource? _overviewCts;
    private UiBusy? _overviewBusy;

    private Control? _activeView;
    private string? _activeNavKey;
    private Panel? _dashboard;
    private Label? _lblCustomers;
    private Label? _lblMeters;
    private Label? _lblInvoices;
    private Label? _lblBalance;
    private Label? _lblOverviewStatus;
    private ProgressBar? _pbOverview;
    private Button? _btnRefreshOverview;

    public MainForm()
    {
        InitializeComponent();
        ApplyDesignSystem();
        BuildNavigation();
        ShowDashboard();

        tmrClock.Tick += (s, e) => UpdateClock();
        UpdateClock();
        tmrClock.Start();
    }

    private void ApplyDesignSystem()
    {
        // Header
        pnlHeader.BackColor = UiTheme.PrimaryDark;
        titleLabel.Font = UiTheme.TitleFont(18F);
        titleLabel.ForeColor = UiTheme.HeaderForeground;
        lblSubtitle.Font = UiTheme.BodyFont(9.5F);
        lblSubtitle.ForeColor = UiTheme.HeaderMutedText;

        // Sidebar
        pnlSidebar.BackColor = UiTheme.SurfaceAlt;
        pnlSidebar.BorderStyle = BorderStyle.FixedSingle;

        // Status strip
        statusStrip.BackColor = UiTheme.Surface;
        tslblStatus.Font = UiTheme.BodyFont(9F);
        tslblClock.Font = UiTheme.BodyFont(9F);
        tspbProgress.Visible = false;

        // Ensure RTL cascade
        UiTheme.EnsureRightToLeft(this);
    }

    private void BuildNavigation()
    {
        pnlNavFlow.Controls.Clear();
        _navButtons.Clear();

        // الرئيسية — عودة سريعة للوحة المعلومات من أي شاشة.
        AddNavButton("الرئيسية", "dashboard");

        // التحصيل — محور العمل اليومي، مرتب أولاً وموضحاً بأولوية.
        AddNavSection("التحصيل",
            ("التحصيل الميداني", "fieldcollection", true));

        AddNavSection("العملاء",
            ("العملاء", "customers", false));

        AddNavSection("العدادات",
            ("إدارة العدادات", "meters", false));

        AddNavSection("الفواتير",
            ("الفواتير", "invoices", false));

        AddNavSection("التقارير",
            ("التقارير", "reports", false));

        AddNavSection("النظام",
            ("الإعدادات", "settings", false));
    }

    private void AddNavSection(string title, params (string Text, string Key, bool Primary)[] items)
    {
        var header = new Label
        {
            Text = title,
            AutoSize = false,
            Width = pnlNavFlow.ClientSize.Width - 20,
            Height = 26,
            Margin = new Padding(2, 10, 2, 2),
            TextAlign = ContentAlignment.MiddleRight,
            Font = UiTheme.SectionHeaderFont,
            ForeColor = UiTheme.PrimaryDark,
            AccessibleName = $"قسم {title}"
        };
        pnlNavFlow.Controls.Add(header);

        foreach (var item in items)
        {
            AddNavButton(item.Text, item.Key, item.Primary);
        }
    }

    private void AddNavButton(string text, string key, bool primary = false)
    {
        var button = new Button
        {
            Text = text,
            Tag = key,
            TextAlign = ContentAlignment.MiddleRight,
            AccessibleName = $"انتقال إلى {text}",
            AutoSize = false,
            Width = pnlNavFlow.ClientSize.Width - 24,
            Height = 40,
            Margin = new Padding(2, 3, 2, 3)
        };

        if (primary)
        {
            UiTheme.StyleNavButton(button, primary: true);
        }
        else
        {
            button.FlatStyle = FlatStyle.Flat;
            button.Font = UiTheme.BodyFont(9.5F);
            button.BackColor = UiTheme.Surface;
            button.ForeColor = UiTheme.TextPrimary;
            button.FlatAppearance.BorderColor = UiTheme.Border;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.MouseOverBackColor = UiTheme.HoverNeutralColor;
            button.FlatAppearance.MouseDownBackColor = UiTheme.PressedNeutralColor;
            button.Cursor = Cursors.Hand;
        }

        button.Click += (s, e) => NavigateTo(key);
        _navButtons[key] = button;

        var indicator = new Panel
        {
            Dock = DockStyle.Left,
            Width = 4,
            BackColor = Color.Transparent,
            Enabled = false
        };
        button.Controls.Add(indicator);
        _navIndicators[key] = indicator;

        pnlNavFlow.Controls.Add(button);
    }

    private void SetActiveNav(string? key)
    {
        _activeNavKey = key;
        foreach (var (navKey, button) in _navButtons)
        {
            var isActive = string.Equals(navKey, key, StringComparison.OrdinalIgnoreCase);
            var indicator = _navIndicators.TryGetValue(navKey, out var ind) ? ind : null;
            if (isActive)
            {
                button.BackColor = UiTheme.PrimaryDark;
                button.ForeColor = UiTheme.HeaderForeground;
                button.FlatAppearance.BorderColor = UiTheme.PrimaryDark;
                button.FlatAppearance.MouseOverBackColor = UiTheme.Primary;
                if (indicator is not null)
                {
                    indicator.BackColor = UiTheme.Accent;
                }
            }
            else if (navKey == "fieldcollection")
            {
                button.BackColor = UiTheme.PrimaryDark;
                button.ForeColor = UiTheme.HeaderForeground;
                button.FlatAppearance.BorderColor = UiTheme.PrimaryDark;
                if (indicator is not null)
                {
                    indicator.BackColor = Color.Transparent;
                }
            }
            else
            {
                button.BackColor = UiTheme.Surface;
                button.ForeColor = UiTheme.TextPrimary;
                button.FlatAppearance.BorderColor = UiTheme.Border;
                if (indicator is not null)
                {
                    indicator.BackColor = Color.Transparent;
                }
            }
        }
    }

    private void NavigateTo(string key)
    {
        switch (key)
        {
            case "dashboard":
                ShowDashboard();
                break;
            case "collection":
                ShowScreenLazy(key, () => new CustomerCollectionForm(), "شاشة تحصيل العميل — البحث، القراءة، السداد، والعكس.");
                break;
            case "fieldcollection":
                ShowScreenLazy(key, () => new FieldCollectionForm(), "التحصيل الميداني — بحث سريع، قراءة، فاتورة، سداد، وعكس.");
                break;
            case "meters":
                ShowScreenLazy(key, () =>
                {
                    var metersForm = new MetersManagementForm(new MeterService(new Database()));
                    metersForm.ExitRequested += () => ShowDashboard();
                    return metersForm;
                }, "إدارة العدادات — عرض العدادات النشطة مع إدخال قراءة أو فتح شاشة تحصيل العميل.");
                break;
            case "readings":
                ShowScreenLazy(key, () => new ReadingsViewForm(new MeterService(new Database())), "سجل قراءات العدادات.");
                break;
            case "invoices":
                ShowScreenLazy(key, () => new InvoicesViewForm(new BillingService(new Database())), "الفواتير المفتوحة مع إمكانية السداد.");
                break;
            case "receipts":
                ShowScreenLazy(key, () => new ReceiptsViewForm(new ReceiptService(new Database())), "الإيصالات الصادرة.");
                break;
            case "customers":
                ShowScreenLazy(key, () =>
                {
                    var customersForm = new CustomersForm(new CustomerService(new Database()));
                    customersForm.ExitRequested += () => ShowDashboard();
                    return customersForm;
                }, "إدارة العملاء — البحث، العرض، وإضافة عملاء جدد.");
                break;
            case "reports":
                ShowScreenLazy(key, () =>
                {
                    var reportsForm = new ReportsForm(
                        new BillingService(new Database()),
                        new ReceiptService(new Database()),
                        new MeterService(new Database()));
                    reportsForm.ExitRequested += () => ShowDashboard();
                    return reportsForm;
                }, "التقارير الإدارية — الفواتير المفتوحة، أرصدة الفواتير، الإيصالات، وقراءات العدادات.");
                break;
            case "settings":
                ShowScreenLazy(key, () =>
                {
                    var settingsForm = new SettingsForm(new ConnectionService(new Database()));
                    settingsForm.ExitRequested += () => ShowDashboard();
                    return settingsForm;
                }, "إعدادات النظام — معلومات الاتصال (قراءة فقط)، اختبار الاتصال، معلومات النظام، والواجهة.");
                break;
            default:
                ShowPlaceholder("قيد التطوير", "هذا القسم غير متوفر حاليًا.");
                break;
        }
    }

    private void ShowScreenLazy(string key, Func<Form> factory, string statusText)
    {
        if (!_screens.TryGetValue(key, out var screen))
        {
            screen = factory();
            _screens[key] = screen;
        }

        ShowView(screen);
        SetActiveNav(key);
        tslblStatus.Text = statusText;
    }

    private void ShowPlaceholder(string title, string description)
    {
        if (!_placeholders.TryGetValue(title + description, out var placeholder))
        {
            placeholder = CreatePlaceholder(title, description);
            _placeholders[title + description] = placeholder;
        }

        ShowView(placeholder);
        SetActiveNav(null);
        tslblStatus.Text = "هذا القسم قيد التطوير.";
    }

    private static Control CreatePlaceholder(string title, string description)
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(UiTheme.SpacingLarge) };
        var lblDescription = new Label
        {
            Dock = DockStyle.Top,
            Height = 120,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = UiTheme.HeadingFont(11F),
            ForeColor = UiTheme.TextSecondary,
            UseCompatibleTextRendering = false,
            Text = description
        };
        var lblTitle = new Label
        {
            Dock = DockStyle.Top,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = UiTheme.TitleFont(18F),
            ForeColor = UiTheme.TextPrimary,
            Text = title
        };
        panel.Controls.Add(lblDescription);
        panel.Controls.Add(lblTitle);
        return panel;
    }

    private void ShowView(Control view)
    {
        if (_activeView is not null && !ReferenceEquals(_activeView, view))
        {
            if (_activeView is Form activeForm)
            {
                UiShell.Detach(activeForm);
            }
            else
            {
                pnlContent.Controls.Remove(_activeView);
            }
        }

        if (view is Form childForm)
        {
            UiShell.EmbedAndShow(childForm, pnlContent);
        }
        else
        {
            view.Dock = DockStyle.Fill;
            if (!pnlContent.Controls.Contains(view))
            {
                pnlContent.Controls.Add(view);
            }

            view.BringToFront();
        }

        _activeView = view;
    }

    private void ShowDashboard()
    {
        _dashboard ??= BuildDashboard();
        ShowView(_dashboard);
        SetActiveNav("dashboard");
        tslblStatus.Text = "لوحة المعلومات.";
        _ = LoadOverviewAsync();
    }

    private Panel BuildDashboard()
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(UiTheme.SpacingMedium) };

        var heading = new Label
        {
            Dock = DockStyle.Top,
            Height = 30,
            TextAlign = ContentAlignment.MiddleRight,
            Font = UiTheme.TitleFont(15F),
            ForeColor = UiTheme.PrimaryDark,
            Text = "لوحة التحكم"
        };

        var subtitle = new Label
        {
            Dock = DockStyle.Top,
            Height = 20,
            TextAlign = ContentAlignment.MiddleRight,
            Font = UiTheme.CaptionFont,
            ForeColor = UiTheme.TextSecondary,
            Text = "نظرة عامة على حالة المحطة"
        };

        var cardsLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 150,
            ColumnCount = 4,
            RowCount = 1,
            Padding = new Padding(0)
        };
        cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        cardsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        cardsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        var card0 = CreateCard("العملاء المسجلون", "0", UiTheme.PrimaryDark, out _lblCustomers);
        var card1 = CreateCard("العدادات النشطة", "0", UiTheme.PrimaryDark, out _lblMeters);
        var card2 = CreateCard("الفواتير المفتوحة", "0", UiTheme.PrimaryDark, out _lblInvoices);
        var card3 = CreateCard("المبالغ المستحقة", "0.00 ر.س", UiTheme.Danger, out _lblBalance);
        cardsLayout.Controls.Add(card0, 0, 0);
        cardsLayout.Controls.Add(card1, 1, 0);
        cardsLayout.Controls.Add(card2, 2, 0);
        cardsLayout.Controls.Add(card3, 3, 0);

        var shortcutsHeading = new Label
        {
            Dock = DockStyle.Top,
            Height = 28,
            TextAlign = ContentAlignment.MiddleRight,
            Font = UiTheme.SectionHeaderFont,
            ForeColor = UiTheme.TextSecondary,
            Text = "البدء السريع"
        };

        var actionsFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 100,
            RightToLeft = RightToLeft.Yes,
            FlowDirection = FlowDirection.LeftToRight,
            Padding = new Padding(0),
            AutoScroll = false
        };

        _btnRefreshOverview = new Button { Text = "تحديث اللوحة", Width = 130, Height = UiTheme.PrimaryButtonHeight, Margin = new Padding(6, 8, 6, 6), AutoSize = false, AccessibleName = "تحديث اللوحة" };
        UiTheme.StyleTertiaryButton(_btnRefreshOverview, 9);
        _btnRefreshOverview.Click += async (s, e) => await LoadOverviewAsync();

        var btnFieldCollection = new Button { Text = "التحصيل الميداني", Width = 180, Height = UiTheme.PrimaryButtonHeight, Margin = new Padding(6, 8, 6, 6), AutoSize = false, AccessibleName = "فتح شاشة التحصيل الميداني" };
        UiTheme.StylePrimaryButton(btnFieldCollection, 10);
        btnFieldCollection.Click += (s, e) => NavigateTo("fieldcollection");

        var btnCustomers = CreateNavShortcut("العملاء", "customers");
        var btnMeters = CreateNavShortcut("إدارة العدادات", "meters");
        var btnInvoices = CreateNavShortcut("الفواتير", "invoices");

        _pbOverview = new ProgressBar { Width = 220, Height = 16, Margin = new Padding(10, 10, 4, 4), Style = ProgressBarStyle.Marquee, Visible = false };
        _lblOverviewStatus = new Label { AutoSize = true, Height = 30, Font = UiTheme.BodyFont(9F), ForeColor = UiTheme.TextSecondary, Margin = new Padding(8, 8, 4, 4), Text = "جاهز" };

        actionsFlow.Controls.Add(_lblOverviewStatus);
        actionsFlow.Controls.Add(_pbOverview);
        actionsFlow.Controls.Add(_btnRefreshOverview);
        actionsFlow.Controls.Add(btnFieldCollection);
        actionsFlow.Controls.Add(btnCustomers);
        actionsFlow.Controls.Add(btnMeters);
        actionsFlow.Controls.Add(btnInvoices);

        var panelFooter = new Label
        {
            Dock = DockStyle.Top,
            Height = 26,
            TextAlign = ContentAlignment.MiddleRight,
            Font = UiTheme.CaptionFont,
            ForeColor = UiTheme.TextSecondary,
            Text = "اختر قسمًا من الشريط الجانبي أو أحد الاختصارات أدناه لإنجاز المهمة."
        };

        panel.Controls.Add(panelFooter);
        panel.Controls.Add(actionsFlow);
        panel.Controls.Add(shortcutsHeading);
        panel.Controls.Add(cardsLayout);
        panel.Controls.Add(subtitle);
        panel.Controls.Add(heading);

        return panel;
    }

    private Button CreateNavShortcut(string text, string key)
    {
        var button = new Button
        {
            Text = text,
            Width = 140,
            Height = UiTheme.SecondaryButtonHeight,
            Margin = new Padding(6, 8, 6, 6),
            AutoSize = false,
            AccessibleName = $"انتقال إلى {text}"
        };
        UiTheme.StyleSecondaryButton(button, 9);
        button.Click += (s, e) => NavigateTo(key);
        return button;
    }

    private static Panel CreateCard(string title, string initialValue, Color valueColor, out Label valueLabel)
    {
        var card = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12, 16, 12, 12),
            Margin = new Padding(6)
        };

        UiTheme.StyleCardPanel(card);

        valueLabel = new Label();
        UiTheme.StyleCardValueLabel(valueLabel, valueColor);

        var caption = new Label();
        UiTheme.StyleCardCaptionLabel(caption);
        caption.Text = title;

        valueLabel.Text = initialValue;

        card.Controls.Add(valueLabel);
        card.Controls.Add(caption);
        return card;
    }

    private async Task LoadOverviewAsync()
    {
        if (_overviewBusy is null)
        {
            _overviewBusy ??= new UiBusy(_btnRefreshOverview is null ? Array.Empty<Control>() : new[] { _btnRefreshOverview }, _pbOverview, _lblOverviewStatus!, "جاهز");
        }

        _overviewBusy.Begin("جاري تحميل إحصائيات لوحة المعلومات...");

        _overviewCts?.Cancel();
        _overviewCts?.Dispose();
        _overviewCts = new CancellationTokenSource();

        try
        {
            var snapshot = await _overviewService.GetOverviewAsync(_overviewCts.Token);
            _lblCustomers!.Text = snapshot.CustomerCount.ToString("N0");
            _lblMeters!.Text = snapshot.ActiveMeterCount.ToString("N0");
            _lblInvoices!.Text = snapshot.OpenInvoiceCount.ToString("N0");
            _lblBalance!.Text = UiText.Currency(snapshot.OpenBalanceTotal);
            _overviewBusy.End($"تم التحديث في {DateTime.Now:HH:mm:ss}.");
        }
        catch (OperationCanceledException)
        {
            _overviewBusy.End("جاهز");
        }
        catch (SqlException sqlEx)
        {
            _overviewBusy.End("تعذر تحميل الإحصائيات.");
            UiMessages.Warning($"حدث خطأ من قاعدة البيانات أثناء تحميل الإحصائيات:\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
        }
        catch (Exception ex)
        {
            _overviewBusy.End("تعذر تحميل الإحصائيات.");
            UiMessages.Error($"حدث خطأ غير متوقع أثناء تحميل الإحصائيات:\n{ex.Message}");
        }
    }

    private void UpdateClock()
    {
        tslblClock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        // application info in title
        titleLabel.Text = $"{ApplicationInfo.AppName} — {ApplicationInfo.Version}";
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        tmrClock.Stop();

        _overviewCts?.Cancel();
        _overviewCts?.Dispose();

        foreach (var screen in _screens.Values)
        {
            screen.Dispose();
        }

        _screens.Clear();
        base.OnFormClosing(e);
    }
}