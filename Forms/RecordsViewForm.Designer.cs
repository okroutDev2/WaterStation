#nullable enable

namespace WaterStation.Forms;

partial class RecordsViewForm<T>
{
    private System.ComponentModel.IContainer? components = null;

    private Panel pnlToolbar = null!;
    private Label lblScreenTitle = null!;
    private GroupBox grpSearch = null!;
    private Label lblSearchCaption = null!;
    private TextBox txtSearch = null!;
    private Button btnSearch = null!;
    private Button btnClear = null!;
    private Button btnAction = null!;

    protected DataGridView dgvList = null!;

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
        var dgvCellStyleHeader = new DataGridViewCellStyle();

        pnlToolbar = new Panel();
        lblScreenTitle = new Label();
        grpSearch = new GroupBox();
        lblSearchCaption = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        btnAction = new Button();
        dgvList = new DataGridView();
        statusStrip = new StatusStrip();
        tslblStatus = new ToolStripStatusLabel();
        tslblCount = new ToolStripStatusLabel();
        pbLoading = new ProgressBar();

        pnlToolbar.SuspendLayout();
        grpSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvList).BeginInit();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // 
        // pnlToolbar
        // 
        pnlToolbar.Controls.Add(lblScreenTitle);
        pnlToolbar.Controls.Add(grpSearch);
        pnlToolbar.Controls.Add(pbLoading);
        pnlToolbar.Dock = DockStyle.Top;
        pnlToolbar.Location = new Point(0, 0);
        pnlToolbar.Name = "pnlToolbar";
        pnlToolbar.Padding = new Padding(8);
        pnlToolbar.Size = new Size(1000, 116);
        pnlToolbar.TabIndex = 0;

        // 
        // lblScreenTitle
        // 
        lblScreenTitle.Dock = DockStyle.Top;
        lblScreenTitle.Location = new Point(8, 8);
        lblScreenTitle.Name = "lblScreenTitle";
        lblScreenTitle.Size = new Size(984, 34);
        lblScreenTitle.TabIndex = 0;
        lblScreenTitle.Text = "السجلات";

        // 
        // grpSearch
        // 
        grpSearch.Controls.Add(lblSearchCaption);
        grpSearch.Controls.Add(txtSearch);
        grpSearch.Controls.Add(btnSearch);
        grpSearch.Controls.Add(btnClear);
        grpSearch.Controls.Add(btnAction);
        grpSearch.Dock = DockStyle.Fill;
        grpSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpSearch.Location = new Point(8, 8);
        grpSearch.Name = "grpSearch";
        grpSearch.Size = new Size(984, 66);
        grpSearch.TabIndex = 0;
        grpSearch.TabStop = false;
        grpSearch.Text = "بحث";

        // lblSearchCaption
        lblSearchCaption.AutoSize = true;
        lblSearchCaption.Font = new Font("Segoe UI", 9F);
        lblSearchCaption.Location = new Point(930, 28);
        lblSearchCaption.Name = "lblSearchCaption";
        lblSearchCaption.Size = new Size(85, 15);
        lblSearchCaption.TabIndex = 0;
        lblSearchCaption.Text = "معيار البحث:";

        // txtSearch
        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.Location = new Point(700, 25);
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new Size(220, 24);
        txtSearch.TabIndex = 1;

        // btnSearch
        btnSearch.BackColor = Color.FromArgb(0, 122, 204);
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSearch.ForeColor = Color.White;
        btnSearch.Location = new Point(600, 23);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(90, 28);
        btnSearch.TabIndex = 2;
        btnSearch.Text = "بحث";
        btnSearch.UseVisualStyleBackColor = false;

        // btnClear
        btnClear.BackColor = Color.FromArgb(240, 240, 240);
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9F);
        btnClear.Location = new Point(500, 23);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(90, 28);
        btnClear.TabIndex = 3;
        btnClear.Text = "مسح";
        btnClear.UseVisualStyleBackColor = true;

        // btnAction
        btnAction.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAction.BackColor = Color.FromArgb(0, 122, 204);
        btnAction.FlatStyle = FlatStyle.Flat;
        btnAction.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAction.ForeColor = Color.White;
        btnAction.Location = new Point(15, 20);
        btnAction.Name = "btnAction";
        btnAction.Size = new Size(150, 34);
        btnAction.TabIndex = 4;
        btnAction.Text = "إجراء";
        btnAction.UseVisualStyleBackColor = false;
        btnAction.Visible = false;

        // 
        // pbLoading
        // 
        pbLoading.Dock = DockStyle.Bottom;
        pbLoading.Location = new Point(0, 0);
        pbLoading.Name = "pbLoading";
        pbLoading.Size = new Size(1000, 4);
        pbLoading.Style = ProgressBarStyle.Marquee;
        pbLoading.TabIndex = 1;
        pbLoading.Visible = false;

        // 
        // dgvList
        // 
        dgvCellStyleHeader.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvCellStyleHeader.BackColor = Color.FromArgb(235, 240, 245);
        dgvCellStyleHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvCellStyleHeader.ForeColor = Color.Black;
        dgvCellStyleHeader.WrapMode = DataGridViewTriState.False;
        dgvList.AutoGenerateColumns = false;
        dgvList.ColumnHeadersDefaultCellStyle = dgvCellStyleHeader;
        dgvList.ColumnHeadersHeight = 32;
        dgvList.Dock = DockStyle.Fill;
        dgvList.Location = new Point(0, 82);
        dgvList.Name = "dgvList";
        dgvList.RowHeadersVisible = false;
        dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvList.MultiSelect = false;
        dgvList.ReadOnly = true;
        dgvList.AllowUserToAddRows = false;
        dgvList.AllowUserToDeleteRows = false;
        dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvList.Size = new Size(1000, 452);
        dgvList.TabIndex = 1;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { tslblStatus, tslblCount });
        statusStrip.Location = new Point(0, 534);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1000, 24);
        statusStrip.TabIndex = 2;

        // tslblStatus
        tslblStatus.Name = "tslblStatus";
        tslblStatus.Size = new Size(40, 19);
        tslblStatus.Text = "جاهز";

        // tslblCount
        tslblCount.Name = "tslblCount";
        tslblCount.Size = new Size(945, 19);
        tslblCount.Spring = true;
        tslblCount.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // RecordsViewForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 558);
        Controls.Add(dgvList);
        Controls.Add(pnlToolbar);
        Controls.Add(statusStrip);
        Font = new Font("Segoe UI", 9F);
        KeyPreview = true;
        MinimumSize = new Size(780, 470);
        Name = "RecordsViewForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;

        pnlToolbar.ResumeLayout(false);
        grpSearch.ResumeLayout(false);
        grpSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvList).EndInit();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
    }
}