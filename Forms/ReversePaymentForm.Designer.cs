#nullable enable

namespace WaterStation.Forms;

partial class ReversePaymentForm
{
    private System.ComponentModel.IContainer? components = null;

    // Payment Info GroupBox
    private GroupBox grpPaymentInfo = null!;
    private Label lblPaymentIdTitle = null!;
    private Label lblPaymentIdVal = null!;
    private Label lblInvoiceNumTitle = null!;
    private Label lblInvoiceNumVal = null!;
    private Label lblPaymentDateTitle = null!;
    private Label lblPaymentDateVal = null!;
    private Label lblPaymentAmountTitle = null!;
    private Label lblPaymentAmountVal = null!;
    private Label lblPaymentMethodTitle = null!;
    private Label lblPaymentMethodVal = null!;
    private Label lblRefNumTitle = null!;
    private Label lblRefNumVal = null!;
    private Label lblIsReversedTitle = null!;
    private Label lblIsReversedVal = null!;

    // Warning GroupBox
    private GroupBox grpWarning = null!;
    private Label lblWarning = null!;

    // Reason GroupBox
    private GroupBox grpReason = null!;
    private Label lblReason = null!;
    private TextBox txtReason = null!;
    private Label lblCharCount = null!;

    // Actions & Status
    private Panel pnlActions = null!;
    private Button btnConfirm = null!;
    private Button btnCancel = null!;
    private ProgressBar pbLoading = null!;
    private Label lblStatus = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        grpPaymentInfo = new GroupBox();
        lblPaymentIdTitle = new Label();
        lblPaymentIdVal = new Label();
        lblInvoiceNumTitle = new Label();
        lblInvoiceNumVal = new Label();
        lblPaymentDateTitle = new Label();
        lblPaymentDateVal = new Label();
        lblPaymentAmountTitle = new Label();
        lblPaymentAmountVal = new Label();
        lblPaymentMethodTitle = new Label();
        lblPaymentMethodVal = new Label();
        lblRefNumTitle = new Label();
        lblRefNumVal = new Label();
        lblIsReversedTitle = new Label();
        lblIsReversedVal = new Label();

        grpWarning = new GroupBox();
        lblWarning = new Label();

        grpReason = new GroupBox();
        lblReason = new Label();
        txtReason = new TextBox();
        lblCharCount = new Label();

        pnlActions = new Panel();
        btnConfirm = new Button();
        btnCancel = new Button();
        pbLoading = new ProgressBar();
        lblStatus = new Label();

        grpPaymentInfo.SuspendLayout();
        grpWarning.SuspendLayout();
        grpReason.SuspendLayout();
        pnlActions.SuspendLayout();
        SuspendLayout();

        // 
        // grpPaymentInfo
        // 
        grpPaymentInfo.Controls.Add(lblIsReversedVal);
        grpPaymentInfo.Controls.Add(lblIsReversedTitle);
        grpPaymentInfo.Controls.Add(lblRefNumVal);
        grpPaymentInfo.Controls.Add(lblRefNumTitle);
        grpPaymentInfo.Controls.Add(lblPaymentMethodVal);
        grpPaymentInfo.Controls.Add(lblPaymentMethodTitle);
        grpPaymentInfo.Controls.Add(lblPaymentAmountVal);
        grpPaymentInfo.Controls.Add(lblPaymentAmountTitle);
        grpPaymentInfo.Controls.Add(lblInvoiceNumVal);
        grpPaymentInfo.Controls.Add(lblInvoiceNumTitle);
        grpPaymentInfo.Controls.Add(lblPaymentDateVal);
        grpPaymentInfo.Controls.Add(lblPaymentDateTitle);
        grpPaymentInfo.Controls.Add(lblPaymentIdVal);
        grpPaymentInfo.Controls.Add(lblPaymentIdTitle);
        grpPaymentInfo.Dock = DockStyle.Top;
        grpPaymentInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpPaymentInfo.Location = new Point(12, 12);
        grpPaymentInfo.Name = "grpPaymentInfo";
        grpPaymentInfo.Padding = new Padding(8);
        grpPaymentInfo.Size = new Size(616, 175);
        grpPaymentInfo.TabIndex = 0;
        grpPaymentInfo.TabStop = false;
        grpPaymentInfo.Text = "بيانات الدفعة المراد عكسها";

