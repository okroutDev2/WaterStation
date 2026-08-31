#nullable enable

namespace WaterStation.Forms;

partial class PaymentForm
{
    private System.ComponentModel.IContainer? components = null;

    // Invoice Info GroupBox
    private GroupBox grpInvoiceInfo = null!;
    private Label lblInvoiceNumTitle = null!;
    private Label lblInvoiceNumVal = null!;
    private Label lblPeriodTitle = null!;
    private Label lblPeriodVal = null!;
    private Label lblTotalTitle = null!;
    private Label lblTotalVal = null!;
    private Label lblPaidTitle = null!;
    private Label lblPaidVal = null!;
    private Label lblBalanceTitle = null!;
    private Label lblBalanceVal = null!;
    private Label lblStatusTitle = null!;
    private Label lblStatusVal = null!;

    // Payment Input GroupBox
    private GroupBox grpPaymentInput = null!;
    private Label lblAmount = null!;
    private NumericUpDown nudAmount = null!;
    private Label lblPaymentMethod = null!;
    private ComboBox cmbPaymentMethod = null!;
    private Label lblPaymentDate = null!;
    private DateTimePicker dtpPaymentDate = null!;
    private Label lblReferenceNumber = null!;
    private TextBox txtReferenceNumber = null!;
    private Label lblNotes = null!;
    private TextBox txtNotes = null!;

    // Actions & Status
    private Panel pnlActions = null!;
    private Button btnPay = null!;
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
        grpInvoiceInfo = new GroupBox();
        lblInvoiceNumTitle = new Label();
        lblInvoiceNumVal = new Label();
        lblPeriodTitle = new Label();
        lblPeriodVal = new Label();
        lblTotalTitle = new Label();
        lblTotalVal = new Label();
        lblPaidTitle = new Label();
        lblPaidVal = new Label();
        lblBalanceTitle = new Label();
        lblBalanceVal = new Label();
        lblStatusTitle = new Label();
        lblStatusVal = new Label();

        grpPaymentInput = new GroupBox();
        lblAmount = new Label();
        nudAmount = new NumericUpDown();
        lblPaymentMethod = new Label();
        cmbPaymentMethod = new ComboBox();
        lblPaymentDate = new Label();
        dtpPaymentDate = new DateTimePicker();
        lblReferenceNumber = new Label();
        txtReferenceNumber = new TextBox();
        lblNotes = new Label();
        txtNotes = new TextBox();

        pnlActions = new Panel();
        btnPay = new Button();
        btnCancel = new Button();
        pbLoading = new ProgressBar();
        lblStatus = new Label();

