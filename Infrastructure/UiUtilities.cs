namespace WaterStation.Infrastructure;

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/// <summary>
/// Shared visual constants and styling helpers for all WinForms screens.
/// Acts as the centralized Design System: colors, typography, spacing,
/// dimensions, component styles, grid defaults and RTL conventions.
/// Forms must not hard-code color hex values or pixel sizes that belong here.
/// </summary>
public static class UiTheme
{
    // Colors
    public static readonly Color Primary = ColorTranslator.FromHtml("#0B69A3"); // deep water blue
    public static readonly Color PrimaryDark = ColorTranslator.FromHtml("#064C73");
    public static readonly Color Accent = ColorTranslator.FromHtml("#0B9BD3");
    public static readonly Color Success = ColorTranslator.FromHtml("#219653");
    public static readonly Color Warning = ColorTranslator.FromHtml("#F2994A");
    public static readonly Color Danger = ColorTranslator.FromHtml("#D64545");
    public static readonly Color Surface = ColorTranslator.FromHtml("#FFFFFF");
    public static readonly Color SurfaceAlt = ColorTranslator.FromHtml("#F5F7FA");
    public static readonly Color TextPrimary = ColorTranslator.FromHtml("#0B2540");
    public static readonly Color TextSecondary = ColorTranslator.FromHtml("#6B7280");
    public static readonly Color Border = ColorTranslator.FromHtml("#DEE6EF");
    public static readonly Color MutedBorder = Border;
    public static readonly Color Disabled = ColorTranslator.FromHtml("#A9B6C2");
    public static readonly Color RowAlternate = ColorTranslator.FromHtml("#F6F8FB");
    public static readonly Color SuccessTint = ColorTranslator.FromHtml("#EEF7EE");
    public static readonly Color DangerTint = ColorTranslator.FromHtml("#F5EEEE");
    public static readonly Color WarningTint = ColorTranslator.FromHtml("#FAF2F0");

    // Interactive state tints (hover / pressed) — kept internal to the design system.
    private static readonly Color HoverPrimary = PrimaryDark;
    private static readonly Color PressedPrimary = ColorTranslator.FromHtml("#045A8A");
    private static readonly Color HoverNeutral = ColorTranslator.FromHtml("#EDF3F9");
    private static readonly Color PressedNeutral = ColorTranslator.FromHtml("#E2EBF4");

    /// <summary>Muted text color used on the dark shell header (title bar area).</summary>
    public static Color HeaderMutedText => ColorTranslator.FromHtml("#D2E1F0");

    /// <summary>Primary (bright) text color used on dark surfaces such as the shell header and active nav buttons.</summary>
    public static Color HeaderForeground => ColorTranslator.FromHtml("#FFFFFF");

    /// <summary>Neutral-button hover tint exposed for shell chrome (e.g. sidebar buttons).</summary>
    public static Color HoverNeutralColor => HoverNeutral;

    /// <summary>Neutral-button pressed tint exposed for shell chrome (e.g. sidebar buttons).</summary>
    public static Color PressedNeutralColor => PressedNeutral;
    private static readonly Color HoverDanger = ColorTranslator.FromHtml("#B53A3A");
    private static readonly Color PressedDanger = ColorTranslator.FromHtml("#9E3131");

    // Typography
    public const string DefaultFontFamily = "Segoe UI";
    public static Font TitleFont(float size = 20F) => new Font(DefaultFontFamily, size, FontStyle.Bold);
    public static Font HeadingFont(float size = 14F) => new Font(DefaultFontFamily, size, FontStyle.Bold);
    public static Font SectionHeaderFont => HeadingFont(10.5F);
    public static Font BodyFont(float size = 10F) => new Font(DefaultFontFamily, size, FontStyle.Regular);
    public static Font CaptionFont => BodyFont(8.5F);
    public static Font FieldLabelFont => BodyFont(9.5F);
    public static Font MonoFont(float size = 9F) => new Font("Consolas", size, FontStyle.Regular);

    // Spacing (pixels)
    public const int SpacingSmall = 8;
    public const int SpacingMedium = 16;
    public const int SpacingLarge = 24;
    public const int SpacingXLarge = 32;

