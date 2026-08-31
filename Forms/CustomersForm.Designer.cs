#nullable enable

namespace WaterStation.Forms;

partial class CustomersForm
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlToolbar = null!;
    private Label lblScreenTitle = null!;
    private GroupBox grpSearch = null!;
    private Label lblSearchCaption = null!;
    private TextBox txtCustomerNumber = null!;
    private TextBox txtName = null!;
    private Button btnSearch = null!;
    private Button btnClear = null!;
    private Button btnRefresh = null!;
    private FlowLayoutPanel actionsFlow = null!;
    private Button btnAdd = null!;
    private Button btnExit = null!;
    private Button btnDetails = null!;
    private Button btnViewMeters = null!;
    private Button btnAddMeter = null!;
    private Button btnCollect = null!;
    private GroupBox grpSummary = null!;
    private Label lblDetailNameValue = null!;
    private Label lblDetailNumberValue = null!;
    private Label lblDetailPhoneValue = null!;
    private Label lblDetailStatusValue = null!;
    private Label lblDetailBalanceValue = null!;
    private DataGridView dgvCustomers = null!;
    private StatusStrip statusStrip = null!;
    private ToolStripStatusLabel tslblStatus = null!;
    private ToolStripStatusLabel tslblCount = null!;
    private ProgressBar pbLoading = null!;

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
        components = new System.ComponentModel.Container();
        pnlToolbar = new Panel();
        lblScreenTitle = new Label();
        grpSearch = new GroupBox();
        lblSearchCaption = new Label();
        txtCustomerNumber = new TextBox();
        txtName = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        btnRefresh = new Button();
        grpSummary = new GroupBox();
        lblDetailNameValue = new Label();
        lblDetailNumberValue = new Label();
        lblDetailPhoneValue = new Label();
        lblDetailStatusValue = new Label();
        lblDetailBalanceValue = new Label();
        actionsFlow = new FlowLayoutPanel();
        btnAdd = new Button();
        btnExit = new Button();
        btnDetails = new Button();
        btnViewMeters = new Button();
        btnAddMeter = new Button();
        btnCollect = new Button();
        pbLoading = new ProgressBar();
        dgvCustomers = new DataGridView();
        statusStrip = new StatusStrip();
        tslblStatus = new ToolStripStatusLabel();
        tslblCount = new ToolStripStatusLabel();
        pnlToolbar.SuspendLayout();
        grpSearch.SuspendLayout();
        grpSummary.SuspendLayout();
        actionsFlow.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // 
        // pnlToolbar
        // 
        pnlToolbar.Controls.Add(lblScreenTitle);
        pnlToolbar.Controls.Add(grpSearch);
        pnlToolbar.Controls.Add(grpSummary);
        pnlToolbar.Controls.Add(actionsFlow);
        pnlToolbar.Dock = DockStyle.Top;
        pnlToolbar.Location = new Point(0, 0);
        pnlToolbar.Name = "pnlToolbar";
        pnlToolbar.Padding = new Padding(8);
        pnlToolbar.Size = new Size(1100, 234);
        pnlToolbar.TabIndex = 0;

        // 
        // lblScreenTitle
        // 
        lblScreenTitle.Dock = DockStyle.Top;
        lblScreenTitle.Location = new Point(8, 8);
        lblScreenTitle.Name = "lblScreenTitle";
        lblScreenTitle.Size = new Size(1084, 34);
        lblScreenTitle.TabIndex = 0;
        lblScreenTitle.Text = "إدارة العملاء";

        // 
        // grpSearch
        // 
        grpSearch.Controls.Add(lblSearchCaption);
        grpSearch.Controls.Add(txtCustomerNumber);
        grpSearch.Controls.Add(txtName);
        grpSearch.Controls.Add(btnSearch);
        grpSearch.Controls.Add(btnClear);
        grpSearch.Controls.Add(btnRefresh);
        grpSearch.Dock = DockStyle.Top;
        grpSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpSearch.Location = new Point(8, 8);
        grpSearch.Name = "grpSearch";
        grpSearch.Size = new Size(1084, 92);
        grpSearch.TabIndex = 0;
        grpSearch.TabStop = false;
        grpSearch.Text = "بحث عن عميل";

        // 
        // lblSearchCaption
        // 
        lblSearchCaption.AutoSize = true;
        lblSearchCaption.Font = new Font("Segoe UI", 9F);
        lblSearchCaption.Location = new Point(1022, 30);
        lblSearchCaption.Name = "lblSearchCaption";
        lblSearchCaption.Size = new Size(50, 15);
        lblSearchCaption.TabIndex = 0;
        lblSearchCaption.Text = "بحث:";

        // 
        // txtCustomerNumber
        // 
        txtCustomerNumber.Font = new Font("Segoe UI", 9.5F);
        txtCustomerNumber.Location = new Point(834, 26);
        txtCustomerNumber.Name = "txtCustomerNumber";
        txtCustomerNumber.PlaceholderText = "رقم العميل";
        txtCustomerNumber.Size = new Size(180, 24);
        txtCustomerNumber.TabIndex = 1;

        // 
        // txtName
        // 
        txtName.Font = new Font("Segoe UI", 9.5F);
        txtName.Location = new Point(646, 26);
        txtName.Name = "txtName";
        txtName.PlaceholderText = "اسم العميل";
        txtName.Size = new Size(180, 24);
        txtName.TabIndex = 2;

        // 
        // btnSearch
        // 
        btnSearch.BackColor = ColorTranslator.FromHtml("#0B69A3");
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSearch.ForeColor = Color.White;
        btnSearch.Location = new Point(552, 24);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(88, 28);
        btnSearch.TabIndex = 3;
        btnSearch.Text = "بحث";
        btnSearch.UseVisualStyleBackColor = false;

        // 
        // btnClear
        // 
        btnClear.BackColor = ColorTranslator.FromHtml("#F5F7FA");
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9F);
        btnClear.Location = new Point(458, 24);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(88, 28);
        btnClear.TabIndex = 4;
        btnClear.Text = "مسح";
        btnClear.UseVisualStyleBackColor = true;

        // 
        // btnRefresh
        // 
        btnRefresh.BackColor = ColorTranslator.FromHtml("#F5F7FA");
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 9F);
        btnRefresh.Location = new Point(364, 24);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 28);
        btnRefresh.TabIndex = 5;
        btnRefresh.Text = "تحديث";
        btnRefresh.UseVisualStyleBackColor = true;

        // 
        // grpSummary
        // 
        grpSummary.Controls.Add(lblDetailNumberValue);
        grpSummary.Controls.Add(lblDetailNameValue);
        grpSummary.Controls.Add(lblDetailPhoneValue);
        grpSummary.Controls.Add(lblDetailStatusValue);
        grpSummary.Controls.Add(lblDetailBalanceValue);
        grpSummary.Dock = DockStyle.Top;
        grpSummary.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        grpSummary.Location = new Point(8, 100);
        grpSummary.Name = "grpSummary";
        grpSummary.Padding = new Padding(10, 4, 10, 4);
        grpSummary.Size = new Size(1084, 52);
        grpSummary.TabIndex = 6;
        grpSummary.TabStop = false;
        grpSummary.Text = "العميل المحدد";
        // 
        // lblDetailNumberValue
        // 
        lblDetailNumberValue.AutoEllipsis = true;
        lblDetailNumberValue.AutoSize = false;
        lblDetailNumberValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDetailNumberValue.ForeColor = ColorTranslator.FromHtml("#0B2540");
        lblDetailNumberValue.Location = new Point(550, 18);
        lblDetailNumberValue.Name = "lblDetailNumberValue";
        lblDetailNumberValue.Size = new Size(160, 20);
        lblDetailNumberValue.Text = "—";
        lblDetailNumberValue.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblDetailNameValue
        // 
        lblDetailNameValue.AutoEllipsis = true;
        lblDetailNameValue.AutoSize = false;
        lblDetailNameValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDetailNameValue.ForeColor = ColorTranslator.FromHtml("#0B2540");
        lblDetailNameValue.Location = new Point(330, 18);
        lblDetailNameValue.Name = "lblDetailNameValue";
        lblDetailNameValue.Size = new Size(210, 20);
        lblDetailNameValue.Text = "—";
        lblDetailNameValue.TextAlign = ContentAlignment.MiddleRight;
        lblDetailNameValue.AccessibleName = "اسم العميل المحدد";
        // 
        // lblDetailPhoneValue
        // 
        lblDetailPhoneValue.AutoEllipsis = true;
        lblDetailPhoneValue.AutoSize = false;
        lblDetailPhoneValue.Font = new Font("Segoe UI", 9F);
        lblDetailPhoneValue.ForeColor = ColorTranslator.FromHtml("#6B7280");
        lblDetailPhoneValue.Location = new Point(150, 18);
        lblDetailPhoneValue.Name = "lblDetailPhoneValue";
        lblDetailPhoneValue.Size = new Size(170, 20);
        lblDetailPhoneValue.Text = "—";
        lblDetailPhoneValue.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblDetailStatusValue
        // 
        lblDetailStatusValue.AutoSize = false;
        lblDetailStatusValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDetailStatusValue.Location = new Point(60, 18);
        lblDetailStatusValue.Name = "lblDetailStatusValue";
        lblDetailStatusValue.Size = new Size(80, 20);
        lblDetailStatusValue.Text = "—";
        lblDetailStatusValue.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblDetailBalanceValue
        // 
        lblDetailBalanceValue.AutoSize = false;
        lblDetailBalanceValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDetailBalanceValue.ForeColor = ColorTranslator.FromHtml("#D64545");
        lblDetailBalanceValue.Location = new Point(12, 14);
        lblDetailBalanceValue.Name = "lblDetailBalanceValue";
        lblDetailBalanceValue.Size = new Size(140, 24);
        lblDetailBalanceValue.Text = "0.00";
        lblDetailBalanceValue.TextAlign = ContentAlignment.MiddleCenter;
        lblDetailBalanceValue.AccessibleName = "الرصيد المستحق للعميل المحدد";
        // 
        // actionsFlow
        // 
        actionsFlow.Controls.Add(btnAdd);
        actionsFlow.Controls.Add(btnCollect);
        actionsFlow.Controls.Add(btnDetails);
        actionsFlow.Controls.Add(btnViewMeters);
        actionsFlow.Controls.Add(btnAddMeter);
        actionsFlow.Controls.Add(btnExit);
        actionsFlow.Dock = DockStyle.Bottom;
        actionsFlow.FlowDirection = FlowDirection.RightToLeft;
        actionsFlow.Location = new Point(8, 152);
        actionsFlow.Name = "actionsFlow";
        actionsFlow.Padding = new Padding(2);
        actionsFlow.RightToLeft = RightToLeft.Yes;
        actionsFlow.Size = new Size(1084, 36);
        actionsFlow.TabIndex = 1;

        // 
        // btnAdd
        // 
        btnAdd.BackColor = ColorTranslator.FromHtml("#0B69A3");
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Height = 30;
        btnAdd.Margin = new Padding(4);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(140, 30);
        btnAdd.TabIndex = 6;
        btnAdd.Text = "إضافة عميل";
        btnAdd.UseVisualStyleBackColor = false;

        // 
        // btnCollect
        // 
        btnCollect.BackColor = ColorTranslator.FromHtml("#0B69A3");
        btnCollect.FlatStyle = FlatStyle.Flat;
        btnCollect.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnCollect.ForeColor = Color.White;
        btnCollect.Height = 30;
        btnCollect.Margin = new Padding(4);
        btnCollect.Name = "btnCollect";
        btnCollect.Size = new Size(140, 30);
        btnCollect.TabIndex = 9;
        btnCollect.Text = "التحصيل";
        btnCollect.UseVisualStyleBackColor = false;
        // 
        // btnDetails
        // 
        btnDetails.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        btnDetails.FlatStyle = FlatStyle.Flat;
        btnDetails.Font = new Font("Segoe UI", 9F);
        btnDetails.Height = 30;
        btnDetails.Margin = new Padding(4);
        btnDetails.Name = "btnDetails";
        btnDetails.Size = new Size(120, 30);
        btnDetails.TabIndex = 7;
        btnDetails.Text = "عرض التفاصيل";
        btnDetails.UseVisualStyleBackColor = true;

        // 
        // btnViewMeters
        // 
        btnViewMeters.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        btnViewMeters.FlatStyle = FlatStyle.Flat;
        btnViewMeters.Font = new Font("Segoe UI", 9F);
        btnViewMeters.Height = 30;
        btnViewMeters.Margin = new Padding(4);
        btnViewMeters.Name = "btnViewMeters";
        btnViewMeters.Size = new Size(140, 30);
        btnViewMeters.TabIndex = 8;
        btnViewMeters.Text = "عرض العدادات";
        btnViewMeters.UseVisualStyleBackColor = true;

        // 
        // btnAddMeter
        // 
        btnAddMeter.BackColor = ColorTranslator.FromHtml("#0B69A3");
        btnAddMeter.FlatStyle = FlatStyle.Flat;
        btnAddMeter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnAddMeter.ForeColor = Color.White;
        btnAddMeter.Height = 30;
        btnAddMeter.Margin = new Padding(4);
        btnAddMeter.Name = "btnAddMeter";
        btnAddMeter.Size = new Size(140, 30);
        btnAddMeter.TabIndex = 10;
        btnAddMeter.Text = "إضافة عداد";
        btnAddMeter.UseVisualStyleBackColor = false;

        // 
        // btnExit
        // 
        btnExit.BackColor = ColorTranslator.FromHtml("#F5F7FA");
        btnExit.FlatStyle = FlatStyle.Flat;
        btnExit.Font = new Font("Segoe UI", 9F);
        btnExit.Height = 30;
        btnExit.Margin = new Padding(4);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(200, 30);
        btnExit.TabIndex = 11;
        btnExit.Text = "خروج";
        btnExit.UseVisualStyleBackColor = true;

        // 
        // pbLoading
        // 
        pbLoading.Dock = DockStyle.Bottom;
        pbLoading.Location = new Point(0, 160);
        pbLoading.Name = "pbLoading";
        pbLoading.Size = new Size(1100, 4);
        pbLoading.Style = ProgressBarStyle.Marquee;
        pbLoading.TabIndex = 1;
        pbLoading.Visible = false;

        // 
        // dgvCustomers
        // 
        dgvCustomers.AllowUserToAddRows = false;
        dgvCustomers.AllowUserToDeleteRows = false;
        dgvCustomers.AutoGenerateColumns = false;
        dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCustomers.ColumnHeadersHeight = 32;
        dgvCustomers.Dock = DockStyle.Fill;
        dgvCustomers.Location = new Point(0, 164);
        dgvCustomers.MultiSelect = false;
        dgvCustomers.Name = "dgvCustomers";
        dgvCustomers.ReadOnly = true;
        dgvCustomers.RowHeadersVisible = false;
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCustomers.Size = new Size(1100, 488);
        dgvCustomers.TabIndex = 2;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { tslblStatus, tslblCount });
        statusStrip.Location = new Point(0, 652);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1100, 24);
        statusStrip.TabIndex = 3;

        // 
        // tslblStatus
        // 
        tslblStatus.Name = "tslblStatus";
        tslblStatus.Size = new Size(40, 19);
        tslblStatus.Text = "جاهز";

        // 
        // tslblCount
        // 
        tslblCount.Name = "tslblCount";
        tslblCount.Size = new Size(1045, 19);
        tslblCount.Spring = true;
        tslblCount.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // CustomersForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 676);
        Controls.Add(dgvCustomers);
        Controls.Add(pbLoading);
        Controls.Add(pnlToolbar);
        Controls.Add(statusStrip);
        Font = new Font("Segoe UI", 9F);
        KeyPreview = true;
        MinimumSize = new Size(860, 520);
        Name = "CustomersForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "إدارة العملاء - WaterStation";
        pnlToolbar.ResumeLayout(false);
        grpSearch.ResumeLayout(false);
        grpSearch.PerformLayout();
        actionsFlow.ResumeLayout(false);
        actionsFlow.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
    }
}