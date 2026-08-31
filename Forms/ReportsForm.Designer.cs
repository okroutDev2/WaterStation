#nullable enable

namespace WaterStation.Forms;

partial class ReportsForm
{
    private System.ComponentModel.IContainer? components = null;

    private TableLayoutPanel tblHeader = null!;
    private Button btnExit = null!;
    private Label lblTitle = null!;
    private TabControl tabReports = null!;
    private TabPage tabOpenInvoices = null!;
    private TabPage tabBalances = null!;
    private TabPage tabReceipts = null!;
    private TabPage tabReadings = null!;

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
        tblHeader = new TableLayoutPanel();
        btnExit = new Button();
        lblTitle = new Label();
        tabReports = new TabControl();
        tabOpenInvoices = new TabPage();
        tabBalances = new TabPage();
        tabReceipts = new TabPage();
        tabReadings = new TabPage();
        tblHeader.SuspendLayout();
        tabReports.SuspendLayout();
        SuspendLayout();
        // 
        // tblHeader
        // 
        tblHeader.ColumnCount = 3;
        tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
        tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
        tblHeader.Controls.Add(btnExit, 0, 0);
        tblHeader.Controls.Add(lblTitle, 1, 0);
        tblHeader.Dock = DockStyle.Top;
        tblHeader.Location = new Point(0, 0);
        tblHeader.Name = "tblHeader";
        tblHeader.RowCount = 1;
        tblHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tblHeader.Size = new Size(1100, 48);
        tblHeader.TabIndex = 0;
        // 
        // btnExit
        // 
        btnExit.Anchor = AnchorStyles.Left;
        btnExit.BackColor = Color.FromArgb(240, 240, 240);
        btnExit.FlatStyle = FlatStyle.Flat;
        btnExit.Font = new Font("Segoe UI", 9F);
        btnExit.Location = new Point(8, 8);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(160, 32);
        btnExit.TabIndex = 1;
        btnExit.Text = "خروج";
        btnExit.UseVisualStyleBackColor = true;
        // 
        // lblTitle
        // 
        lblTitle.Dock = DockStyle.Fill;
        lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(0, 92, 165);
        lblTitle.Location = new Point(180, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(740, 48);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "التقارير";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tabReports
        // 
        tabReports.Controls.Add(tabOpenInvoices);
        tabReports.Controls.Add(tabBalances);
        tabReports.Controls.Add(tabReceipts);
        tabReports.Controls.Add(tabReadings);
        tabReports.Dock = DockStyle.Fill;
        tabReports.Location = new Point(0, 48);
        tabReports.Name = "tabReports";
        tabReports.RightToLeft = RightToLeft.Yes;
        tabReports.RightToLeftLayout = true;
        tabReports.SelectedIndex = 0;
        tabReports.Size = new Size(1100, 608);
        tabReports.TabIndex = 0;
        // 
        // tabOpenInvoices
        // 
        tabOpenInvoices.Location = new Point(4, 26);
        tabOpenInvoices.Name = "tabOpenInvoices";
        tabOpenInvoices.Padding = new Padding(3);
        tabOpenInvoices.RightToLeft = RightToLeft.Yes;
        tabOpenInvoices.Size = new Size(1092, 578);
        tabOpenInvoices.TabIndex = 0;
        tabOpenInvoices.Text = "الفواتير المفتوحة";
        tabOpenInvoices.UseVisualStyleBackColor = true;
        // 
        // tabBalances
        // 
        tabBalances.Location = new Point(4, 26);
        tabBalances.Name = "tabBalances";
        tabBalances.Padding = new Padding(3);
        tabBalances.RightToLeft = RightToLeft.Yes;
        tabBalances.Size = new Size(1092, 578);
        tabBalances.TabIndex = 1;
        tabBalances.Text = "أرصدة الفواتير";
        tabBalances.UseVisualStyleBackColor = true;
        // 
        // tabReceipts
        // 
        tabReceipts.Location = new Point(4, 26);
        tabReceipts.Name = "tabReceipts";
        tabReceipts.Padding = new Padding(3);
        tabReceipts.RightToLeft = RightToLeft.Yes;
        tabReceipts.Size = new Size(1092, 578);
        tabReceipts.TabIndex = 2;
        tabReceipts.Text = "الإيصالات";
        tabReceipts.UseVisualStyleBackColor = true;
        // 
        // tabReadings
        // 
        tabReadings.Location = new Point(4, 26);
        tabReadings.Name = "tabReadings";
        tabReadings.Padding = new Padding(3);
        tabReadings.RightToLeft = RightToLeft.Yes;
        tabReadings.Size = new Size(1092, 578);
        tabReadings.TabIndex = 3;
        tabReadings.Text = "قراءات العدادات";
        tabReadings.UseVisualStyleBackColor = true;
        //
        // ReportsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 656);
        Controls.Add(tabReports);
        Controls.Add(tblHeader);
        Font = new Font("Segoe UI", 9F);
        KeyPreview = true;
        MinimumSize = new Size(860, 520);
        Name = "ReportsForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "التقارير - WaterStation";
        tblHeader.ResumeLayout(false);
        tblHeader.PerformLayout();
        tabReports.ResumeLayout(false);
        ResumeLayout(false);
    }
}