        // Row 1: PaymentId | PaymentDate
        lblPaymentIdTitle.AutoSize = true;
        lblPaymentIdTitle.Font = new Font("Segoe UI", 9F);
        lblPaymentIdTitle.Location = new Point(500, 30);
        lblPaymentIdTitle.Name = "lblPaymentIdTitle";
        lblPaymentIdTitle.Size = new Size(71, 15);
        lblPaymentIdTitle.TabIndex = 0;
        lblPaymentIdTitle.Text = "رقم الدفعة:";
        lblPaymentIdVal.AutoSize = true;
        lblPaymentIdVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblPaymentIdVal.ForeColor = Color.MidnightBlue;
        lblPaymentIdVal.Location = new Point(395, 28);
        lblPaymentIdVal.Name = "lblPaymentIdVal";
        lblPaymentIdVal.Size = new Size(24, 19);
        lblPaymentIdVal.TabIndex = 1;
        lblPaymentIdVal.Text = "—";
        lblPaymentDateTitle.AutoSize = true;
        lblPaymentDateTitle.Font = new Font("Segoe UI", 9F);
        lblPaymentDateTitle.Location = new Point(165, 30);
        lblPaymentDateTitle.Name = "lblPaymentDateTitle";
        lblPaymentDateTitle.Size = new Size(84, 15);
        lblPaymentDateTitle.TabIndex = 2;
        lblPaymentDateTitle.Text = "تاريخ الدفعة:";
        lblPaymentDateVal.AutoSize = true;
        lblPaymentDateVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblPaymentDateVal.Location = new Point(60, 28);
        lblPaymentDateVal.Name = "lblPaymentDateVal";
        lblPaymentDateVal.Size = new Size(24, 19);
        lblPaymentDateVal.TabIndex = 3;
        lblPaymentDateVal.Text = "—";

        // Row 2: InvoiceNumber | PaymentAmount
        lblInvoiceNumTitle.AutoSize = true;
        lblInvoiceNumTitle.Font = new Font("Segoe UI", 9F);
        lblInvoiceNumTitle.Location = new Point(500, 66);
        lblInvoiceNumTitle.Name = "lblInvoiceNumTitle";
        lblInvoiceNumTitle.Size = new Size(71, 15);
        lblInvoiceNumTitle.TabIndex = 4;
        lblInvoiceNumTitle.Text = "رقم الفاتورة:";
        lblInvoiceNumVal.AutoSize = true;
        lblInvoiceNumVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblInvoiceNumVal.ForeColor = Color.DarkSlateBlue;
        lblInvoiceNumVal.Location = new Point(395, 64);
        lblInvoiceNumVal.Name = "lblInvoiceNumVal";
        lblInvoiceNumVal.Size = new Size(24, 19);
        lblInvoiceNumVal.TabIndex = 5;
        lblInvoiceNumVal.Text = "—";
        lblPaymentAmountTitle.AutoSize = true;
        lblPaymentAmountTitle.Font = new Font("Segoe UI", 9F);
        lblPaymentAmountTitle.Location = new Point(165, 66);
        lblPaymentAmountTitle.Name = "lblPaymentAmountTitle";
        lblPaymentAmountTitle.Size = new Size(91, 15);
        lblPaymentAmountTitle.TabIndex = 6;
        lblPaymentAmountTitle.Text = "مبلغ الدفعة (ر.س):";
        lblPaymentAmountVal.AutoSize = true;
        lblPaymentAmountVal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblPaymentAmountVal.ForeColor = Color.DarkGreen;
        lblPaymentAmountVal.Location = new Point(60, 63);
        lblPaymentAmountVal.Name = "lblPaymentAmountVal";
        lblPaymentAmountVal.Size = new Size(36, 20);
        lblPaymentAmountVal.TabIndex = 7;
        lblPaymentAmountVal.Text = "0.00";