    // Standard empty-state messages (Arabic), shared across list/detail screens.
    public const string EmptyCustomers = "لا يوجد عملاء مسجلون بعد.";
    public const string EmptyCustomersSearch = "لا توجد نتائج مطابقة لبحث العملاء.";
    public const string EmptyMeters = "لا توجد عدادات مسجلة.";
    public const string EmptyMetersCustomer = "لا توجد عدادات مسجلة لهذا العميل.";
    public const string EmptyMetersSearch = "لا توجد عدادات مطابقة للبحث أو التصفية.";
    public const string EmptyInvoices = "لا توجد فواتير مفتوحة.";
    public const string EmptyReceipts = "لا توجد إيصالات صادرة.";
    public const string EmptyReadings = "لا توجد قراءات مسجلة للعداد.";

    // Component dimensions (pixels) — the single source for all control sizes.
    public const int PrimaryButtonHeight = 36;
    public const int SecondaryButtonHeight = 34;
    public const int TertiaryButtonHeight = 30;
    public const int InputHeight = 30;
    public const int GridRowHeight = 28;
    public const int GridHeaderHeight = 32;
    public const int HeaderHeight = 52;
    public const int SectionSpacing = 20;
    public const int ControlGap = 8;

    // Standard status messages (in-screen; MessageBox is reserved for decisions).
    public const string StatusLoading = "جارٍ تحميل البيانات...";
    public const string StatusEmpty = "لا توجد بيانات لعرضها.";
    public const string StatusError = "تعذر تحميل البيانات.";
    public const string StatusSaved = "تم الحفظ بنجاح.";
    public const string StatusUpdated = "تم التحديث بنجاح.";

    /// <summary>Applies the design-system surface (form background / primary text color).</summary>
    public static void ApplyFormSurface(Form form)
    {
        form.BackColor = SurfaceAlt;
        form.ForeColor = TextPrimary;
    }

    private static void ApplyHoverStates(Button button, Color hover, Color pressed)
    {
        button.FlatAppearance.MouseOverBackColor = hover;
        button.FlatAppearance.MouseDownBackColor = pressed;
        button.Cursor = Cursors.Hand;
    }

    // Button styles
    public static void StylePrimaryButton(Button button, int fontSize = 10)
    {
        button.BackColor = Primary;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = new Font(DefaultFontFamily, fontSize, FontStyle.Bold);
        button.ForeColor = Color.White;
        button.UseVisualStyleBackColor = false;
        button.Height = PrimaryButtonHeight;
        ApplyHoverStates(button, HoverPrimary, PressedPrimary);
    }

    public static void StyleSecondaryButton(Button button, int fontSize = 9)
    {
        button.BackColor = Surface;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderColor = MutedBorder;
        button.FlatAppearance.BorderSize = 1;
        button.Font = new Font(DefaultFontFamily, fontSize, FontStyle.Regular);
        button.ForeColor = TextPrimary;
        button.UseVisualStyleBackColor = true;
        button.Height = SecondaryButtonHeight;
        ApplyHoverStates(button, HoverNeutral, PressedNeutral);
    }

    public static void StyleTertiaryButton(Button button, int fontSize = 9)
    {
        button.BackColor = Color.Transparent;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = new Font(DefaultFontFamily, fontSize, FontStyle.Regular);
        button.ForeColor = TextPrimary;
        button.UseVisualStyleBackColor = true;
        button.Height = TertiaryButtonHeight;
        ApplyHoverStates(button, HoverNeutral, PressedNeutral);
    }

    public static void StyleDangerButton(Button button, int fontSize = 9)
    {
        button.BackColor = Danger;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = new Font(DefaultFontFamily, fontSize, FontStyle.Bold);
        button.ForeColor = Color.White;
        button.UseVisualStyleBackColor = false;
        button.Height = PrimaryButtonHeight;
        ApplyHoverStates(button, HoverDanger, PressedDanger);
    }

