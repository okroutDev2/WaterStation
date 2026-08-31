namespace WaterStation.Forms;

partial class AddMeterForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblHeading = new Label();
        fieldsPanel = new TableLayoutPanel();
        lblCustNumberCaption = new Label();
        txtCustomerNumber = new TextBox();
        lblCustNameCaption = new Label();
        txtCustomerName = new TextBox();
        lblBranchCaption = new Label();
        cmbBranch = new ComboBox();
        lblAreaCaption = new Label();
        cmbArea = new ComboBox();
        lblTypeCaption = new Label();
        cmbMeterType = new ComboBox();
        lblDirectionCaption = new Label();
        lblReadingDirectionValue = new Label();
        lblDateCaption = new Label();
        dtpInstallationDate = new DateTimePicker();
        lblReadingCaption = new Label();
        nudInstallationReading = new NumericUpDown();
        lblNotesCaption = new Label();
        lblNotesCount = new Label();
        txtNotes = new TextBox();
        reviewPanel = new Panel();
        lblReviewTitle = new Label();
        lblReviewSummary = new Label();
        pnlButtons = new Panel();
        flowButtons = new FlowLayoutPanel();
        btnSave = new Button();
        btnCancel = new Button();
        tspbSaving = new ProgressBar();
        lblStatus = new Label();
        fieldsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudInstallationReading).BeginInit();
        reviewPanel.SuspendLayout();
        pnlButtons.SuspendLayout();
        flowButtons.SuspendLayout();
        SuspendLayout();
        // 
        // lblHeading
        // 
        lblHeading.AutoSize = true;
        lblHeading.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblHeading.ForeColor = Color.FromArgb(11, 37, 64);
        lblHeading.Location = new Point(1, 2);
        lblHeading.Margin = new Padding(0);
        lblHeading.Name = "lblHeading";
        lblHeading.Size = new Size(150, 28);
        lblHeading.TabIndex = 0;
        lblHeading.Text = "إضافة عداد جديد";
        // 
        // fieldsPanel
        // 
        fieldsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        fieldsPanel.BackColor = Color.White;
        fieldsPanel.ColumnCount = 5;
        fieldsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
        fieldsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 18F));
        fieldsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 2F));
        fieldsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
        fieldsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 18F));
        fieldsPanel.Controls.Add(lblCustNumberCaption, 0, 0);
        fieldsPanel.Controls.Add(txtCustomerNumber, 1, 0);
        fieldsPanel.Controls.Add(lblCustNameCaption, 3, 0);
        fieldsPanel.Controls.Add(txtCustomerName, 4, 0);
        fieldsPanel.Controls.Add(lblBranchCaption, 0, 1);
        fieldsPanel.Controls.Add(cmbBranch, 1, 1);
        fieldsPanel.Controls.Add(lblAreaCaption, 3, 1);
        fieldsPanel.Controls.Add(cmbArea, 4, 1);
        fieldsPanel.Controls.Add(lblTypeCaption, 0, 2);
        fieldsPanel.Controls.Add(cmbMeterType, 1, 2);
        fieldsPanel.Controls.Add(lblDirectionCaption, 3, 2);
        fieldsPanel.Controls.Add(lblReadingDirectionValue, 4, 2);
        fieldsPanel.Controls.Add(lblDateCaption, 0, 3);
        fieldsPanel.Controls.Add(dtpInstallationDate, 1, 3);
        fieldsPanel.Controls.Add(lblReadingCaption, 3, 3);
        fieldsPanel.Controls.Add(nudInstallationReading, 4, 3);
        fieldsPanel.Controls.Add(lblNotesCaption, 0, 4);
        fieldsPanel.Controls.Add(lblNotesCount, 4, 4);
        fieldsPanel.Controls.Add(txtNotes, 0, 5);
        fieldsPanel.Location = new Point(2, 10);
        fieldsPanel.Margin = new Padding(0, 1, 0, 1);
        fieldsPanel.Name = "fieldsPanel";
        fieldsPanel.Padding = new Padding(1);
        fieldsPanel.RightToLeft = RightToLeft.Yes;
        fieldsPanel.RowCount = 6;
        fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 4F));
        fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 12F));
        fieldsPanel.Size = new Size(686, 190);
        fieldsPanel.TabIndex = 1;
        // 
        // lblCustNumberCaption
        // 
        lblCustNumberCaption.Dock = DockStyle.Fill;
        lblCustNumberCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustNumberCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblCustNumberCaption.Location = new Point(677, 1);
        lblCustNumberCaption.Margin = new Padding(0);
        lblCustNumberCaption.Name = "lblCustNumberCaption";
        lblCustNumberCaption.Size = new Size(8, 8);
        lblCustNumberCaption.TabIndex = 100;
        lblCustNumberCaption.Text = "رقم العميل";
        lblCustNumberCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtCustomerNumber
        // 
        txtCustomerNumber.AccessibleName = "رقم العميل";
        txtCustomerNumber.Dock = DockStyle.Fill;
        txtCustomerNumber.Font = new Font("Segoe UI", 9.5F);
        txtCustomerNumber.Location = new Point(660, 1);
        txtCustomerNumber.Margin = new Padding(0, 0, 1, 0);
        txtCustomerNumber.MaxLength = 30;
        txtCustomerNumber.Name = "txtCustomerNumber";
        txtCustomerNumber.Size = new Size(17, 29);
        txtCustomerNumber.TabIndex = 1;
        // 
        // lblCustNameCaption
        // 
        lblCustNameCaption.Dock = DockStyle.Fill;
        lblCustNameCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustNameCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblCustNameCaption.Location = new Point(649, 1);
        lblCustNameCaption.Margin = new Padding(0);
        lblCustNameCaption.Name = "lblCustNameCaption";
        lblCustNameCaption.Size = new Size(8, 8);
        lblCustNameCaption.TabIndex = 101;
        lblCustNameCaption.Text = "اسم العميل";
        lblCustNameCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtCustomerName
        // 
        txtCustomerName.AccessibleName = "اسم العميل";
        txtCustomerName.Dock = DockStyle.Fill;
        txtCustomerName.Font = new Font("Segoe UI", 9.5F);
        txtCustomerName.Location = new Point(2, 1);
        txtCustomerName.Margin = new Padding(0, 0, 1, 0);
        txtCustomerName.MaxLength = 250;
        txtCustomerName.Name = "txtCustomerName";
        txtCustomerName.Size = new Size(647, 29);
        txtCustomerName.TabIndex = 2;
        // 
        // lblBranchCaption
        // 
        lblBranchCaption.Dock = DockStyle.Fill;
        lblBranchCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBranchCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblBranchCaption.Location = new Point(677, 9);
        lblBranchCaption.Margin = new Padding(0);
        lblBranchCaption.Name = "lblBranchCaption";
        lblBranchCaption.Size = new Size(8, 8);
        lblBranchCaption.TabIndex = 102;
        lblBranchCaption.Text = "الفرع";
        lblBranchCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cmbBranch
        // 
        cmbBranch.AccessibleName = "الفرع";
        cmbBranch.Dock = DockStyle.Fill;
        cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBranch.Font = new Font("Segoe UI", 9.5F);
        cmbBranch.Location = new Point(660, 9);
        cmbBranch.Margin = new Padding(0, 0, 1, 0);
        cmbBranch.Name = "cmbBranch";
        cmbBranch.Size = new Size(17, 29);
        cmbBranch.TabIndex = 3;
        // 
        // lblAreaCaption
        // 
        lblAreaCaption.Dock = DockStyle.Fill;
        lblAreaCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblAreaCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblAreaCaption.Location = new Point(649, 9);
        lblAreaCaption.Margin = new Padding(0);
        lblAreaCaption.Name = "lblAreaCaption";
        lblAreaCaption.Size = new Size(8, 8);
        lblAreaCaption.TabIndex = 103;
        lblAreaCaption.Text = "المنطقة";
        lblAreaCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cmbArea
        // 
        cmbArea.AccessibleName = "المنطقة";
        cmbArea.Dock = DockStyle.Fill;
        cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbArea.Font = new Font("Segoe UI", 9.5F);
        cmbArea.Location = new Point(2, 9);
        cmbArea.Margin = new Padding(0, 0, 1, 0);
        cmbArea.Name = "cmbArea";
        cmbArea.Size = new Size(647, 29);
        cmbArea.TabIndex = 4;
        // 
        // lblTypeCaption
        // 
        lblTypeCaption.Dock = DockStyle.Fill;
        lblTypeCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblTypeCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblTypeCaption.Location = new Point(677, 17);
        lblTypeCaption.Margin = new Padding(0);
        lblTypeCaption.Name = "lblTypeCaption";
        lblTypeCaption.Size = new Size(8, 8);
        lblTypeCaption.TabIndex = 104;
        lblTypeCaption.Text = "نوع العداد";
        lblTypeCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // cmbMeterType
        // 
        cmbMeterType.AccessibleName = "نوع العداد";
        cmbMeterType.Dock = DockStyle.Fill;
        cmbMeterType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMeterType.Font = new Font("Segoe UI", 9.5F);
        cmbMeterType.Location = new Point(660, 17);
        cmbMeterType.Margin = new Padding(0, 0, 1, 0);
        cmbMeterType.Name = "cmbMeterType";
        cmbMeterType.Size = new Size(17, 29);
        cmbMeterType.TabIndex = 5;
        // 
        // lblDirectionCaption
        // 
        lblDirectionCaption.Dock = DockStyle.Fill;
        lblDirectionCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDirectionCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblDirectionCaption.Location = new Point(649, 17);
        lblDirectionCaption.Margin = new Padding(0);
        lblDirectionCaption.Name = "lblDirectionCaption";
        lblDirectionCaption.Size = new Size(8, 8);
        lblDirectionCaption.TabIndex = 105;
        lblDirectionCaption.Text = "اتجاه القراءة";
        lblDirectionCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblReadingDirectionValue
        // 
        lblReadingDirectionValue.Dock = DockStyle.Fill;
        lblReadingDirectionValue.Font = new Font("Segoe UI", 9.5F);
        lblReadingDirectionValue.ForeColor = Color.FromArgb(107, 114, 128);
        lblReadingDirectionValue.Location = new Point(2, 17);
        lblReadingDirectionValue.Margin = new Padding(0, 0, 1, 0);
        lblReadingDirectionValue.Name = "lblReadingDirectionValue";
        lblReadingDirectionValue.Size = new Size(647, 8);
        lblReadingDirectionValue.TabIndex = 106;
        lblReadingDirectionValue.Text = "—";
        lblReadingDirectionValue.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblDateCaption
        // 
        lblDateCaption.Dock = DockStyle.Fill;
        lblDateCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDateCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblDateCaption.Location = new Point(677, 25);
        lblDateCaption.Margin = new Padding(0);
        lblDateCaption.Name = "lblDateCaption";
        lblDateCaption.Size = new Size(8, 8);
        lblDateCaption.TabIndex = 106;
        lblDateCaption.Text = "تاريخ التركيب";
        lblDateCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // dtpInstallationDate
        // 
        dtpInstallationDate.AccessibleName = "تاريخ التركيب";
        dtpInstallationDate.CustomFormat = "yyyy-MM-dd";
        dtpInstallationDate.Dock = DockStyle.Fill;
        dtpInstallationDate.Font = new Font("Segoe UI", 9.5F);
        dtpInstallationDate.Format = DateTimePickerFormat.Custom;
        dtpInstallationDate.Location = new Point(660, 25);
        dtpInstallationDate.Margin = new Padding(0, 0, 1, 0);
        dtpInstallationDate.Name = "dtpInstallationDate";
        dtpInstallationDate.Size = new Size(17, 29);
        dtpInstallationDate.TabIndex = 6;
        // 
        // lblReadingCaption
        // 
        lblReadingCaption.Dock = DockStyle.Fill;
        lblReadingCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblReadingCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblReadingCaption.Location = new Point(649, 25);
        lblReadingCaption.Margin = new Padding(0);
        lblReadingCaption.Name = "lblReadingCaption";
        lblReadingCaption.Size = new Size(8, 8);
        lblReadingCaption.TabIndex = 107;
        lblReadingCaption.Text = "قراءة التركيب";
        lblReadingCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // nudInstallationReading
        // 
        nudInstallationReading.AccessibleName = "قراءة التركيب";
        nudInstallationReading.DecimalPlaces = 3;
        nudInstallationReading.Dock = DockStyle.Fill;
        nudInstallationReading.Font = new Font("Segoe UI", 9.5F);
        nudInstallationReading.Location = new Point(2, 25);
        nudInstallationReading.Margin = new Padding(0, 0, 1, 0);
        nudInstallationReading.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
        nudInstallationReading.Name = "nudInstallationReading";
        nudInstallationReading.Size = new Size(647, 29);
        nudInstallationReading.TabIndex = 7;
        // 
        // lblNotesCaption
        // 
        fieldsPanel.SetColumnSpan(lblNotesCaption, 4);
        lblNotesCaption.Dock = DockStyle.Fill;
        lblNotesCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotesCaption.ForeColor = Color.FromArgb(11, 37, 64);
        lblNotesCaption.Location = new Point(649, 33);
        lblNotesCaption.Margin = new Padding(0);
        lblNotesCaption.Name = "lblNotesCaption";
        lblNotesCaption.Size = new Size(36, 4);
        lblNotesCaption.TabIndex = 108;
        lblNotesCaption.Text = "ملاحظات (اختياري)";
        lblNotesCaption.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblNotesCount
        // 
        lblNotesCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblNotesCount.Font = new Font("Segoe UI", 8F);
        lblNotesCount.ForeColor = Color.FromArgb(107, 114, 128);
        lblNotesCount.Location = new Point(1, 33);
        lblNotesCount.Margin = new Padding(0);
        lblNotesCount.Name = "lblNotesCount";
        lblNotesCount.Size = new Size(8, 4);
        lblNotesCount.TabIndex = 109;
        lblNotesCount.Text = "0/4000";
        lblNotesCount.TextAlign = ContentAlignment.MiddleRight;
        // 
        // txtNotes
        // 
        txtNotes.AccessibleName = "ملاحظات تركيب العداد";
        fieldsPanel.SetColumnSpan(txtNotes, 5);
        txtNotes.Dock = DockStyle.Fill;
        txtNotes.Font = new Font("Segoe UI", 9.5F);
        txtNotes.Location = new Point(2, 37);
        txtNotes.Margin = new Padding(0, 0, 1, 0);
        txtNotes.MaxLength = 4000;
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(683, 152);
        txtNotes.TabIndex = 8;
        // 
        // reviewPanel
        // 
        reviewPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        reviewPanel.BackColor = Color.FromArgb(245, 247, 250);
        reviewPanel.BorderStyle = BorderStyle.FixedSingle;
        reviewPanel.Controls.Add(lblReviewTitle);
        reviewPanel.Controls.Add(lblReviewSummary);
        reviewPanel.Location = new Point(21, 241);
        reviewPanel.Margin = new Padding(0, 1, 0, 1);
        reviewPanel.Name = "reviewPanel";
        reviewPanel.Size = new Size(687, 33);
        reviewPanel.TabIndex = 2;
        // 
        // lblReviewTitle
        // 
        lblReviewTitle.AutoSize = true;
        lblReviewTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblReviewTitle.ForeColor = Color.FromArgb(11, 37, 64);
        lblReviewTitle.Location = new Point(1, 2);
        lblReviewTitle.Margin = new Padding(0);
        lblReviewTitle.Name = "lblReviewTitle";
        lblReviewTitle.Size = new Size(195, 23);
        lblReviewTitle.TabIndex = 110;
        lblReviewTitle.Text = "مراجعة البيانات قبل الحفظ";
        // 
        // lblReviewSummary
        // 
        lblReviewSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblReviewSummary.Font = new Font("Segoe UI", 9.5F);
        lblReviewSummary.ForeColor = Color.FromArgb(107, 114, 128);
        lblReviewSummary.Location = new Point(1, 8);
        lblReviewSummary.Margin = new Padding(0);
        lblReviewSummary.Name = "lblReviewSummary";
        lblReviewSummary.Size = new Size(683, 22);
        lblReviewSummary.TabIndex = 111;
        lblReviewSummary.TextAlign = ContentAlignment.TopRight;
        // 
        // pnlButtons
        // 
        pnlButtons.BackColor = Color.FromArgb(245, 247, 250);
        pnlButtons.Controls.Add(flowButtons);
        pnlButtons.Dock = DockStyle.Bottom;
        pnlButtons.Location = new Point(0, 287);
        pnlButtons.Margin = new Padding(0, 1, 0, 1);
        pnlButtons.Name = "pnlButtons";
        pnlButtons.Size = new Size(777, 43);
        pnlButtons.TabIndex = 3;
        // 
        // flowButtons
        // 
        flowButtons.Controls.Add(btnSave);
        flowButtons.Controls.Add(btnCancel);
        flowButtons.Controls.Add(tspbSaving);
        flowButtons.Controls.Add(lblStatus);
        flowButtons.Dock = DockStyle.Fill;
        flowButtons.Location = new Point(0, 0);
        flowButtons.Margin = new Padding(0, 1, 0, 1);
        flowButtons.Name = "flowButtons";
        flowButtons.Padding = new Padding(1, 2, 1, 1);
        flowButtons.RightToLeft = RightToLeft.Yes;
        flowButtons.Size = new Size(777, 43);
        flowButtons.TabIndex = 0;
        flowButtons.WrapContents = false;
        // 
        // btnSave
        // 
        btnSave.AccessibleName = "حفظ العداد";
        btnSave.Location = new Point(686, 3);
        btnSave.Margin = new Padding(0, 1, 0, 1);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(89, 39);
        btnSave.TabIndex = 9;
        btnSave.Text = "حفظ العداد";
        btnSave.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.AccessibleName = "إلغاء";
        btnCancel.Location = new Point(596, 3);
        btnCancel.Margin = new Padding(0, 1, 0, 1);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(90, 40);
        btnCancel.TabIndex = 10;
        btnCancel.Text = "إلغاء";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // tspbSaving
        // 
        tspbSaving.Location = new Point(437, 6);
        tspbSaving.Margin = new Padding(1, 4, 1, 0);
        tspbSaving.Name = "tspbSaving";
        tspbSaving.Size = new Size(158, 36);
        tspbSaving.Style = ProgressBarStyle.Marquee;
        tspbSaving.TabIndex = 11;
        tspbSaving.Visible = false;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 9F);
        lblStatus.ForeColor = Color.FromArgb(107, 114, 128);
        lblStatus.Location = new Point(435, 4);
        lblStatus.Margin = new Padding(1, 2, 0, 0);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(0, 20);
        lblStatus.TabIndex = 12;
        // 
        // AddMeterForm
        // 
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(777, 330);
        Controls.Add(reviewPanel);
        Controls.Add(fieldsPanel);
        Controls.Add(lblHeading);
        Controls.Add(pnlButtons);
        Font = new Font("Segoe UI", 9F);
        Margin = new Padding(0, 1, 0, 1);
        MinimumSize = new Size(73, 150);
        Name = "AddMeterForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "إضافة عداد جديد";
        fieldsPanel.ResumeLayout(false);
        fieldsPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudInstallationReading).EndInit();
        reviewPanel.ResumeLayout(false);
        reviewPanel.PerformLayout();
        pnlButtons.ResumeLayout(false);
        flowButtons.ResumeLayout(false);
        flowButtons.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblHeading;
    private TableLayoutPanel fieldsPanel;
    private Label lblCustNumberCaption;
    private TextBox txtCustomerNumber;
    private Label lblCustNameCaption;
    private TextBox txtCustomerName;
    private Label lblBranchCaption;
    private ComboBox cmbBranch;
    private Label lblAreaCaption;
    private ComboBox cmbArea;
    private Label lblTypeCaption;
    private ComboBox cmbMeterType;
    private Label lblDirectionCaption;
    private Label lblReadingDirectionValue;
    private Label lblDateCaption;
    private DateTimePicker dtpInstallationDate;
    private Label lblReadingCaption;
    private NumericUpDown nudInstallationReading;
    private Label lblNotesCaption;
    private TextBox txtNotes;
    private Label lblNotesCount;
    private Panel reviewPanel;
    private Label lblReviewTitle;
    private Label lblReviewSummary;
    private Panel pnlButtons;
    private FlowLayoutPanel flowButtons;
    private Button btnSave;
    private Button btnCancel;
    private ProgressBar tspbSaving;
    private Label lblStatus;
}