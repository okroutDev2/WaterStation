namespace WaterStation.Forms;

partial class MeterReadingForm
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
        grpMeterInfo = new GroupBox();
        lblReadingDirectionVal = new Label();
        lblReadingDirectionTitle = new Label();
        lblMeterTypeVal = new Label();
        lblMeterTypeTitle = new Label();
        lblAreaVal = new Label();
        lblAreaTitle = new Label();
        lblBranchVal = new Label();
        lblBranchTitle = new Label();
        lblCustomerNameVal = new Label();
        lblCustomerNameTitle = new Label();
        lblCustomerNumVal = new Label();
        lblCustomerNumTitle = new Label();
        lblMeterNumVal = new Label();
        lblMeterNumTitle = new Label();
        grpLastReading = new GroupBox();
        lblLastIsReverseVal = new Label();
        lblLastIsReverseTitle = new Label();
        lblLastConsumptionVal = new Label();
        lblLastConsumptionTitle = new Label();
        lblLastReadingValueVal = new Label();
        lblLastReadingValueTitle = new Label();
        lblLastReadingDateVal = new Label();
        lblLastReadingDateTitle = new Label();
        lblInstallationReadingVal = new Label();
        lblInstallationReadingTitle = new Label();
        grpNewReading = new GroupBox();
        lblNotesCount = new Label();
        txtNotes = new TextBox();
        lblNotes = new Label();
        nudReadingValue = new NumericUpDown();
        lblReadingValue = new Label();
        dtpReadingDate = new DateTimePicker();
        lblReadingDate = new Label();
        pnlActions = new Panel();
        lblStatus = new Label();
        pbLoading = new ProgressBar();
        btnCancel = new Button();
        btnSave = new Button();
        grpMeterInfo.SuspendLayout();
        grpLastReading.SuspendLayout();
        grpNewReading.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudReadingValue).BeginInit();
        pnlActions.SuspendLayout();
        SuspendLayout();
        //
        // grpMeterInfo
        //
        grpMeterInfo.Controls.Add(lblReadingDirectionVal);
        grpMeterInfo.Controls.Add(lblReadingDirectionTitle);
        grpMeterInfo.Controls.Add(lblMeterTypeVal);
        grpMeterInfo.Controls.Add(lblMeterTypeTitle);
        grpMeterInfo.Controls.Add(lblAreaVal);
        grpMeterInfo.Controls.Add(lblAreaTitle);
        grpMeterInfo.Controls.Add(lblBranchVal);
        grpMeterInfo.Controls.Add(lblBranchTitle);
        grpMeterInfo.Controls.Add(lblCustomerNameVal);
        grpMeterInfo.Controls.Add(lblCustomerNameTitle);
        grpMeterInfo.Controls.Add(lblCustomerNumVal);
        grpMeterInfo.Controls.Add(lblCustomerNumTitle);
        grpMeterInfo.Controls.Add(lblMeterNumVal);
        grpMeterInfo.Controls.Add(lblMeterNumTitle);
        grpMeterInfo.Dock = DockStyle.Top;
        grpMeterInfo.Font = new Font("Segoe UI", 9.5F);
        grpMeterInfo.Location = new Point(12, 12);
        grpMeterInfo.Name = "grpMeterInfo";
        grpMeterInfo.RightToLeft = RightToLeft.Yes;
        grpMeterInfo.Size = new Size(636, 165);
        grpMeterInfo.TabIndex = 0;
        grpMeterInfo.TabStop = false;
        grpMeterInfo.Text = "معلومات العداد المحدد";
        //
        // lblMeterNumTitle
        //
        lblMeterNumTitle.AutoSize = true;
        lblMeterNumTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMeterNumTitle.Location = new Point(496, 28);
        lblMeterNumTitle.Name = "lblMeterNumTitle";
        lblMeterNumTitle.Size = new Size(128, 17);
        lblMeterNumTitle.TabIndex = 0;
        lblMeterNumTitle.Text = "رقم العداد:";
        lblMeterNumTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblMeterNumVal
        //
        lblMeterNumVal.AutoSize = true;
        lblMeterNumVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMeterNumVal.Location = new Point(385, 28);
        lblMeterNumVal.Name = "lblMeterNumVal";
        lblMeterNumVal.Size = new Size(100, 17);
        lblMeterNumVal.TabIndex = 1;
        lblMeterNumVal.Text = "—";
        lblMeterNumVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblCustomerNumTitle
        //
        lblCustomerNumTitle.AutoSize = true;
        lblCustomerNumTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomerNumTitle.Location = new Point(165, 28);
        lblCustomerNumTitle.Name = "lblCustomerNumTitle";
        lblCustomerNumTitle.Size = new Size(112, 17);
        lblCustomerNumTitle.TabIndex = 2;
        lblCustomerNumTitle.Text = "رقم العميل:";
        lblCustomerNumTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblCustomerNumVal
        //
        lblCustomerNumVal.AutoSize = true;
        lblCustomerNumVal.Font = new Font("Segoe UI", 9.5F);
        lblCustomerNumVal.Location = new Point(60, 28);
        lblCustomerNumVal.Name = "lblCustomerNumVal";
        lblCustomerNumVal.Size = new Size(90, 17);
        lblCustomerNumVal.TabIndex = 3;
        lblCustomerNumVal.Text = "—";
        lblCustomerNumVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblCustomerNameTitle
        //
        lblCustomerNameTitle.AutoSize = true;
        lblCustomerNameTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomerNameTitle.Location = new Point(496, 62);
        lblCustomerNameTitle.Name = "lblCustomerNameTitle";
        lblCustomerNameTitle.Size = new Size(110, 17);
        lblCustomerNameTitle.TabIndex = 4;
        lblCustomerNameTitle.Text = "اسم العميل:";
        lblCustomerNameTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblCustomerNameVal
        //
        lblCustomerNameVal.AutoSize = true;
        lblCustomerNameVal.Font = new Font("Segoe UI", 9.5F);
        lblCustomerNameVal.Location = new Point(385, 62);
        lblCustomerNameVal.Name = "lblCustomerNameVal";
        lblCustomerNameVal.Size = new Size(100, 17);
        lblCustomerNameVal.TabIndex = 5;
        lblCustomerNameVal.Text = "—";
        lblCustomerNameVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblBranchTitle
        //
        lblBranchTitle.AutoSize = true;
        lblBranchTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBranchTitle.Location = new Point(165, 62);
        lblBranchTitle.Name = "lblBranchTitle";
        lblBranchTitle.Size = new Size(92, 17);
        lblBranchTitle.TabIndex = 6;
        lblBranchTitle.Text = "الفرع:";
        lblBranchTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblBranchVal
        //
        lblBranchVal.AutoSize = true;
        lblBranchVal.Font = new Font("Segoe UI", 9.5F);
        lblBranchVal.Location = new Point(60, 62);
        lblBranchVal.Name = "lblBranchVal";
        lblBranchVal.Size = new Size(90, 17);
        lblBranchVal.TabIndex = 7;
        lblBranchVal.Text = "—";
        lblBranchVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblAreaTitle
        //
        lblAreaTitle.AutoSize = true;
        lblAreaTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblAreaTitle.Location = new Point(496, 96);
        lblAreaTitle.Name = "lblAreaTitle";
        lblAreaTitle.Size = new Size(86, 17);
        lblAreaTitle.TabIndex = 8;
        lblAreaTitle.Text = "المنطقة:";
        lblAreaTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblAreaVal
        //
        lblAreaVal.AutoSize = true;
        lblAreaVal.Font = new Font("Segoe UI", 9.5F);
        lblAreaVal.Location = new Point(385, 96);
        lblAreaVal.Name = "lblAreaVal";
        lblAreaVal.Size = new Size(100, 17);
        lblAreaVal.TabIndex = 9;
        lblAreaVal.Text = "—";
        lblAreaVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblMeterTypeTitle
        //
        lblMeterTypeTitle.AutoSize = true;
        lblMeterTypeTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMeterTypeTitle.Location = new Point(165, 96);
        lblMeterTypeTitle.Name = "lblMeterTypeTitle";
        lblMeterTypeTitle.Size = new Size(92, 17);
        lblMeterTypeTitle.TabIndex = 10;
        lblMeterTypeTitle.Text = "نوع العداد:";
        lblMeterTypeTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblMeterTypeVal
        //
        lblMeterTypeVal.AutoSize = true;
        lblMeterTypeVal.Font = new Font("Segoe UI", 9.5F);
        lblMeterTypeVal.Location = new Point(60, 96);
        lblMeterTypeVal.Name = "lblMeterTypeVal";
        lblMeterTypeVal.Size = new Size(90, 17);
        lblMeterTypeVal.TabIndex = 11;
        lblMeterTypeVal.Text = "—";
        lblMeterTypeVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblReadingDirectionTitle
        //
        lblReadingDirectionTitle.AutoSize = true;
        lblReadingDirectionTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblReadingDirectionTitle.Location = new Point(496, 130);
        lblReadingDirectionTitle.Name = "lblReadingDirectionTitle";
        lblReadingDirectionTitle.Size = new Size(128, 17);
        lblReadingDirectionTitle.TabIndex = 12;
        lblReadingDirectionTitle.Text = "اتجاه العداد:";
        lblReadingDirectionTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblReadingDirectionVal
        //
        lblReadingDirectionVal.AutoSize = true;
        lblReadingDirectionVal.Font = new Font("Segoe UI", 9.5F);
        lblReadingDirectionVal.Location = new Point(385, 130);
        lblReadingDirectionVal.Name = "lblReadingDirectionVal";
        lblReadingDirectionVal.Size = new Size(100, 17);
        lblReadingDirectionVal.TabIndex = 13;
        lblReadingDirectionVal.Text = "—";
        lblReadingDirectionVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // grpLastReading
        //
        grpLastReading.Controls.Add(lblLastIsReverseVal);
        grpLastReading.Controls.Add(lblLastIsReverseTitle);
        grpLastReading.Controls.Add(lblLastConsumptionVal);
        grpLastReading.Controls.Add(lblLastConsumptionTitle);
        grpLastReading.Controls.Add(lblLastReadingValueVal);
        grpLastReading.Controls.Add(lblLastReadingValueTitle);
        grpLastReading.Controls.Add(lblLastReadingDateVal);
        grpLastReading.Controls.Add(lblLastReadingDateTitle);
        grpLastReading.Controls.Add(lblInstallationReadingVal);
        grpLastReading.Controls.Add(lblInstallationReadingTitle);
        grpLastReading.Dock = DockStyle.Top;
        grpLastReading.Font = new Font("Segoe UI", 9.5F);
        grpLastReading.Location = new Point(12, 177);
        grpLastReading.Name = "grpLastReading";
        grpLastReading.RightToLeft = RightToLeft.Yes;
        grpLastReading.Size = new Size(636, 140);
        grpLastReading.TabIndex = 1;
        grpLastReading.TabStop = false;
        grpLastReading.Text = "القراءة السابقة";
        //
        // lblInstallationReadingTitle
        //
        lblInstallationReadingTitle.AutoSize = true;
        lblInstallationReadingTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblInstallationReadingTitle.Location = new Point(496, 28);
        lblInstallationReadingTitle.Name = "lblInstallationReadingTitle";
        lblInstallationReadingTitle.Size = new Size(128, 17);
        lblInstallationReadingTitle.TabIndex = 0;
        lblInstallationReadingTitle.Text = "قراءة التركيب:";
        lblInstallationReadingTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblInstallationReadingVal
        //
        lblInstallationReadingVal.AutoSize = true;
        lblInstallationReadingVal.Font = new Font("Segoe UI", 9.5F);
        lblInstallationReadingVal.Location = new Point(385, 28);
        lblInstallationReadingVal.Name = "lblInstallationReadingVal";
        lblInstallationReadingVal.Size = new Size(100, 17);
        lblInstallationReadingVal.TabIndex = 1;
        lblInstallationReadingVal.Text = "—";
        lblInstallationReadingVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblLastReadingDateTitle
        //
        lblLastReadingDateTitle.AutoSize = true;
        lblLastReadingDateTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLastReadingDateTitle.Location = new Point(165, 28);
        lblLastReadingDateTitle.Name = "lblLastReadingDateTitle";
        lblLastReadingDateTitle.Size = new Size(112, 17);
        lblLastReadingDateTitle.TabIndex = 2;
        lblLastReadingDateTitle.Text = "تاريخ آخر قراءة:";
        lblLastReadingDateTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblLastReadingDateVal
        //
        lblLastReadingDateVal.AutoSize = true;
        lblLastReadingDateVal.Font = new Font("Segoe UI", 9.5F);
        lblLastReadingDateVal.Location = new Point(60, 28);
        lblLastReadingDateVal.Name = "lblLastReadingDateVal";
        lblLastReadingDateVal.Size = new Size(90, 17);
        lblLastReadingDateVal.TabIndex = 3;
        lblLastReadingDateVal.Text = "—";
        lblLastReadingDateVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblLastReadingValueTitle
        //
        lblLastReadingValueTitle.AutoSize = true;
        lblLastReadingValueTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLastReadingValueTitle.Location = new Point(496, 62);
        lblLastReadingValueTitle.Name = "lblLastReadingValueTitle";
        lblLastReadingValueTitle.Size = new Size(128, 17);
        lblLastReadingValueTitle.TabIndex = 4;
        lblLastReadingValueTitle.Text = "قيمة آخر قراءة:";
        lblLastReadingValueTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblLastReadingValueVal
        //
        lblLastReadingValueVal.AutoSize = true;
        lblLastReadingValueVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLastReadingValueVal.Location = new Point(385, 62);
        lblLastReadingValueVal.Name = "lblLastReadingValueVal";
        lblLastReadingValueVal.Size = new Size(100, 17);
        lblLastReadingValueVal.TabIndex = 5;
        lblLastReadingValueVal.Text = "—";
        lblLastReadingValueVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblLastConsumptionTitle
        //
        lblLastConsumptionTitle.AutoSize = true;
        lblLastConsumptionTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLastConsumptionTitle.Location = new Point(165, 62);
        lblLastConsumptionTitle.Name = "lblLastConsumptionTitle";
        lblLastConsumptionTitle.Size = new Size(112, 17);
        lblLastConsumptionTitle.TabIndex = 6;
        lblLastConsumptionTitle.Text = "الاستهلاك السابق:";
        lblLastConsumptionTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblLastConsumptionVal
        //
        lblLastConsumptionVal.AutoSize = true;
        lblLastConsumptionVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLastConsumptionVal.Location = new Point(60, 62);
        lblLastConsumptionVal.Name = "lblLastConsumptionVal";
        lblLastConsumptionVal.Size = new Size(90, 17);
        lblLastConsumptionVal.TabIndex = 7;
        lblLastConsumptionVal.Text = "—";
        lblLastConsumptionVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblLastIsReverseTitle
        //
        lblLastIsReverseTitle.AutoSize = true;
        lblLastIsReverseTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLastIsReverseTitle.Location = new Point(496, 96);
        lblLastIsReverseTitle.Name = "lblLastIsReverseTitle";
        lblLastIsReverseTitle.Size = new Size(128, 17);
        lblLastIsReverseTitle.TabIndex = 8;
        lblLastIsReverseTitle.Text = "عداد عكسي:";
        lblLastIsReverseTitle.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblLastIsReverseVal
        //
        lblLastIsReverseVal.AutoSize = true;
        lblLastIsReverseVal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLastIsReverseVal.Location = new Point(385, 96);
        lblLastIsReverseVal.Name = "lblLastIsReverseVal";
        lblLastIsReverseVal.Size = new Size(100, 17);
        lblLastIsReverseVal.TabIndex = 9;
        lblLastIsReverseVal.Text = "—";
        lblLastIsReverseVal.TextAlign = ContentAlignment.MiddleLeft;
        //
        // grpNewReading
        //
        grpNewReading.Controls.Add(lblNotesCount);
        grpNewReading.Controls.Add(txtNotes);
        grpNewReading.Controls.Add(lblNotes);
        grpNewReading.Controls.Add(nudReadingValue);
        grpNewReading.Controls.Add(lblReadingValue);
        grpNewReading.Controls.Add(dtpReadingDate);
        grpNewReading.Controls.Add(lblReadingDate);
        grpNewReading.Dock = DockStyle.Top;
        grpNewReading.Font = new Font("Segoe UI", 9.5F);
        grpNewReading.Location = new Point(12, 317);
        grpNewReading.Name = "grpNewReading";
        grpNewReading.RightToLeft = RightToLeft.Yes;
        grpNewReading.Size = new Size(636, 220);
        grpNewReading.TabIndex = 2;
        grpNewReading.TabStop = false;
        grpNewReading.Text = "القراءة الجديدة";
        //
        // lblReadingDate
        //
        lblReadingDate.AutoSize = true;
        lblReadingDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblReadingDate.Location = new Point(500, 32);
        lblReadingDate.Name = "lblReadingDate";
        lblReadingDate.Size = new Size(124, 17);
        lblReadingDate.TabIndex = 0;
        lblReadingDate.Text = "تاريخ القراءة:";
        lblReadingDate.TextAlign = ContentAlignment.MiddleRight;
        //
        // dtpReadingDate
        //
        dtpReadingDate.CustomFormat = "dd/MM/yyyy";
        dtpReadingDate.Format = DateTimePickerFormat.Custom;
        dtpReadingDate.Location = new Point(240, 28);
        dtpReadingDate.Name = "dtpReadingDate";
        dtpReadingDate.Size = new Size(250, 28);
        dtpReadingDate.TabIndex = 1;
        //
        // lblReadingValue
        //
        lblReadingValue.AutoSize = true;
        lblReadingValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblReadingValue.Location = new Point(500, 74);
        lblReadingValue.Name = "lblReadingValue";
        lblReadingValue.Size = new Size(124, 17);
        lblReadingValue.TabIndex = 2;
        lblReadingValue.Text = "قيمة القراءة:";
        lblReadingValue.TextAlign = ContentAlignment.MiddleRight;
        //
        // nudReadingValue
        //
        nudReadingValue.DecimalPlaces = 3;
        nudReadingValue.Location = new Point(240, 68);
        nudReadingValue.Maximum = 999999999999999M;
        nudReadingValue.Name = "nudReadingValue";
        nudReadingValue.RightToLeft = RightToLeft.No;
        nudReadingValue.Size = new Size(250, 28);
        nudReadingValue.TabIndex = 3;
        nudReadingValue.TextAlign = HorizontalAlignment.Right;
        nudReadingValue.ThousandsSeparator = true;
        //
        // lblNotes
        //
        lblNotes.AutoSize = true;
        lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotes.Location = new Point(500, 116);
        lblNotes.Name = "lblNotes";
        lblNotes.Size = new Size(124, 17);
        lblNotes.TabIndex = 4;
        lblNotes.Text = "الملاحظات:";
        lblNotes.TextAlign = ContentAlignment.MiddleRight;
        //
        // txtNotes
        //
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.Location = new Point(240, 112);
        txtNotes.MaxLength = 2000;
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        txtNotes.RightToLeft = RightToLeft.Yes;
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(330, 84);
        txtNotes.TabIndex = 5;
        //
        // lblNotesCount
        //
        lblNotesCount.AutoSize = true;
        lblNotesCount.Font = new Font("Segoe UI", 9F);
        lblNotesCount.Location = new Point(60, 200);
        lblNotesCount.Name = "lblNotesCount";
        lblNotesCount.Size = new Size(60, 15);
        lblNotesCount.TabIndex = 6;
        lblNotesCount.Text = "0 / 2000";
        lblNotesCount.TextAlign = ContentAlignment.MiddleLeft;
        //
        // pnlActions
        //
        pnlActions.Controls.Add(lblStatus);
        pnlActions.Controls.Add(pbLoading);
        pnlActions.Controls.Add(btnCancel);
        pnlActions.Controls.Add(btnSave);
        pnlActions.Dock = DockStyle.Bottom;
        pnlActions.Location = new Point(12, 556);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(636, 72);
        pnlActions.TabIndex = 3;
        //
        // btnSave
        //
        btnSave.BackColor = Color.FromArgb(46, 125, 50);
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(300, 14);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(205, 42);
        btnSave.TabIndex = 0;
        btnSave.Text = "حفظ القراءة";
        btnSave.UseVisualStyleBackColor = false;
        //
        // btnCancel
        //
        btnCancel.BackColor = Color.FromArgb(120, 120, 120);
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnCancel.ForeColor = Color.White;
        btnCancel.Location = new Point(150, 14);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(130, 42);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "إلغاء";
        btnCancel.UseVisualStyleBackColor = false;
        //
        // pbLoading
        //
        pbLoading.Location = new Point(150, 62);
        pbLoading.MarqueeAnimationSpeed = 15;
        pbLoading.Name = "pbLoading";
        pbLoading.Size = new Size(350, 6);
        pbLoading.Style = ProgressBarStyle.Marquee;
        pbLoading.TabIndex = 2;
        pbLoading.TabStop = false;
        pbLoading.Visible = false;
        //
        // lblStatus
        //
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 9F);
        lblStatus.ForeColor = Color.DimGray;
        lblStatus.Location = new Point(15, 26);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(100, 15);
        lblStatus.TabIndex = 3;
        lblStatus.Text = "جاهز لإدخال القراءة";
        //
        // MeterReadingForm
        //
        AcceptButton = btnSave;
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(660, 640);
        Controls.Add(pnlActions);
        Controls.Add(grpNewReading);
        Controls.Add(grpLastReading);
        Controls.Add(grpMeterInfo);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "MeterReadingForm";
        Padding = new Padding(12);
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "إدخال قراءة العداد - WaterStation";
        grpMeterInfo.ResumeLayout(false);
        grpMeterInfo.PerformLayout();
        grpLastReading.ResumeLayout(false);
        grpLastReading.PerformLayout();
        grpNewReading.ResumeLayout(false);
        grpNewReading.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudReadingValue).EndInit();
        pnlActions.ResumeLayout(false);
        pnlActions.PerformLayout();
        ResumeLayout(false);
    }

    private GroupBox grpMeterInfo;
    private Label lblReadingDirectionVal;
    private Label lblReadingDirectionTitle;
    private Label lblMeterTypeVal;
    private Label lblMeterTypeTitle;
    private Label lblAreaVal;
    private Label lblAreaTitle;
    private Label lblBranchVal;
    private Label lblBranchTitle;
    private Label lblCustomerNameVal;
    private Label lblCustomerNameTitle;
    private Label lblCustomerNumVal;
    private Label lblCustomerNumTitle;
    private Label lblMeterNumVal;
    private Label lblMeterNumTitle;
    private GroupBox grpLastReading;
    private Label lblLastIsReverseVal;
    private Label lblLastIsReverseTitle;
    private Label lblLastConsumptionVal;
    private Label lblLastConsumptionTitle;
    private Label lblLastReadingValueVal;
    private Label lblLastReadingValueTitle;
    private Label lblLastReadingDateVal;
    private Label lblLastReadingDateTitle;
    private Label lblInstallationReadingVal;
    private Label lblInstallationReadingTitle;
    private GroupBox grpNewReading;
    private Label lblNotesCount;
    private TextBox txtNotes;
    private Label lblNotes;
    private NumericUpDown nudReadingValue;
    private Label lblReadingValue;
    private DateTimePicker dtpReadingDate;
    private Label lblReadingDate;
    private Panel pnlActions;
    private Label lblStatus;
    private ProgressBar pbLoading;
    private Button btnCancel;
    private Button btnSave;
}