        grpInvoiceInfo.SuspendLayout();
        grpPaymentInput.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudAmount).BeginInit();
        pnlActions.SuspendLayout();
        SuspendLayout();

        // 
        // grpInvoiceInfo
        // 
        grpInvoiceInfo.Controls.Add(lblStatusVal);
        grpInvoiceInfo.Controls.Add(lblStatusTitle);
        grpInvoiceInfo.Controls.Add(lblBalanceVal);
        grpInvoiceInfo.Controls.Add(lblBalanceTitle);
        grpInvoiceInfo.Controls.Add(lblPaidVal);
        grpInvoiceInfo.Controls.Add(lblPaidTitle);
        grpInvoiceInfo.Controls.Add(lblTotalVal);
        grpInvoiceInfo.Controls.Add(lblTotalTitle);
        grpInvoiceInfo.Controls.Add(lblPeriodVal);
        grpInvoiceInfo.Controls.Add(lblPeriodTitle);
        grpInvoiceInfo.Controls.Add(lblInvoiceNumVal);
        grpInvoiceInfo.Controls.Add(lblInvoiceNumTitle);
        grpInvoiceInfo.Dock = DockStyle.Top;
        grpInvoiceInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpInvoiceInfo.Location = new Point(12, 12);
        grpInvoiceInfo.Name = "grpInvoiceInfo";
        grpInvoiceInfo.Padding = new Padding(8);
        grpInvoiceInfo.Size = new Size(580, 140);
        grpInvoiceInfo.TabIndex = 0;
        grpInvoiceInfo.TabStop = false;
        grpInvoiceInfo.Text = "بيانات الفاتورة المراد سدادها";

        // Invoice Number
        lblInvoiceNumTitle.AutoSize = true;
        lblInvoiceNumTitle.Font = new Font("Segoe UI", 9F);
        lblInvoiceNumTitle.Location = new Point(480, 30);
        lblInvoiceNumTitle.Name = "lblInvoiceNumTitle";
        lblInvoiceNumTitle.Size = new Size(71, 15);
        lblInvoiceNumTitle.TabIndex = 0;
        lblInvoiceNumTitle.Text = "رقم الفاتورة:";
        lblInvoiceNumVal.AutoSize = true;
        lblInvoiceNumVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblInvoiceNumVal.ForeColor = Color.MidnightBlue;
        lblInvoiceNumVal.Location = new Point(330, 28);
        lblInvoiceNumVal.Name = "lblInvoiceNumVal";
        lblInvoiceNumVal.Size = new Size(24, 19);
        lblInvoiceNumVal.TabIndex = 1;
        lblInvoiceNumVal.Text = "—";

        // Period
        lblPeriodTitle.AutoSize = true;
        lblPeriodTitle.Font = new Font("Segoe UI", 9F);
        lblPeriodTitle.Location = new Point(200, 30);
        lblPeriodTitle.Name = "lblPeriodTitle";
        lblPeriodTitle.Size = new Size(76, 15);
        lblPeriodTitle.TabIndex = 2;
        lblPeriodTitle.Text = "فترة الفاتورة:";
        lblPeriodVal.AutoSize = true;
        lblPeriodVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblPeriodVal.Location = new Point(60, 28);
        lblPeriodVal.Name = "lblPeriodVal";
        lblPeriodVal.Size = new Size(24, 19);
        lblPeriodVal.TabIndex = 3;
        lblPeriodVal.Text = "—";

        // Total
        lblTotalTitle.AutoSize = true;
        lblTotalTitle.Font = new Font("Segoe UI", 9F);
        lblTotalTitle.Location = new Point(480, 65);
        lblTotalTitle.Name = "lblTotalTitle";
        lblTotalTitle.Size = new Size(76, 15);
        lblTotalTitle.TabIndex = 4;
        lblTotalTitle.Text = "إجمالي المبلغ:";
        lblTotalVal.AutoSize = true;
        lblTotalVal.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        lblTotalVal.Location = new Point(330, 63);
        lblTotalVal.Name = "lblTotalVal";
        lblTotalVal.Size = new Size(36, 19);
        lblTotalVal.TabIndex = 5;
        lblTotalVal.Text = "0.00";

        // Paid
        lblPaidTitle.AutoSize = true;
        lblPaidTitle.Font = new Font("Segoe UI", 9F);
        lblPaidTitle.Location = new Point(200, 65);
        lblPaidTitle.Name = "lblPaidTitle";
        lblPaidTitle.Size = new Size(82, 15);
        lblPaidTitle.TabIndex = 6;
        lblPaidTitle.Text = "المدفوع سابقاً:";
        lblPaidVal.AutoSize = true;
        lblPaidVal.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        lblPaidVal.ForeColor = Color.Green;
        lblPaidVal.Location = new Point(60, 63);
        lblPaidVal.Name = "lblPaidVal";
        lblPaidVal.Size = new Size(36, 19);
        lblPaidVal.TabIndex = 7;
        lblPaidVal.Text = "0.00";

        // Balance
        lblBalanceTitle.AutoSize = true;
        lblBalanceTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBalanceTitle.ForeColor = Color.DarkRed;
        lblBalanceTitle.Location = new Point(480, 100);
        lblBalanceTitle.Name = "lblBalanceTitle";
        lblBalanceTitle.Size = new Size(86, 17);
        lblBalanceTitle.TabIndex = 8;
        lblBalanceTitle.Text = "المبلغ المتبقي:";
        lblBalanceVal.AutoSize = true;
        lblBalanceVal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblBalanceVal.ForeColor = Color.DarkRed;
        lblBalanceVal.Location = new Point(330, 96);
        lblBalanceVal.Name = "lblBalanceVal";
        lblBalanceVal.Size = new Size(47, 25);
        lblBalanceVal.TabIndex = 9;
        lblBalanceVal.Text = "0.00";

        // Status
        lblStatusTitle.AutoSize = true;
        lblStatusTitle.Font = new Font("Segoe UI", 9F);
        lblStatusTitle.Location = new Point(200, 100);
        lblStatusTitle.Name = "lblStatusTitle";
        lblStatusTitle.Size = new Size(72, 15);
        lblStatusTitle.TabIndex = 10;
        lblStatusTitle.Text = "حالة الفاتورة:";
        lblStatusVal.AutoSize = true;
        lblStatusVal.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        lblStatusVal.ForeColor = Color.Navy;
        lblStatusVal.Location = new Point(60, 98);
        lblStatusVal.Name = "lblStatusVal";
        lblStatusVal.Size = new Size(24, 19);
        lblStatusVal.TabIndex = 11;
        lblStatusVal.Text = "—";

        // 
        // grpPaymentInput
        // 
        grpPaymentInput.Controls.Add(txtNotes);
        grpPaymentInput.Controls.Add(lblNotes);
        grpPaymentInput.Controls.Add(txtReferenceNumber);
        grpPaymentInput.Controls.Add(lblReferenceNumber);
        grpPaymentInput.Controls.Add(dtpPaymentDate);
        grpPaymentInput.Controls.Add(lblPaymentDate);
        grpPaymentInput.Controls.Add(cmbPaymentMethod);
        grpPaymentInput.Controls.Add(lblPaymentMethod);
        grpPaymentInput.Controls.Add(nudAmount);
        grpPaymentInput.Controls.Add(lblAmount);
        grpPaymentInput.Dock = DockStyle.Top;
        grpPaymentInput.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpPaymentInput.Location = new Point(12, 160);
        grpPaymentInput.Name = "grpPaymentInput";
        grpPaymentInput.Padding = new Padding(12);
        grpPaymentInput.Size = new Size(580, 270);
        grpPaymentInput.TabIndex = 1;
        grpPaymentInput.TabStop = false;
        grpPaymentInput.Text = "بيانات عملية السداد";

        // lblAmount
        lblAmount.AutoSize = true;
        lblAmount.Font = new Font("Segoe UI", 9.5F);
        lblAmount.Location = new Point(455, 33);
        lblAmount.Name = "lblAmount";
        lblAmount.Size = new Size(100, 17);
        lblAmount.TabIndex = 0;
        lblAmount.Text = "مبلغ السداد (ر.س): *";

        // nudAmount
        nudAmount.DecimalPlaces = 2;
        nudAmount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        nudAmount.Location = new Point(150, 30);
        nudAmount.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        nudAmount.Name = "nudAmount";
        nudAmount.Size = new Size(280, 27);
        nudAmount.TabIndex = 1;
        nudAmount.TextAlign = HorizontalAlignment.Right;

        // lblPaymentMethod
        lblPaymentMethod.AutoSize = true;
        lblPaymentMethod.Font = new Font("Segoe UI", 9.5F);
        lblPaymentMethod.Location = new Point(455, 73);
        lblPaymentMethod.Name = "lblPaymentMethod";
        lblPaymentMethod.Size = new Size(82, 17);
        lblPaymentMethod.TabIndex = 2;
        lblPaymentMethod.Text = "طريقة الدفع: *";

        // cmbPaymentMethod
        cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPaymentMethod.Font = new Font("Segoe UI", 9.5F);
        cmbPaymentMethod.FormattingEnabled = true;
        cmbPaymentMethod.Location = new Point(150, 70);
        cmbPaymentMethod.Name = "cmbPaymentMethod";
        cmbPaymentMethod.Size = new Size(280, 24);
        cmbPaymentMethod.TabIndex = 3;

        // lblPaymentDate
        lblPaymentDate.AutoSize = true;
        lblPaymentDate.Font = new Font("Segoe UI", 9.5F);
        lblPaymentDate.Location = new Point(455, 113);
        lblPaymentDate.Name = "lblPaymentDate";
        lblPaymentDate.Size = new Size(84, 17);
        lblPaymentDate.TabIndex = 4;
        lblPaymentDate.Text = "تاريخ السداد: *";

        // dtpPaymentDate
        dtpPaymentDate.CustomFormat = "yyyy-MM-dd HH:mm";
        dtpPaymentDate.Font = new Font("Segoe UI", 9.5F);
        dtpPaymentDate.Format = DateTimePickerFormat.Custom;
        dtpPaymentDate.Location = new Point(150, 110);
        dtpPaymentDate.Name = "dtpPaymentDate";
        dtpPaymentDate.Size = new Size(280, 24);
        dtpPaymentDate.TabIndex = 5;

        // lblReferenceNumber
        lblReferenceNumber.AutoSize = true;
        lblReferenceNumber.Font = new Font("Segoe UI", 9.5F);
        lblReferenceNumber.Location = new Point(455, 153);
        lblReferenceNumber.Name = "lblReferenceNumber";
        lblReferenceNumber.Size = new Size(74, 17);
        lblReferenceNumber.TabIndex = 6;
        lblReferenceNumber.Text = "رقم المرجع:";

        // txtReferenceNumber
        txtReferenceNumber.Font = new Font("Segoe UI", 9.5F);
        txtReferenceNumber.Location = new Point(150, 150);
        txtReferenceNumber.MaxLength = 100;
        txtReferenceNumber.Name = "txtReferenceNumber";
        txtReferenceNumber.PlaceholderText = "اختياري (رقم الحوالة أو الإيصال اليدوي)";
        txtReferenceNumber.Size = new Size(280, 24);
        txtReferenceNumber.TabIndex = 7;

        // lblNotes
        lblNotes.AutoSize = true;
        lblNotes.Font = new Font("Segoe UI", 9.5F);
        lblNotes.Location = new Point(455, 193);
        lblNotes.Name = "lblNotes";
        lblNotes.Size = new Size(60, 17);
        lblNotes.TabIndex = 8;
        lblNotes.Text = "ملاحظات:";

        // txtNotes
        txtNotes.Font = new Font("Segoe UI", 9.5F);
        txtNotes.Location = new Point(150, 190);
        txtNotes.MaxLength = 1000;
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        txtNotes.PlaceholderText = "ملاحظات إضافية على عملية الدفع...";
        txtNotes.Size = new Size(280, 60);
        txtNotes.TabIndex = 9;

        // 
        // pnlActions
        // 
        pnlActions.Controls.Add(lblStatus);
        pnlActions.Controls.Add(pbLoading);
        pnlActions.Controls.Add(btnCancel);
        pnlActions.Controls.Add(btnPay);
        pnlActions.Dock = DockStyle.Bottom;
        pnlActions.Location = new Point(12, 440);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(580, 75);
        pnlActions.TabIndex = 2;

        // btnPay
        btnPay.BackColor = Color.FromArgb(0, 122, 204);
        btnPay.FlatStyle = FlatStyle.Flat;
        btnPay.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnPay.ForeColor = Color.White;
        btnPay.Location = new Point(280, 15);
        btnPay.Name = "btnPay";
        btnPay.Size = new Size(160, 42);
        btnPay.TabIndex = 0;
        btnPay.Text = "تنفيذ السداد";
        btnPay.UseVisualStyleBackColor = false;

        // btnCancel
        btnCancel.BackColor = Color.FromArgb(240, 240, 240);
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F);
        btnCancel.Location = new Point(150, 15);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(110, 42);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "إلغاء";
        btnCancel.UseVisualStyleBackColor = true;

        // pbLoading
        pbLoading.Location = new Point(150, 60);
        pbLoading.Name = "pbLoading";
        pbLoading.Size = new Size(290, 8);
        pbLoading.Style = ProgressBarStyle.Marquee;
        pbLoading.TabIndex = 2;
        pbLoading.Visible = false;

        // lblStatus
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 8.5F);
        lblStatus.ForeColor = Color.DimGray;
        lblStatus.Location = new Point(15, 28);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(71, 15);
        lblStatus.TabIndex = 3;
        lblStatus.Text = "جاهز للسداد";

        // 
        // PaymentForm
        // 
        AcceptButton = btnPay;
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(604, 527);
        Controls.Add(pnlActions);
        Controls.Add(grpPaymentInput);
        Controls.Add(grpInvoiceInfo);
        Font = new Font("Segoe UI", 9.5F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PaymentForm";
        Padding = new Padding(12);
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "سداد الفاتورة - WaterStation";

        grpInvoiceInfo.ResumeLayout(false);
        grpInvoiceInfo.PerformLayout();
        grpPaymentInput.ResumeLayout(false);
        grpPaymentInput.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudAmount).EndInit();
        pnlActions.ResumeLayout(false);
        pnlActions.PerformLayout();
        ResumeLayout(false);
    }
}