        // Row 3: PaymentMethodName | ReferenceNumber
        lblPaymentMethodTitle.AutoSize = true;
        lblPaymentMethodTitle.Font = new Font("Segoe UI", 9F);
        lblPaymentMethodTitle.Location = new Point(500, 102);
        lblPaymentMethodTitle.Name = "lblPaymentMethodTitle";
        lblPaymentMethodTitle.Size = new Size(82, 15);
        lblPaymentMethodTitle.TabIndex = 8;
        lblPaymentMethodTitle.Text = "طريقة الدفع:";
        lblPaymentMethodVal.AutoSize = true;
        lblPaymentMethodVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblPaymentMethodVal.Location = new Point(395, 100);
        lblPaymentMethodVal.Name = "lblPaymentMethodVal";
        lblPaymentMethodVal.Size = new Size(24, 19);
        lblPaymentMethodVal.TabIndex = 9;
        lblPaymentMethodVal.Text = "—";
        lblRefNumTitle.AutoSize = true;
        lblRefNumTitle.Font = new Font("Segoe UI", 9F);
        lblRefNumTitle.Location = new Point(165, 102);
        lblRefNumTitle.Name = "lblRefNumTitle";
        lblRefNumTitle.Size = new Size(74, 15);
        lblRefNumTitle.TabIndex = 10;
        lblRefNumTitle.Text = "رقم المرجع:";
        lblRefNumVal.AutoSize = true;
        lblRefNumVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblRefNumVal.Location = new Point(60, 100);
        lblRefNumVal.Name = "lblRefNumVal";
        lblRefNumVal.Size = new Size(24, 19);
        lblRefNumVal.TabIndex = 11;
        lblRefNumVal.Text = "—";

        // Row 4: IsReversed
        lblIsReversedTitle.AutoSize = true;
        lblIsReversedTitle.Font = new Font("Segoe UI", 9F);
        lblIsReversedTitle.Location = new Point(500, 138);
        lblIsReversedTitle.Name = "lblIsReversedTitle";
        lblIsReversedTitle.Size = new Size(72, 15);
        lblIsReversedTitle.TabIndex = 12;
        lblIsReversedTitle.Text = "حالة الدفعة:";
        lblIsReversedVal.AutoSize = true;
        lblIsReversedVal.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        lblIsReversedVal.ForeColor = Color.DarkGreen;
        lblIsReversedVal.Location = new Point(395, 136);
        lblIsReversedVal.Name = "lblIsReversedVal";
        lblIsReversedVal.Size = new Size(24, 19);
        lblIsReversedVal.TabIndex = 13;
        lblIsReversedVal.Text = "—";

        // 
        // grpWarning
        // 
        grpWarning.Controls.Add(lblWarning);
        grpWarning.Dock = DockStyle.Top;
        grpWarning.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpWarning.ForeColor = Color.FromArgb(168, 70, 20);
        grpWarning.Location = new Point(12, 195);
        grpWarning.Name = "grpWarning";
        grpWarning.Padding = new Padding(12);
        grpWarning.Size = new Size(616, 110);
        grpWarning.TabIndex = 1;
        grpWarning.TabStop = false;
        grpWarning.Text = "تنبيه هام";

        // lblWarning
        lblWarning.AutoSize = true;
        lblWarning.Font = new Font("Segoe UI", 9.5F);
        lblWarning.ForeColor = Color.FromArgb(168, 70, 20);
        lblWarning.Location = new Point(12, 24);
        lblWarning.MaximumSize = new Size(580, 0);
        lblWarning.Name = "lblWarning";
        lblWarning.TabIndex = 0;
        lblWarning.Text = "سيؤدي تأكيد هذه العملية إلى ما يلي:\r\n• عكس الدفعة المحددة بالكامل.\r\n• إعادة احتساب حالة الفاتورة ورصيدها تلقائيًا.\r\n• لا يمكن تنفيذ العكس مرة ثانية لنفس الدفعة.";

        // 
        // grpReason
        // 
        grpReason.Controls.Add(lblCharCount);
        grpReason.Controls.Add(txtReason);
        grpReason.Controls.Add(lblReason);
        grpReason.Dock = DockStyle.Top;
        grpReason.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpReason.Location = new Point(12, 313);
        grpReason.Name = "grpReason";
        grpReason.Padding = new Padding(12);
        grpReason.Size = new Size(616, 120);
        grpReason.TabIndex = 2;
        grpReason.TabStop = false;
        grpReason.Text = "سبب عكس الدفعة";

