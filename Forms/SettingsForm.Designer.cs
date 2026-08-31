#nullable enable

namespace WaterStation.Forms;

partial class SettingsForm
{
    private System.ComponentModel.IContainer? components = null;

    private TableLayoutPanel tblHeader = null!;
    private Button btnExit = null!;
    private Label lblTitle = null!;
    private Panel pnlBody = null!;

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
        pnlBody = new Panel();
        tblHeader.SuspendLayout();
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
        lblTitle.Text = "الإعدادات";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // pnlBody
        // 
        pnlBody.AutoScroll = true;
        pnlBody.Dock = DockStyle.Fill;
        pnlBody.Location = new Point(0, 48);
        pnlBody.Name = "pnlBody";
        pnlBody.Padding = new Padding(8);
        pnlBody.Size = new Size(1100, 608);
        pnlBody.TabIndex = 0;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 656);
        Controls.Add(pnlBody);
        Controls.Add(tblHeader);
        Font = new Font("Segoe UI", 9F);
        KeyPreview = true;
        MinimumSize = new Size(860, 520);
        Name = "SettingsForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "الإعدادات - WaterStation";
        tblHeader.ResumeLayout(false);
        tblHeader.PerformLayout();
        ResumeLayout(false);
    }
}