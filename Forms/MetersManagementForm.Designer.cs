#nullable enable

namespace WaterStation.Forms;

partial class MetersManagementForm
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlToolbar = null!;
    private GroupBox grpSearch = null!;
    private Label lblSearchCaption = null!;
    private TextBox txtSearch = null!;
    private Button btnSearch = null!;
    private Button btnClear = null!;
    private Button btnRefresh = null!;
    private Label lblBranchCaption = null!;
    private ComboBox cmbBranch = null!;
    private Label lblAreaCaption = null!;
    private ComboBox cmbArea = null!;
    private Label lblTypeCaption = null!;
    private ComboBox cmbMeterType = null!;
    private FlowLayoutPanel actionsFlow = null!;
    private Button btnAddMeter = null!;
    private Button btnReading = null!;
    private Button btnCollection = null!;
    private Button btnExit = null!;
    private DataGridView dgvMeters = null!;
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
        grpSearch = new GroupBox();
        lblSearchCaption = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        btnRefresh = new Button();
        lblBranchCaption = new Label();
        cmbBranch = new ComboBox();
        lblAreaCaption = new Label();
        cmbArea = new ComboBox();
        lblTypeCaption = new Label();
        cmbMeterType = new ComboBox();
        actionsFlow = new FlowLayoutPanel();
        btnAddMeter = new Button();
        btnReading = new Button();
        btnCollection = new Button();
        btnExit = new Button();
        pbLoading = new ProgressBar();
        dgvMeters = new DataGridView();
        statusStrip = new StatusStrip();
        tslblStatus = new ToolStripStatusLabel();
        tslblCount = new ToolStripStatusLabel();
        pnlToolbar.SuspendLayout();
        grpSearch.SuspendLayout();
        actionsFlow.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvMeters).BeginInit();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // 
        // pnlToolbar
        // 
        pnlToolbar.Controls.Add(grpSearch);
        pnlToolbar.Controls.Add(actionsFlow);
        pnlToolbar.Dock = DockStyle.Top;
        pnlToolbar.Location = new Point(0, 0);
        pnlToolbar.Name = "pnlToolbar";
        pnlToolbar.Padding = new Padding(8);
        pnlToolbar.Size = new Size(1100, 172);
        pnlToolbar.TabIndex = 0;

        // 
        // grpSearch
        // 
        grpSearch.Controls.Add(lblSearchCaption);
        grpSearch.Controls.Add(txtSearch);
        grpSearch.Controls.Add(btnSearch);
        grpSearch.Controls.Add(btnClear);
        grpSearch.Controls.Add(btnRefresh);
        grpSearch.Controls.Add(lblBranchCaption);
        grpSearch.Controls.Add(cmbBranch);
        grpSearch.Controls.Add(lblAreaCaption);
        grpSearch.Controls.Add(cmbArea);
        grpSearch.Controls.Add(lblTypeCaption);
        grpSearch.Controls.Add(cmbMeterType);
        grpSearch.Dock = DockStyle.Top;
        grpSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpSearch.Location = new Point(8, 8);
        grpSearch.Name = "grpSearch";
        grpSearch.Padding = new Padding(8, 2, 8, 2);
        grpSearch.Size = new Size(1084, 120);
        grpSearch.TabIndex = 0;
        grpSearch.TabStop = false;
        grpSearch.Text = "بحث في العدادات النشطة";

        // 
        // lblSearchCaption
        // 
        lblSearchCaption.AutoSize = true;
        lblSearchCaption.Font = new Font("Segoe UI", 9F);
        lblSearchCaption.Location = new Point(1022, 22);
        lblSearchCaption.Name = "lblSearchCaption";
        lblSearchCaption.Size = new Size(50, 15);
        lblSearchCaption.TabIndex = 0;
        lblSearchCaption.Text = "بحث:";

        // 
        // txtSearch
        // 
        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.Location = new Point(646, 18);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "رقم العداد / رقم العميل / اسم العميل";
        txtSearch.Size = new Size(368, 24);
        txtSearch.TabIndex = 1;

        // 
        // btnSearch
        // 
        btnSearch.BackColor = Color.FromArgb(0, 122, 204);
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSearch.ForeColor = Color.White;
        btnSearch.Location = new Point(552, 16);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(88, 28);
        btnSearch.TabIndex = 2;
        btnSearch.Text = "بحث";
        btnSearch.UseVisualStyleBackColor = false;

        // 
        // btnClear
        // 
        btnClear.BackColor = Color.FromArgb(240, 240, 240);
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9F);
        btnClear.Location = new Point(458, 16);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(88, 28);
        btnClear.TabIndex = 3;
        btnClear.Text = "مسح";
        btnClear.UseVisualStyleBackColor = true;

        // 
        // btnRefresh
        // 
        btnRefresh.BackColor = Color.FromArgb(240, 240, 240);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 9F);
        btnRefresh.Location = new Point(364, 16);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 28);
        btnRefresh.TabIndex = 4;
        btnRefresh.Text = "تحديث";
        btnRefresh.UseVisualStyleBackColor = true;

        // 
        // lblBranchCaption
        // 
        lblBranchCaption.AutoSize = true;
        lblBranchCaption.Font = new Font("Segoe UI", 9F);
        lblBranchCaption.Location = new Point(996, 53);
        lblBranchCaption.Name = "lblBranchCaption";
        lblBranchCaption.Size = new Size(72, 15);
        lblBranchCaption.TabIndex = 8;
        lblBranchCaption.Text = "الفرع:";

        // 
        // cmbBranch
        // 
        cmbBranch.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBranch.Font = new Font("Segoe UI", 9F);
        cmbBranch.FormattingEnabled = true;
        cmbBranch.Location = new Point(700, 50);
        cmbBranch.Name = "cmbBranch";
        cmbBranch.Size = new Size(296, 23);
        cmbBranch.TabIndex = 9;

        // 
        // lblAreaCaption
        // 
        lblAreaCaption.AutoSize = true;
        lblAreaCaption.Font = new Font("Segoe UI", 9F);
        lblAreaCaption.Location = new Point(640, 53);
        lblAreaCaption.Name = "lblAreaCaption";
        lblAreaCaption.Size = new Size(72, 15);
        lblAreaCaption.TabIndex = 10;
        lblAreaCaption.Text = "المنطقة:";

        // 
        // cmbArea
        // 
        cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbArea.Font = new Font("Segoe UI", 9F);
        cmbArea.FormattingEnabled = true;
        cmbArea.Location = new Point(344, 50);
        cmbArea.Name = "cmbArea";
        cmbArea.Size = new Size(296, 23);
        cmbArea.TabIndex = 11;

        // 
        // lblTypeCaption
        // 
        lblTypeCaption.AutoSize = true;
        lblTypeCaption.Font = new Font("Segoe UI", 9F);
        lblTypeCaption.Location = new Point(284, 53);
        lblTypeCaption.Name = "lblTypeCaption";
        lblTypeCaption.Size = new Size(72, 15);
        lblTypeCaption.TabIndex = 12;
        lblTypeCaption.Text = "نوع العداد:";

        // 
        // cmbMeterType
        // 
        cmbMeterType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMeterType.Font = new Font("Segoe UI", 9F);
        cmbMeterType.FormattingEnabled = true;
        cmbMeterType.Location = new Point(8, 50);
        cmbMeterType.Name = "cmbMeterType";
        cmbMeterType.Size = new Size(276, 23);
        cmbMeterType.TabIndex = 13;

        // 
        // actionsFlow
        // 
        actionsFlow.Controls.Add(btnAddMeter);
        actionsFlow.Controls.Add(btnReading);
        actionsFlow.Controls.Add(btnCollection);
        actionsFlow.Controls.Add(btnExit);
        actionsFlow.Dock = DockStyle.Bottom;
        actionsFlow.FlowDirection = FlowDirection.RightToLeft;
        actionsFlow.Location = new Point(8, 128);
        actionsFlow.Name = "actionsFlow";
        actionsFlow.Padding = new Padding(2);
        actionsFlow.RightToLeft = RightToLeft.Yes;
        actionsFlow.Size = new Size(1084, 36);
        actionsFlow.TabIndex = 1;

        // 
        // btnAddMeter
        // 
        btnAddMeter.BackColor = Color.FromArgb(0, 122, 204);
        btnAddMeter.FlatStyle = FlatStyle.Flat;
        btnAddMeter.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAddMeter.ForeColor = Color.White;
        btnAddMeter.Height = 30;
        btnAddMeter.Margin = new Padding(4);
        btnAddMeter.Name = "btnAddMeter";
        btnAddMeter.Size = new Size(140, 30);
        btnAddMeter.TabIndex = 5;
        btnAddMeter.Text = "إضافة عداد";
        btnAddMeter.UseVisualStyleBackColor = false;

        // 
        // btnReading
        // 
        btnReading.BackColor = Color.FromArgb(0, 122, 204);
        btnReading.FlatStyle = FlatStyle.Flat;
        btnReading.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnReading.ForeColor = Color.White;
        btnReading.Height = 30;
        btnReading.Margin = new Padding(4);
        btnReading.Name = "btnReading";
        btnReading.Size = new Size(150, 30);
        btnReading.TabIndex = 6;
        btnReading.Text = "إدخال قراءة";
        btnReading.UseVisualStyleBackColor = false;

        // 
        // btnCollection
        // 
        btnCollection.BackColor = Color.FromArgb(0, 122, 204);
        btnCollection.FlatStyle = FlatStyle.Flat;
        btnCollection.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnCollection.ForeColor = Color.White;
        btnCollection.Height = 30;
        btnCollection.Margin = new Padding(4);
        btnCollection.Name = "btnCollection";
        btnCollection.Size = new Size(170, 30);
        btnCollection.TabIndex = 7;
        btnCollection.Text = "التحصيل";
        btnCollection.UseVisualStyleBackColor = false;

        // 
        // btnExit
        // 
        btnExit.BackColor = Color.FromArgb(240, 240, 240);
        btnExit.FlatStyle = FlatStyle.Flat;
        btnExit.Font = new Font("Segoe UI", 9F);
        btnExit.Height = 30;
        btnExit.Margin = new Padding(4);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(200, 30);
        btnExit.TabIndex = 8;
        btnExit.Text = "خروج";
        btnExit.UseVisualStyleBackColor = true;

        // 
        // pbLoading
        // 
        pbLoading.Dock = DockStyle.Bottom;
        pbLoading.Location = new Point(0, 136);
        pbLoading.Name = "pbLoading";
        pbLoading.Size = new Size(1100, 4);
        pbLoading.Style = ProgressBarStyle.Marquee;
        pbLoading.TabIndex = 1;
        pbLoading.Visible = false;

        // 
        // dgvMeters
        // 
        dgvMeters.AllowUserToAddRows = false;
        dgvMeters.AllowUserToDeleteRows = false;
        dgvMeters.AutoGenerateColumns = false;
        dgvMeters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMeters.ColumnHeadersHeight = 32;
        dgvMeters.Dock = DockStyle.Fill;
        dgvMeters.Location = new Point(0, 144);
        dgvMeters.MultiSelect = false;
        dgvMeters.Name = "dgvMeters";
        dgvMeters.ReadOnly = true;
        dgvMeters.RowHeadersVisible = false;
        dgvMeters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMeters.Size = new Size(1100, 488);
        dgvMeters.TabIndex = 2;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { tslblStatus, tslblCount });
        statusStrip.Location = new Point(0, 632);
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
        // MetersManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 656);
        Controls.Add(dgvMeters);
        Controls.Add(pbLoading);
        Controls.Add(pnlToolbar);
        Controls.Add(statusStrip);
        Font = new Font("Segoe UI", 9F);
        KeyPreview = true;
        MinimumSize = new Size(860, 520);
        Name = "MetersManagementForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "إدارة العدادات - WaterStation";
        pnlToolbar.ResumeLayout(false);
        grpSearch.ResumeLayout(false);
        grpSearch.PerformLayout();
        actionsFlow.ResumeLayout(false);
        actionsFlow.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvMeters).EndInit();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
    }
}