    public static void StyleNavButton(Button button, bool primary = false)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.Font = new Font(DefaultFontFamily, 10F, FontStyle.Bold);
        button.Height = 44;
        button.AutoSize = true;
        button.MinimumSize = new Size(92, 44);
        button.Padding = new Padding(6, 0, 6, 0);
        button.Margin = new Padding(4, 3, 4, 3);
        if (primary)
        {
            button.BackColor = PrimaryDark;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderColor = PrimaryDark;
            button.FlatAppearance.BorderSize = 1;
            ApplyHoverStates(button, HoverPrimary, PressedPrimary);
        }
        else
        {
            button.BackColor = Surface;
            button.ForeColor = PrimaryDark;
            button.FlatAppearance.BorderColor = Border;
            button.FlatAppearance.BorderSize = 1;
            ApplyHoverStates(button, HoverNeutral, PressedNeutral);
        }
    }

    // Section headers
    public static void StyleSectionHeader(Label label)
    {
        label.Font = SectionHeaderFont;
        label.ForeColor = TextPrimary;
        label.TextAlign = ContentAlignment.MiddleLeft;
    }

    /// <summary>Applies the standard screen title text style used at the top of shell pages.</summary>
    public static void StyleScreenHeaderLabel(Label label)
    {
        label.Dock = DockStyle.Top;
        label.Height = 34;
        label.Font = HeadingFont(13F);
        label.ForeColor = PrimaryDark;
        label.TextAlign = ContentAlignment.MiddleRight;
        label.UseCompatibleTextRendering = false;
    }

    /// <summary>
    /// Applies a unified read/edit input (TextBox / ComboBox / NumericUpDown) style:
    /// fixed single-line height, clear border, and the design-system font.
    /// </summary>
    public static void StyleInput(Control input)
    {
        input.Font = BodyFont(9.5F);
        input.ForeColor = TextPrimary;
        input.BackColor = Surface;
        if (input is TextBox tb)
        {
            tb.BorderStyle = BorderStyle.FixedSingle;
            if (tb.Multiline)
            {
                tb.ScrollBars = ScrollBars.Vertical;
            }
        }
        else if (input is ComboBox cb)
        {
            cb.FlatStyle = FlatStyle.Flat;
        }
        else if (input is NumericUpDown nud)
        {
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.TextAlign = HorizontalAlignment.Right;
        }
        input.Height = Math.Max(input.Height, InputHeight);
    }

    /// <summary>
    /// Styles a Label to look like a small status chip (rounded-back appearance) so
    /// status is conveyed by a compact colored tag rather than per-cell recoloring.
    /// </summary>
    public static void StyleStatusChip(Label chip, Color backColor, Color foreColor)
    {
        chip.BackColor = backColor;
        chip.ForeColor = foreColor;
        chip.AutoSize = true;
        chip.Padding = new Padding(8, 2, 8, 2);
        chip.TextAlign = ContentAlignment.MiddleCenter;
        chip.Font = new Font(DefaultFontFamily, 8.5F, FontStyle.Bold);
    }

    /// <summary>Returns a light tinted background for a given semantic color (for status chips).</summary>
    public static Color TintOf(Color color)
    {
        if (color == Success) return SuccessTint;
        if (color == Danger) return DangerTint;
        if (color == Warning) return WarningTint;
        return SurfaceAlt;
    }

    /// <summary>
    /// Creates a centered empty-state panel (icon + message + optional action) as the
    /// single reusable "no data" component; use instead of a bare blank grid.
    /// </summary>
    public static EmptyPanel CreateEmptyPanel(string message, string? actionText = null, Action? action = null)
    {
        var panel = new EmptyPanel();
        if (!string.IsNullOrWhiteSpace(message))
        {
            panel.SetMessage(message);
        }

        if (!string.IsNullOrWhiteSpace(actionText) && action is not null)
        {
            panel.SetAction(actionText, action);
        }

        return panel;
    }

    // Card styles
    public static void StyleCardPanel(Panel card)
    {
        card.BackColor = Surface;
        card.Padding = new Padding(12, 16, 12, 12);
        card.Margin = new Padding(6);
        card.BorderStyle = BorderStyle.FixedSingle;
    }

    public static void StyleCardValueLabel(Label lbl, Color valueColor)
    {
        lbl.Font = new Font(DefaultFontFamily, 22F, FontStyle.Bold);
        lbl.ForeColor = valueColor;
        lbl.TextAlign = ContentAlignment.MiddleCenter;
        lbl.Height = 48;
        lbl.Dock = DockStyle.Top;
        lbl.AutoEllipsis = true;
        lbl.UseCompatibleTextRendering = false;
    }

    public static void StyleCardCaptionLabel(Label lbl)
    {
        lbl.Font = new Font(DefaultFontFamily, 9.5F, FontStyle.Bold);
        lbl.ForeColor = TextSecondary;
        lbl.TextAlign = ContentAlignment.MiddleCenter;
        lbl.Height = 32;
        lbl.Dock = DockStyle.Top;
    }

    // Grid styles (DataGridView helpers)
    public static void ApplyGridDefaults(DataGridView dgv)
    {
        dgv.AutoGenerateColumns = false;
        dgv.EnableHeadersVisualStyles = false;
        dgv.RightToLeft = RightToLeft.Yes;
        dgv.BackgroundColor = Surface;
        dgv.GridColor = MutedBorder;
        dgv.BorderStyle = BorderStyle.FixedSingle;

        dgv.RowTemplate.Height = GridRowHeight;
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgv.ColumnHeadersHeight = GridHeaderHeight;

        dgv.ColumnHeadersDefaultCellStyle.BackColor = SurfaceAlt;
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font(DefaultFontFamily, 9F, FontStyle.Bold);
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(4, 1, 4, 1);

        dgv.DefaultCellStyle.Font = BodyFont(9F);
        dgv.DefaultCellStyle.ForeColor = TextPrimary;
        dgv.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
        dgv.DefaultCellStyle.SelectionBackColor = Accent;
        dgv.DefaultCellStyle.SelectionForeColor = Color.White;

        dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = RowAlternate };

        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;
    }

    // Status indicators
    public static Color StatusColorActive => Success;
    public static Color StatusColorInactive => Danger;
    public static Color StatusColorWarning => Warning;

    /// <summary>Sets the text and a semantic accent color of an in-screen status label.</summary>
    public static void SetStatusLabel(Label label, UiStatusState state, string text)
    {
        label.Text = text;
        label.ForeColor = state switch
        {
            UiStatusState.Success => Success,
            UiStatusState.Warning => Warning,
            UiStatusState.Error => Danger,
            _ => TextSecondary
        };
    }

    // Misc
    public static void EnsureRightToLeft(Control c)
    {
        if (c is null) return;
        c.RightToLeft = RightToLeft.Yes;
        if (c is Form f)
        {
            f.RightToLeftLayout = true;
        }
        foreach (Control child in c.Controls.Cast<Control>())
        {
            EnsureRightToLeft(child);
        }
    }
}

