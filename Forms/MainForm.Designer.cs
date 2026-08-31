#nullable enable

namespace WaterStation.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    private Label titleLabel = null!;
    private Label lblSubtitle = null!;
    private Panel pnlHeader = null!;
    private Panel pnlSidebar = null!;
    private FlowLayoutPanel pnlNavFlow = null!;
    private Panel pnlContent = null!;
    private StatusStrip statusStrip = null!;
    private ToolStripStatusLabel tslblStatus = null!;
    private ToolStripStatusLabel tslblClock = null!;
    private ToolStripProgressBar tspbProgress = null!;
    private System.Windows.Forms.Timer tmrClock = null!;

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
        pnlHeader = new Panel();
        lblSubtitle = new Label();
        titleLabel = new Label();
        pnlSidebar = new Panel();
        pnlNavFlow = new FlowLayoutPanel();
        pnlContent = new Panel();
        statusStrip = new StatusStrip();
        tslblStatus = new ToolStripStatusLabel();
        tslblClock = new ToolStripStatusLabel();
        tspbProgress = new ToolStripProgressBar();
        tmrClock = new System.Windows.Forms.Timer(components);
        pnlHeader.SuspendLayout();
        pnlSidebar.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = ColorTranslator.FromHtml("#064C73");
        pnlHeader.Controls.Add(lblSubtitle);
        pnlHeader.Controls.Add(titleLabel);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1100, 74);
        pnlHeader.TabIndex = 0;
        // 
        // titleLabel
        // 
        titleLabel.Dock = DockStyle.Top;
        titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        titleLabel.ForeColor = Color.White;
        titleLabel.Location = new Point(0, 0);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(1100, 46);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "نظام إدارة وفوترة محطة المياه";
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblSubtitle
        // 
        lblSubtitle.Dock = DockStyle.Top;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F);
        lblSubtitle.ForeColor = Color.FromArgb(210, 225, 240);
        lblSubtitle.Location = new Point(0, 46);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(1100, 26);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "التحصيل · الفوترة · العدادات · القراءات · التقارير";
        lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // pnlSidebar
        // 
        pnlSidebar.BackColor = ColorTranslator.FromHtml("#F5F7FA");
        pnlSidebar.BorderStyle = BorderStyle.FixedSingle;
        pnlSidebar.Controls.Add(pnlNavFlow);
        pnlSidebar.Dock = DockStyle.Left;
        pnlSidebar.Location = new Point(0, 74);
        pnlSidebar.Name = "pnlSidebar";
        pnlSidebar.Padding = new Padding(10, 12, 10, 10);
        pnlSidebar.Size = new Size(250, 558);
        pnlSidebar.TabIndex = 1;
        // 
        // pnlNavFlow
        // 
        pnlNavFlow.AutoScroll = true;
        pnlNavFlow.Dock = DockStyle.Fill;
        pnlNavFlow.FlowDirection = FlowDirection.TopDown;
        pnlNavFlow.Location = new Point(10, 12);
        pnlNavFlow.Name = "pnlNavFlow";
        pnlNavFlow.Padding = new Padding(0);
        pnlNavFlow.RightToLeft = RightToLeft.Yes;
        pnlNavFlow.Size = new Size(228, 534);
        pnlNavFlow.TabIndex = 0;
        pnlNavFlow.WrapContents = false;
        // 
        // pnlContent
        // 
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(250, 74);
        pnlContent.Name = "pnlContent";
        pnlContent.Size = new Size(850, 558);
        pnlContent.TabIndex = 2;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { tslblStatus, tslblClock, tspbProgress });
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
        // tslblClock
        // 
        tslblClock.Alignment = ToolStripItemAlignment.Right;
        tslblClock.Name = "tslblClock";
        tslblClock.Size = new Size(80, 19);
        tslblClock.Text = "—";
        // 
        // tspbProgress
        // 
        tspbProgress.Name = "tspbProgress";
        tspbProgress.Size = new Size(100, 18);
        tspbProgress.Style = ProgressBarStyle.Marquee;
        tspbProgress.Visible = false;
        // 
        // tmrClock
        // 
        tmrClock.Interval = 30000;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 656);
        Controls.Add(pnlContent);
        Controls.Add(pnlSidebar);
        Controls.Add(pnlHeader);
        Controls.Add(statusStrip);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(960, 560);
        Name = "MainForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "نظام إدارة وفوترة محطة المياه";
        pnlHeader.ResumeLayout(false);
        pnlSidebar.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
    }
}