        // lblReason
        lblReason.AutoSize = true;
        lblReason.Font = new Font("Segoe UI", 9.5F);
        lblReason.Location = new Point(500, 32);
        lblReason.Name = "lblReason";
        lblReason.Size = new Size(100, 17);
        lblReason.TabIndex = 0;
        lblReason.Text = "سبب العكس (مطلوب): *";

        // txtReason
        txtReason.Font = new Font("Segoe UI", 9.5F);
        txtReason.Location = new Point(230, 28);
        txtReason.MaxLength = 1000;
        txtReason.Multiline = true;
        txtReason.Name = "txtReason";
        txtReason.PlaceholderText = "أدخل سبب عكس الدفعة بوضوح (إلزامي)...";
        txtReason.ScrollBars = ScrollBars.Vertical;
        txtReason.Size = new Size(360, 62);
        txtReason.TabIndex = 1;

        // lblCharCount
        lblCharCount.AutoSize = true;
        lblCharCount.Font = new Font("Segoe UI", 8.5F);
        lblCharCount.ForeColor = Color.DimGray;
        lblCharCount.Location = new Point(60, 94);
        lblCharCount.Name = "lblCharCount";
        lblCharCount.Size = new Size(60, 15);
        lblCharCount.TabIndex = 2;
        lblCharCount.Text = "0 / 1000";

        // 
        // pnlActions
        // 
        pnlActions.Controls.Add(lblStatus);
        pnlActions.Controls.Add(pbLoading);
        pnlActions.Controls.Add(btnCancel);
        pnlActions.Controls.Add(btnConfirm);
        pnlActions.Dock = DockStyle.Bottom;
        pnlActions.Location = new Point(12, 448);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(616, 85);
        pnlActions.TabIndex = 3;

        // btnConfirm
        btnConfirm.BackColor = Color.FromArgb(156, 47, 28);
        btnConfirm.FlatStyle = FlatStyle.Flat;
        btnConfirm.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnConfirm.ForeColor = Color.White;
        btnConfirm.Location = new Point(300, 16);
        btnConfirm.Name = "btnConfirm";
        btnConfirm.Size = new Size(205, 42);
        btnConfirm.TabIndex = 0;
        btnConfirm.Text = "تأكيد عكس الدفعة";
        btnConfirm.UseVisualStyleBackColor = false;

        // btnCancel
        btnCancel.BackColor = Color.FromArgb(240, 240, 240);
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F);
        btnCancel.Location = new Point(150, 16);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(130, 42);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "إلغاء";
        btnCancel.UseVisualStyleBackColor = true;

        // pbLoading
        pbLoading.Location = new Point(150, 64);
        pbLoading.Name = "pbLoading";
        pbLoading.Size = new Size(350, 8);
        pbLoading.Style = ProgressBarStyle.Marquee;
        pbLoading.TabIndex = 2;
        pbLoading.Visible = false;

        // lblStatus
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.5F);
        lblStatus.ForeColor = Color.DimGray;
        lblStatus.Location = new Point(15, 34);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(40, 15);
        lblStatus.TabIndex = 3;
        lblStatus.Text = "جاهز";

        // 
        // ReversePaymentForm
        // 
        AcceptButton = btnConfirm;
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(640, 545);
        Controls.Add(pnlActions);
        Controls.Add(grpReason);
        Controls.Add(grpWarning);
        Controls.Add(grpPaymentInfo);
        Font = new Font("Segoe UI", 9.5F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReversePaymentForm";
        Padding = new Padding(12);
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "عكس الدفعة - WaterStation";

        grpPaymentInfo.ResumeLayout(false);
        grpPaymentInfo.PerformLayout();
        grpWarning.ResumeLayout(false);
        grpWarning.PerformLayout();
        grpReason.ResumeLayout(false);
        grpReason.PerformLayout();
        pnlActions.ResumeLayout(false);
        pnlActions.PerformLayout();
        ResumeLayout(false);
    }
}