/// <summary>Semantic state for in-screen status labels.</summary>
public enum UiStatusState
{
    Neutral,
    Loading,
    Success,
    Warning,
    Error
}

/// <summary>
/// Consistent, localized message dialogs for the application (Arabic).
/// </summary>
public static class UiMessages
{
    public static void Information(string message, string title = "معلومات")
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void Warning(string message, string title = "تنبيه")
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void Error(string message, string title = "خطأ")
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static bool Confirm(string message, string title = "تأكيد العملية")
    {
        return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}

/// <summary>
/// Consistent formatting helpers used across the user interface.
/// </summary>
public static class UiText
{
    public static string Money(decimal value) => value.ToString("N2");

    /// <summary>Formats a monetary amount with the Saudi Riyal unit for display in derived labels.</summary>
    public static string Currency(decimal value) => $"{value:N2} ر.س";

    public static string Amount3(decimal value) => value.ToString("N3");

    public static string Date(DateOnly? date) => date.HasValue ? date.Value.ToString("yyyy-MM-dd") : "—";

    public static string DateTime(DateTime? value) => value.HasValue ? value.Value.ToString("yyyy-MM-dd HH:mm") : "—";

    public static string InvoiceStatusName(byte status) => status switch
    {
        1 => "غير مسددة",
        2 => "مسددة جزئياً",
        3 => "مسددة بالكامل",
        4 => "ملغاة",
        _ => "غير معروف"
    };
}

/// <summary>
/// Helpers for hosting forms inside a content panel (main shell pattern).
/// </summary>
public static class UiShell
{
    public static void EmbedAndShow(Form child, Control host)
    {
        ArgumentNullException.ThrowIfNull(child);
        ArgumentNullException.ThrowIfNull(host);

        child.TopLevel = false;
        child.FormBorderStyle = FormBorderStyle.None;
        child.Dock = DockStyle.Fill;
        child.RightToLeft = RightToLeft.Yes;
        if (child is Form f) f.RightToLeftLayout = true;
        host.Controls.Add(child);
        child.Show();
        child.BringToFront();
    }

    public static void Detach(Form child)
    {
        if (child is null)
        {
            return;
        }

        if (child.IsDisposed)
        {
            return;
        }

        if (child.Parent is { } parent)
        {
            parent.Controls.Remove(child);
        }

        child.Hide();
    }
}

/// <summary>
/// Shared loading/busy state helper: disables a set of controls while an
/// operation runs and shows a marquee progress indicator.
/// </summary>
public sealed class UiBusy : IDisposable
{
    private readonly Control[] _controls;
    private readonly ProgressBar? _progressBar;
    private readonly Action<string> _setStatus;
    private readonly string _idleText;
    private bool _busy;

