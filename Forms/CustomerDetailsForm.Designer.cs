namespace WaterStation.Forms;

partial class CustomerDetailsForm
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
        this.components = new System.ComponentModel.Container();
        this.lblHeading = new Label();
        this.fieldsInfo = new TableLayoutPanel();
        this.lblCustNumberCaption = new Label();
        this.lblCustomerNumberValue = new Label();
        this.lblFullNameCaption = new Label();
        this.lblFullNameValue = new Label();
        this.lblPhoneCaption = new Label();
        this.lblPhoneValue = new Label();
        this.lblAddressCaption = new Label();
        this.lblAddressValue = new Label();
        this.lblFamilyCaption = new Label();
        this.lblFamilyCountValue = new Label();
        this.lblStatusCaption = new Label();
        this.lblStatusValue = new Label();
        this.lblNotesCaption = new Label();
        this.lblNotesValue = new Label();
        this.lblMetersTitle = new Label();
        this.dgvMeters = new DataGridView();
        this.lblInvoicesTitle = new Label();
        this.dgvInvoices = new DataGridView();
        this.pnlButtons = new Panel();
        this.flowActions = new FlowLayoutPanel();
        this.btnAddMeter = new Button();
        this.btnOpenCollection = new Button();
        this.btnBack = new Button();
        this.lblFooter = new Label();
        this.fieldsInfo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.dgvMeters).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.dgvInvoices).BeginInit();
        this.pnlButtons.SuspendLayout();
        this.flowActions.SuspendLayout();
        this.SuspendLayout();

        // lblHeading
        this.lblHeading.AutoSize = true;
        this.lblHeading.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        this.lblHeading.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblHeading.Location = new Point(16, 12);
        this.lblHeading.Name = "lblHeading";
        this.lblHeading.Size = new Size(140, 21);
        this.lblHeading.TabIndex = 0;
        this.lblHeading.Text = "تفاصيل العميل";

        // lblCustNumberCaption
        this.lblCustNumberCaption.AutoSize = false;
        this.lblCustNumberCaption.Dock = DockStyle.Fill;
        this.lblCustNumberCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblCustNumberCaption.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblCustNumberCaption.Margin = new Padding(0, 2, 2, 2);
        this.lblCustNumberCaption.Name = "lblCustNumberCaption";
        this.lblCustNumberCaption.Text = "رقم العميل";
        this.lblCustNumberCaption.TextAlign = ContentAlignment.MiddleRight;

        // lblCustomerNumberValue
        this.lblCustomerNumberValue.AutoSize = false;
        this.lblCustomerNumberValue.Dock = DockStyle.Fill;
        this.lblCustomerNumberValue.Font = new Font("Segoe UI", 9.5F);
        this.lblCustomerNumberValue.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblCustomerNumberValue.Margin = new Padding(2, 2, 8, 2);
        this.lblCustomerNumberValue.Name = "lblCustomerNumberValue";
        this.lblCustomerNumberValue.Text = "—";
        this.lblCustomerNumberValue.TextAlign = ContentAlignment.MiddleLeft;

        // lblFullNameCaption
        this.lblFullNameCaption.AutoSize = false;
        this.lblFullNameCaption.Dock = DockStyle.Fill;
        this.lblFullNameCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblFullNameCaption.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblFullNameCaption.Margin = new Padding(0, 2, 2, 2);
        this.lblFullNameCaption.Name = "lblFullNameCaption";
        this.lblFullNameCaption.Text = "اسم العميل";
        this.lblFullNameCaption.TextAlign = ContentAlignment.MiddleRight;

        // lblFullNameValue
        this.lblFullNameValue.AutoSize = false;
        this.lblFullNameValue.Dock = DockStyle.Fill;
        this.lblFullNameValue.Font = new Font("Segoe UI", 9.5F);
        this.lblFullNameValue.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblFullNameValue.Margin = new Padding(2, 2, 8, 2);
        this.lblFullNameValue.Name = "lblFullNameValue";
        this.lblFullNameValue.Text = "—";
        this.lblFullNameValue.TextAlign = ContentAlignment.MiddleLeft;

        // lblPhoneCaption
        this.lblPhoneCaption.AutoSize = false;
        this.lblPhoneCaption.Dock = DockStyle.Fill;
        this.lblPhoneCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblPhoneCaption.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblPhoneCaption.Margin = new Padding(0, 2, 2, 2);
        this.lblPhoneCaption.Name = "lblPhoneCaption";
        this.lblPhoneCaption.Text = "الهاتف";
        this.lblPhoneCaption.TextAlign = ContentAlignment.MiddleRight;

        // lblPhoneValue
        this.lblPhoneValue.AutoSize = false;
        this.lblPhoneValue.Dock = DockStyle.Fill;
        this.lblPhoneValue.Font = new Font("Segoe UI", 9.5F);
        this.lblPhoneValue.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblPhoneValue.Margin = new Padding(2, 2, 8, 2);
        this.lblPhoneValue.Name = "lblPhoneValue";
        this.lblPhoneValue.Text = "—";
        this.lblPhoneValue.TextAlign = ContentAlignment.MiddleLeft;

        // lblAddressCaption
        this.lblAddressCaption.AutoSize = false;
        this.lblAddressCaption.Dock = DockStyle.Fill;
        this.lblAddressCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblAddressCaption.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblAddressCaption.Margin = new Padding(0, 2, 2, 2);
        this.lblAddressCaption.Name = "lblAddressCaption";
        this.lblAddressCaption.Text = "العنوان";
        this.lblAddressCaption.TextAlign = ContentAlignment.MiddleRight;

        // lblAddressValue
        this.lblAddressValue.AutoSize = false;
        this.lblAddressValue.Dock = DockStyle.Fill;
        this.lblAddressValue.Font = new Font("Segoe UI", 9.5F);
        this.lblAddressValue.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblAddressValue.Margin = new Padding(2, 2, 8, 2);
        this.lblAddressValue.Name = "lblAddressValue";
        this.lblAddressValue.Text = "—";
        this.lblAddressValue.TextAlign = ContentAlignment.MiddleLeft;

        // lblFamilyCaption
        this.lblFamilyCaption.AutoSize = false;
        this.lblFamilyCaption.Dock = DockStyle.Fill;
        this.lblFamilyCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblFamilyCaption.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblFamilyCaption.Margin = new Padding(0, 2, 2, 2);
        this.lblFamilyCaption.Name = "lblFamilyCaption";
        this.lblFamilyCaption.Text = "عدد الأفراد";
        this.lblFamilyCaption.TextAlign = ContentAlignment.MiddleRight;

        // lblFamilyCountValue
        this.lblFamilyCountValue.AutoSize = false;
        this.lblFamilyCountValue.Dock = DockStyle.Fill;
        this.lblFamilyCountValue.Font = new Font("Segoe UI", 9.5F);
        this.lblFamilyCountValue.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblFamilyCountValue.Margin = new Padding(2, 2, 8, 2);
        this.lblFamilyCountValue.Name = "lblFamilyCountValue";
        this.lblFamilyCountValue.Text = "—";
        this.lblFamilyCountValue.TextAlign = ContentAlignment.MiddleLeft;

        // lblStatusCaption
        this.lblStatusCaption.AutoSize = false;
        this.lblStatusCaption.Dock = DockStyle.Fill;
        this.lblStatusCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblStatusCaption.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblStatusCaption.Margin = new Padding(0, 2, 2, 2);
        this.lblStatusCaption.Name = "lblStatusCaption";
        this.lblStatusCaption.Text = "الحالة";
        this.lblStatusCaption.TextAlign = ContentAlignment.MiddleRight;

        // lblStatusValue
        this.lblStatusValue.AutoSize = false;
        this.lblStatusValue.Dock = DockStyle.Fill;
        this.lblStatusValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblStatusValue.Margin = new Padding(2, 2, 8, 2);
        this.lblStatusValue.Name = "lblStatusValue";
        this.lblStatusValue.Text = "—";
        this.lblStatusValue.TextAlign = ContentAlignment.MiddleLeft;

        // lblNotesCaption
        this.lblNotesCaption.AutoSize = false;
        this.lblNotesCaption.Dock = DockStyle.Fill;
        this.lblNotesCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        this.lblNotesCaption.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblNotesCaption.Margin = new Padding(0, 2, 2, 2);
        this.lblNotesCaption.Name = "lblNotesCaption";
        this.lblNotesCaption.Text = "الملاحظات";
        this.lblNotesCaption.TextAlign = ContentAlignment.MiddleRight;

        // lblNotesValue
        this.lblNotesValue.AutoSize = false;
        this.lblNotesValue.Dock = DockStyle.Fill;
        this.lblNotesValue.Font = new Font("Segoe UI", 9.5F);
        this.lblNotesValue.ForeColor = Color.FromArgb(107, 114, 128);
        this.lblNotesValue.Margin = new Padding(2, 2, 2, 2);
        this.lblNotesValue.Name = "lblNotesValue";
        this.lblNotesValue.Text = "—";
        this.lblNotesValue.TextAlign = ContentAlignment.MiddleRight;

        // fieldsInfo
        this.fieldsInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.fieldsInfo.BackColor = Color.White;
        this.fieldsInfo.ColumnCount = 5;
        this.fieldsInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        this.fieldsInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
        this.fieldsInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        this.fieldsInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
        this.fieldsInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
        this.fieldsInfo.Location = new Point(16, 44);
        this.fieldsInfo.Name = "fieldsInfo";
        this.fieldsInfo.Padding = new Padding(8, 6, 8, 6);
        this.fieldsInfo.RowCount = 5;
        this.fieldsInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        this.fieldsInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        this.fieldsInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        this.fieldsInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        this.fieldsInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        this.fieldsInfo.Size = new Size(700, 140);
        this.fieldsInfo.TabIndex = 1;
        this.fieldsInfo.RightToLeft = RightToLeft.Yes;
        this.fieldsInfo.Controls.Add(this.lblCustNumberCaption, 0, 0);
        this.fieldsInfo.Controls.Add(this.lblCustomerNumberValue, 1, 0);
        this.fieldsInfo.Controls.Add(this.lblFullNameCaption, 3, 0);
        this.fieldsInfo.Controls.Add(this.lblFullNameValue, 4, 0);
        this.fieldsInfo.Controls.Add(this.lblPhoneCaption, 0, 1);
        this.fieldsInfo.Controls.Add(this.lblPhoneValue, 1, 1);
        this.fieldsInfo.Controls.Add(this.lblAddressCaption, 3, 1);
        this.fieldsInfo.Controls.Add(this.lblAddressValue, 4, 1);
        this.fieldsInfo.Controls.Add(this.lblFamilyCaption, 0, 2);
        this.fieldsInfo.Controls.Add(this.lblFamilyCountValue, 1, 2);
        this.fieldsInfo.Controls.Add(this.lblStatusCaption, 3, 2);
        this.fieldsInfo.Controls.Add(this.lblStatusValue, 4, 2);
        this.fieldsInfo.Controls.Add(this.lblNotesCaption, 0, 3);
        this.fieldsInfo.Controls.Add(this.lblNotesValue, 0, 4);
        this.fieldsInfo.SetColumnSpan(this.lblNotesCaption, 5);
        this.fieldsInfo.SetColumnSpan(this.lblNotesValue, 5);

        // lblMetersTitle
        this.lblMetersTitle.AutoSize = true;
        this.lblMetersTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.lblMetersTitle.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblMetersTitle.Location = new Point(16, 196);
        this.lblMetersTitle.Name = "lblMetersTitle";
        this.lblMetersTitle.Size = new Size(120, 19);
        this.lblMetersTitle.TabIndex = 2;
        this.lblMetersTitle.Text = "عدادات العميل";

        // dgvMeters
        this.dgvMeters.AllowUserToAddRows = false;
        this.dgvMeters.AllowUserToDeleteRows = false;
        this.dgvMeters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.dgvMeters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvMeters.Location = new Point(16, 222);
        this.dgvMeters.Name = "dgvMeters";
        this.dgvMeters.ReadOnly = true;
        this.dgvMeters.RowHeadersVisible = false;
        this.dgvMeters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvMeters.Size = new Size(1048, 130);
        this.dgvMeters.TabIndex = 3;

        // lblInvoicesTitle
        this.lblInvoicesTitle.AutoSize = true;
        this.lblInvoicesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.lblInvoicesTitle.ForeColor = Color.FromArgb(11, 37, 64);
        this.lblInvoicesTitle.Location = new Point(16, 364);
        this.lblInvoicesTitle.Name = "lblInvoicesTitle";
        this.lblInvoicesTitle.Size = new Size(140, 19);
        this.lblInvoicesTitle.TabIndex = 4;
        this.lblInvoicesTitle.Text = "الفواتير المفتوحة";

        // dgvInvoices
        this.dgvInvoices.AllowUserToAddRows = false;
        this.dgvInvoices.AllowUserToDeleteRows = false;
        this.dgvInvoices.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.dgvInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvInvoices.Location = new Point(16, 390);
        this.dgvInvoices.Name = "dgvInvoices";
        this.dgvInvoices.ReadOnly = true;
        this.dgvInvoices.RowHeadersVisible = false;
        this.dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvInvoices.Size = new Size(1048, 112);
        this.dgvInvoices.TabIndex = 5;

        // btnAddMeter
        this.btnAddMeter.AutoSize = false;
        this.btnAddMeter.Name = "btnAddMeter";
        this.btnAddMeter.Size = new Size(130, 36);
        this.btnAddMeter.TabIndex = 6;
        this.btnAddMeter.Text = "إضافة عداد";
        this.btnAddMeter.UseVisualStyleBackColor = true;
        this.btnAddMeter.AccessibleName = "إضافة عداد جديد لهذا العميل";

        // btnOpenCollection
        this.btnOpenCollection.AutoSize = false;
        this.btnOpenCollection.Name = "btnOpenCollection";
        this.btnOpenCollection.Size = new Size(150, 34);
        this.btnOpenCollection.TabIndex = 7;
        this.btnOpenCollection.Text = "فتح شاشة التحصيل";
        this.btnOpenCollection.UseVisualStyleBackColor = true;
        this.btnOpenCollection.AccessibleName = "فتح شاشة تحصيل العميل";

        // btnBack
        this.btnBack.AutoSize = false;
        this.btnBack.Name = "btnBack";
        this.btnBack.Size = new Size(80, 34);
        this.btnBack.TabIndex = 8;
        this.btnBack.Text = "عودة";
        this.btnBack.UseVisualStyleBackColor = true;
        this.btnBack.AccessibleName = "العودة لقائمة العملاء";

        // lblFooter
        this.lblFooter.AutoSize = true;
        this.lblFooter.Font = new Font("Segoe UI", 9F);
        this.lblFooter.ForeColor = Color.FromArgb(107, 114, 128);
        this.lblFooter.Margin = new Padding(8, 9, 0, 0);
        this.lblFooter.Name = "lblFooter";
        this.lblFooter.Size = new Size(300, 15);
        this.lblFooter.TabIndex = 10;
        this.lblFooter.Text = "";

        // flowActions
        this.flowActions.Controls.Add(this.btnAddMeter);
        this.flowActions.Controls.Add(this.btnOpenCollection);
        this.flowActions.Controls.Add(this.btnBack);
        this.flowActions.Controls.Add(this.lblFooter);
        this.flowActions.Dock = DockStyle.Fill;
        this.flowActions.FlowDirection = FlowDirection.LeftToRight;
        this.flowActions.Location = new Point(0, 0);
        this.flowActions.Name = "flowActions";
        this.flowActions.Padding = new Padding(10, 8, 10, 6);
        this.flowActions.RightToLeft = RightToLeft.Yes;
        this.flowActions.Size = new Size(1080, 52);
        this.flowActions.TabIndex = 0;
        this.flowActions.WrapContents = false;

        // pnlButtons
        this.pnlButtons.BackColor = Color.FromArgb(245, 247, 250);
        this.pnlButtons.Controls.Add(this.flowActions);
        this.pnlButtons.Dock = DockStyle.Bottom;
        this.pnlButtons.Location = new Point(0, 508);
        this.pnlButtons.Name = "pnlButtons";
        this.pnlButtons.Size = new Size(1080, 52);
        this.pnlButtons.TabIndex = 9;

        // CustomerDetailsForm
        this.AutoScaleDimensions = new SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1080, 560);
        this.Controls.Add(this.dgvInvoices);
        this.Controls.Add(this.lblInvoicesTitle);
        this.Controls.Add(this.dgvMeters);
        this.Controls.Add(this.lblMetersTitle);
        this.Controls.Add(this.fieldsInfo);
        this.Controls.Add(this.lblHeading);
        this.Controls.Add(this.pnlButtons);
        this.Font = new Font("Segoe UI", 9F);
        this.MaximizeBox = false;
        this.MinimumSize = new Size(900, 560);
        this.Name = "CustomerDetailsForm";
        this.RightToLeft = RightToLeft.Yes;
        this.RightToLeftLayout = true;
        this.ShowInTaskbar = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "تفاصيل العميل";
        this.fieldsInfo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.dgvMeters).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.dgvInvoices).EndInit();
        this.pnlButtons.ResumeLayout(false);
        this.flowActions.ResumeLayout(false);
        this.flowActions.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Label lblHeading;
    private TableLayoutPanel fieldsInfo;
    private Label lblCustNumberCaption;
    private Label lblCustomerNumberValue;
    private Label lblFullNameCaption;
    private Label lblFullNameValue;
    private Label lblPhoneCaption;
    private Label lblPhoneValue;
    private Label lblAddressCaption;
    private Label lblAddressValue;
    private Label lblFamilyCaption;
    private Label lblFamilyCountValue;
    private Label lblStatusCaption;
    private Label lblStatusValue;
    private Label lblNotesCaption;
    private Label lblNotesValue;
    private Label lblMetersTitle;
    private DataGridView dgvMeters;
    private Label lblInvoicesTitle;
    private DataGridView dgvInvoices;
    private Panel pnlButtons;
    private FlowLayoutPanel flowActions;
    private Button btnAddMeter;
    private Button btnOpenCollection;
    private Button btnBack;
    private Label lblFooter;
}