    public UiBusy(IEnumerable<Control> controls, ProgressBar? progressBar, Action<string> setStatus, string idleText)
    {
        _controls = controls.ToArray();
        _progressBar = progressBar;
        _setStatus = setStatus ?? throw new ArgumentNullException(nameof(setStatus));
        _idleText = idleText;
    }

    public UiBusy(IEnumerable<Control> controls, ProgressBar? progressBar, Label statusLabel, string idleText)
        : this(controls, progressBar, message => statusLabel.Text = message, idleText)
    {
    }

    public void Begin(string busyMessage)
    {
        if (_busy)
        {
            return;
        }

        _busy = true;
        foreach (var control in _controls)
        {
            control.Enabled = false;
        }

        if (_progressBar is not null)
        {
            _progressBar.Style = ProgressBarStyle.Marquee;
            _progressBar.Visible = true;
        }

        _setStatus(busyMessage);
    }

    public void End(string? message = null)
    {
        _busy = false;
        foreach (var control in _controls)
        {
            control.Enabled = true;
        }

        if (_progressBar is not null)
        {
            _progressBar.Visible = false;
        }

        _setStatus(message ?? _idleText);
    }

    public void Dispose() => End();
}

/// <summary>
/// Reusable, centered empty-state panel: a large icon glyph, an Arabic message, and an
/// optional single action button. Use it wherever a list/detail area can be empty
/// (instead of a bare blank grid) to keep Empty/Loading/Error states consistent.
/// </summary>
public sealed class EmptyPanel : Panel
{
    private readonly Label _icon;
    private readonly Label _message;
    private readonly Button _action;

    public EmptyPanel()
    {
        AutoSize = false;
        Dock = DockStyle.Fill;
        BackColor = UiTheme.Surface;
        Padding = new Padding(UiTheme.SpacingLarge);

        _action = new Button { Text = string.Empty, Visible = false, Height = UiTheme.SecondaryButtonHeight, AutoSize = false, TabStop = false };
        UiTheme.StyleSecondaryButton(_action, 9);
        _action.Margin = new Padding(0, 12, 0, 0);

        _message = new Label
        {
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = UiTheme.BodyFont(10F),
            ForeColor = UiTheme.TextSecondary,
            Height = 60,
            Dock = DockStyle.Top,
            UseCompatibleTextRendering = false
        };

        _icon = new Label
        {
            Dock = DockStyle.Top,
            Height = 56,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = UiTheme.TitleFont(30F),
            ForeColor = UiTheme.MutedBorder,
            Text = "\u2A23",
            UseCompatibleTextRendering = true
        };

        var stack = new FlowLayoutPanel
        {
            Dock = DockStyle.None,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = UiTheme.Surface,
            Padding = new Padding(0)
        };
        stack.Controls.Add(_icon);
        stack.Controls.Add(_message);
        stack.Controls.Add(_action);

        Controls.Add(stack);
        stack.Anchor = AnchorStyles.None;
        stack.Left = 0;
        stack.Top = 0;

        Resize += (s, e) => CenterStack(stack);
        UiTheme.EnsureRightToLeft(this);
    }

    private void CenterStack(Control stack)
    {
        stack.Left = Math.Max(0, (ClientSize.Width - stack.Width) / 2);
        stack.Top = Math.Max(0, (ClientSize.Height - stack.Height) / 2);
    }

    /// <summary>Sets the Arabic empty-state message shown to the user.</summary>
    public void SetMessage(string message)
    {
        _message.Text = message;
        if (_message.Parent is { } p)
        {
            CenterStack(p);
        }
    }

    /// <summary>Sets an optional action button; pass null to hide the button.</summary>
    public void SetAction(string? text, Action? action)
    {
        var hasAction = !string.IsNullOrWhiteSpace(text) && action is not null;
        _action.Visible = hasAction;
        if (!hasAction)
        {
            return;
        }

        _action.Text = text!;
        _action.Click -= OnActionClick;
        _action.Click += OnActionClick;
        _onAction = action;
    }

    private Action? _onAction;
    private void OnActionClick(object? sender, EventArgs e) => _onAction?.Invoke();
}