#nullable enable
using WaterStation.Infrastructure;

namespace WaterStation.Forms;

partial class FieldCollectionForm
{
    private System.ComponentModel.IContainer? components = null;

    // Header
    private Panel pnlHeader = null!;
    private Label lblScreenTitle = null!;

    // Search Bar
    private Panel pnlSearch = null!;
    private FlowLayoutPanel pnlSearchFlow = null!;
    private Label lblCustomerNumber = null!;
    private TextBox txtCustomerNumber = null!;
    private Label lblMeterNumber = null!;
    private TextBox txtMeterNumber = null!;
    private Label lblCustomerId = null!;
    private TextBox txtCustomerId = null!;
    private Button btnSearch = null!;
    private Button btnClear = null!;
    private Label lblSearchHint = null!;

    // Customer + Meter Cards
    private TableLayoutPanel tblCards = null!;
    private GroupBox grpCustomer = null!;
    private TableLayoutPanel tblCustomer = null!;
    private Label lblCNumT = null!;
    private Label lblCNumV = null!;
    private Label lblCPhoneT = null!;
    private Label lblCPhoneV = null!;
    private Label lblCNameT = null!;
    private Label lblCNameV = null!;
    private Label lblCStatusT = null!;
    private Label lblCStatusV = null!;
    private Label lblCAddrT = null!;
    private Label lblCAddrV = null!;

    private GroupBox grpMeter = null!;
    private TableLayoutPanel tblMeter = null!;
    private Label lblMeterPick = null!;
    private ComboBox cmbMeters = null!;
    private Label lblMNumT = null!;
    private Label lblMNumV = null!;
    private Label lblMTypeT = null!;
    private Label lblMTypeV = null!;
    private Label lblMDirT = null!;
    private Label lblMDirV = null!;
    private Label lblMBranchT = null!;
    private Label lblMBranchV = null!;
    private Label lblMAreaT = null!;
    private Label lblMAreaV = null!;
    private Label lblMLastReadT = null!;
    private Label lblMLastReadV = null!;
    private Label lblMLastReadDateT = null!;
    private Label lblMLastReadDateV = null!;
    private Label lblMLastConsT = null!;
    private Label lblMLastConsV = null!;
    private Label lblMCumulT = null!;
    private Label lblMCumulV = null!;
    private Label lblMeterNotice = null!;

    // Quick Actions
    private Panel pnlActions = null!;
    private FlowLayoutPanel pnlActionsFlow = null!;
    private Label lblPeriod = null!;
    private NumericUpDown nudBillingYear = null!;
    private ComboBox cmbBillingMonth = null!;
    private Button btnCreateReading = null!;
    private Button btnCreateInvoice = null!;
    private Button btnPayInvoice = null!;
    private Button btnReversePayment = null!;

    // Grids area
    private TableLayoutPanel tblGrids = null!;

    // Open Invoices
    private Panel pnlInvArea = null!;
    private Label lblNoInvoices = null!;
    private DataGridView dgvOpenInvoices = null!;
    private Panel pnlInvSummary = null!;
    private FlowLayoutPanel pnlInvSummaryFlow = null!;
    private Label lblInvTotalT = null!;
    private Label lblInvTotalV = null!;
    private Label lblInvPaidT = null!;
    private Label lblInvPaidV = null!;
    private Label lblInvBalT = null!;
    private Label lblInvBalV = null!;
    private Label lblInvStatusT = null!;
    private Label lblInvStatusV = null!;

    // Payment History
    private Panel pnlPayArea = null!;
    private Label lblNoPayments = null!;
    private DataGridView dgvPayments = null!;
    private Panel pnlPaySummary = null!;
    private FlowLayoutPanel pnlPaySummaryFlow = null!;
    private Label lblPayAmtT = null!;
    private Label lblPayAmtV = null!;
    private Label lblPayMethodT = null!;
    private Label lblPayMethodV = null!;
    private Label lblPayReceiptT = null!;
    private Label lblPayReceiptV = null!;
    private Label lblPayRevT = null!;
    private Label lblPayRevV = null!;
    private Label lblPayReasonT = null!;
    private Label lblPayReasonV = null!;
    private Label lblPayCurrencyNote = null!;
    private Label lblInvCurrencyNote = null!;

    // Status Strip
    private StatusStrip statusStrip = null!;
    private ToolStripStatusLabel tslblStatus = null!;
    private ToolStripStatusLabel tslblCounts = null!;
    private ToolStripProgressBar tspbProgress = null!;

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
        var dgvCellStyleAltRow = new DataGridViewCellStyle();

        var titleStyle = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        var smallGrayStyle = new Font("Segoe UI", 8.5F);

        pnlHeader = new Panel();
        lblScreenTitle = new Label();
        pnlSearch = new Panel();
        pnlSearchFlow = new FlowLayoutPanel();
        lblCustomerNumber = new Label();
        txtCustomerNumber = new TextBox();
        lblMeterNumber = new Label();
        txtMeterNumber = new TextBox();
        lblCustomerId = new Label();
        txtCustomerId = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        lblSearchHint = new Label();

        tblCards = new TableLayoutPanel();
        grpCustomer = new GroupBox();
        tblCustomer = new TableLayoutPanel();
        lblCNumT = new Label();
        lblCNumV = new Label();
        lblCPhoneT = new Label();
        lblCPhoneV = new Label();
        lblCNameT = new Label();
        lblCNameV = new Label();
        lblCStatusT = new Label();
        lblCStatusV = new Label();
        lblCAddrT = new Label();
        lblCAddrV = new Label();
        grpMeter = new GroupBox();
        tblMeter = new TableLayoutPanel();
        lblMeterPick = new Label();
        cmbMeters = new ComboBox();
        lblMNumT = new Label();
        lblMNumV = new Label();
        lblMTypeT = new Label();
        lblMTypeV = new Label();
        lblMDirT = new Label();
        lblMDirV = new Label();
        lblMBranchT = new Label();
        lblMBranchV = new Label();
        lblMAreaT = new Label();
        lblMAreaV = new Label();
        lblMLastReadT = new Label();
        lblMLastReadV = new Label();
        lblMLastReadDateT = new Label();
        lblMLastReadDateV = new Label();
        lblMLastConsT = new Label();
        lblMLastConsV = new Label();
        lblMCumulT = new Label();
        lblMCumulV = new Label();
        lblMeterNotice = new Label();

        pnlActions = new Panel();
        pnlActionsFlow = new FlowLayoutPanel();
        lblPeriod = new Label();
        nudBillingYear = new NumericUpDown();
        cmbBillingMonth = new ComboBox();
        btnCreateReading = new Button();
        btnCreateInvoice = new Button();
        btnPayInvoice = new Button();
        btnReversePayment = new Button();

        tblGrids = new TableLayoutPanel();
        pnlInvArea = new Panel();
        lblNoInvoices = new Label();
        dgvOpenInvoices = new DataGridView();
        pnlInvSummary = new Panel();
        pnlInvSummaryFlow = new FlowLayoutPanel();
        lblInvTotalT = new Label();
        lblInvTotalV = new Label();
        lblInvPaidT = new Label();
        lblInvPaidV = new Label();
        lblInvBalT = new Label();
        lblInvBalV = new Label();
        lblInvStatusT = new Label();
        lblInvStatusV = new Label();

        pnlPayArea = new Panel();
        lblNoPayments = new Label();
        dgvPayments = new DataGridView();
        pnlPaySummary = new Panel();
        pnlPaySummaryFlow = new FlowLayoutPanel();
        lblPayAmtT = new Label();
        lblPayAmtV = new Label();
        lblPayMethodT = new Label();
        lblPayMethodV = new Label();
        lblPayReceiptT = new Label();
        lblPayReceiptV = new Label();
        lblPayRevT = new Label();
        lblPayRevV = new Label();
        lblPayReasonT = new Label();
        lblPayReasonV = new Label();
        lblPayCurrencyNote = new Label();
        lblInvCurrencyNote = new Label();

        statusStrip = new StatusStrip();
        tslblStatus = new ToolStripStatusLabel();
        tslblCounts = new ToolStripStatusLabel();
        tspbProgress = new ToolStripProgressBar();

        pnlHeader.SuspendLayout();
        pnlSearch.SuspendLayout();
        pnlSearchFlow.SuspendLayout();
        tblCards.SuspendLayout();
        grpCustomer.SuspendLayout();
        tblCustomer.SuspendLayout();
        grpMeter.SuspendLayout();
        tblMeter.SuspendLayout();
        pnlActions.SuspendLayout();
        pnlActionsFlow.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudBillingYear).BeginInit();
        tblGrids.SuspendLayout();
        pnlInvArea.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOpenInvoices).BeginInit();
        pnlInvSummary.SuspendLayout();
        pnlInvSummaryFlow.SuspendLayout();
        pnlPayArea.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
        pnlPaySummary.SuspendLayout();
        pnlPaySummaryFlow.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(0, 92, 165);
        pnlHeader.Controls.Add(lblScreenTitle);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1120, 38);
        pnlHeader.TabIndex = 0;

        // 
        // lblScreenTitle
        // 
        lblScreenTitle.Dock = DockStyle.Fill;
        lblScreenTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblScreenTitle.ForeColor = Color.White;
        lblScreenTitle.Location = new Point(0, 0);
        lblScreenTitle.Name = "lblScreenTitle";
        lblScreenTitle.Size = new Size(1120, 38);
        lblScreenTitle.TabIndex = 0;
        lblScreenTitle.Text = "التحصيل الميداني";
        lblScreenTitle.TextAlign = ContentAlignment.MiddleCenter;

        // 
        // pnlSearch
        // 
        pnlSearch.BackColor = Color.FromArgb(240, 243, 247);
        pnlSearch.Controls.Add(pnlSearchFlow);
        pnlSearch.Dock = DockStyle.Top;
        pnlSearch.Location = new Point(0, 38);
        pnlSearch.Name = "pnlSearch";
        pnlSearch.Size = new Size(1120, 58);
        pnlSearch.TabIndex = 1;

        // 
        // pnlSearchFlow
        // 
        pnlSearchFlow.Dock = DockStyle.Fill;
        pnlSearchFlow.FlowDirection = FlowDirection.LeftToRight;
        pnlSearchFlow.Padding = new Padding(8, 8, 8, 0);
        pnlSearchFlow.RightToLeft = RightToLeft.Yes;
        pnlSearchFlow.WrapContents = false;

        // lblCustomerNumber
        lblCustomerNumber.AutoSize = true;
        lblCustomerNumber.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomerNumber.Margin = new Padding(2, 8, 4, 0);
        lblCustomerNumber.Text = "رقم العميل:";

        // txtCustomerNumber
        txtCustomerNumber.Font = new Font("Segoe UI", 9.5F);
        txtCustomerNumber.Margin = new Padding(0, 4, 6, 0);
        txtCustomerNumber.Size = new Size(130, 24);
        txtCustomerNumber.TabIndex = 1;

        // lblMeterNumber
        lblMeterNumber.AutoSize = true;
        lblMeterNumber.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblMeterNumber.Margin = new Padding(2, 8, 4, 0);
        lblMeterNumber.Text = "رقم العداد:";

        // txtMeterNumber
        txtMeterNumber.Font = new Font("Segoe UI", 9.5F);
        txtMeterNumber.Margin = new Padding(0, 4, 6, 0);
        txtMeterNumber.Size = new Size(130, 24);
        txtMeterNumber.TabIndex = 2;

        // lblCustomerId
        lblCustomerId.AutoSize = true;
        lblCustomerId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomerId.Margin = new Padding(2, 8, 4, 0);
        lblCustomerId.Text = "معرف العميل:";

        // txtCustomerId
        txtCustomerId.Font = new Font("Segoe UI", 9.5F);
        txtCustomerId.Margin = new Padding(0, 4, 6, 0);
        txtCustomerId.Size = new Size(90, 24);
        txtCustomerId.TabIndex = 3;

        // btnSearch
        btnSearch.BackColor = Color.FromArgb(0, 122, 204);
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSearch.ForeColor = Color.White;
        btnSearch.Margin = new Padding(8, 3, 4, 3);
        btnSearch.Size = new Size(115, 30);
        btnSearch.TabIndex = 4;
        btnSearch.Text = "بحث";
        btnSearch.UseVisualStyleBackColor = false;

        // btnClear
        btnClear.BackColor = Color.FromArgb(240, 240, 240);
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9F);
        btnClear.ForeColor = Color.FromArgb(60, 60, 60);
        btnClear.Margin = new Padding(4, 3, 4, 3);
        btnClear.Size = new Size(96, 30);
        btnClear.TabIndex = 5;
        btnClear.Text = "مسح";
        btnClear.UseVisualStyleBackColor = true;

        // lblSearchHint
        lblSearchHint.AutoSize = true;
        lblSearchHint.Font = smallGrayStyle;
        lblSearchHint.ForeColor = Color.DimGray;
        lblSearchHint.Margin = new Padding(10, 10, 2, 0);
        lblSearchHint.Text = "بحث بأحد المعايير (معرف العميل > رقم العميل > رقم العداد).";

        pnlSearchFlow.Controls.Add(lblCustomerNumber);
        pnlSearchFlow.Controls.Add(txtCustomerNumber);
        pnlSearchFlow.Controls.Add(lblMeterNumber);
        pnlSearchFlow.Controls.Add(txtMeterNumber);
        pnlSearchFlow.Controls.Add(lblCustomerId);
        pnlSearchFlow.Controls.Add(txtCustomerId);
        pnlSearchFlow.Controls.Add(btnSearch);
        pnlSearchFlow.Controls.Add(btnClear);
        pnlSearchFlow.Controls.Add(lblSearchHint);

        // 
        // tblCards
        // 
        tblCards.ColumnCount = 2;
        tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
        tblCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
        tblCards.Controls.Add(grpCustomer, 0, 0);
        tblCards.Controls.Add(grpMeter, 1, 0);
        tblCards.Dock = DockStyle.Top;
        tblCards.Location = new Point(0, 96);
        tblCards.Name = "tblCards";
        tblCards.Padding = new Padding(6, 4, 6, 2);
        tblCards.RowCount = 1;
        tblCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tblCards.Size = new Size(1120, 120);
        tblCards.TabIndex = 2;

        // 
        // grpCustomer
        // 
        grpCustomer.Controls.Add(tblCustomer);
        grpCustomer.Dock = DockStyle.Fill;
        grpCustomer.Font = titleStyle;
        grpCustomer.Margin = new Padding(3);
        grpCustomer.Padding = new Padding(6, 2, 6, 2);
        grpCustomer.Text = "بيانات العميل";

        // 
        // tblCustomer
        // 
        tblCustomer.ColumnCount = 4;
        tblCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
        tblCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        tblCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
        tblCustomer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        tblCustomer.Dock = DockStyle.Fill;
        tblCustomer.RowCount = 3;
        tblCustomer.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        tblCustomer.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        tblCustomer.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));

        AddCell(tblCustomer, lblCNumT, "رقم العميل:", 0, 0);
        AddCell(tblCustomer, lblCNumV, "—", 1, 0, bold: true);
        AddCell(tblCustomer, lblCPhoneT, "الهاتف:", 2, 0);
        AddCell(tblCustomer, lblCPhoneV, "—", 3, 0, bold: true);
        AddCell(tblCustomer, lblCNameT, "اسم العميل:", 0, 1);
        AddCell(tblCustomer, lblCNameV, "—", 1, 1, bold: true);
        AddCell(tblCustomer, lblCStatusT, "الحالة:", 2, 1);
        AddCell(tblCustomer, lblCStatusV, "—", 3, 1, bold: true);
        AddCell(tblCustomer, lblCAddrT, "العنوان:", 0, 2);
        AddCell(tblCustomer, lblCAddrV, "—", 1, 2, bold: true);
        tblCustomer.SetColumnSpan(lblCAddrV, 2);

        // 
        // grpMeter
        // 
        grpMeter.Controls.Add(tblMeter);
        grpMeter.Dock = DockStyle.Fill;
        grpMeter.Font = titleStyle;
        grpMeter.Margin = new Padding(3);
        grpMeter.Padding = new Padding(6, 2, 6, 2);
        grpMeter.Text = "بيانات العداد";

        // 
        // tblMeter
        // 
        tblMeter.ColumnCount = 2;
        tblMeter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F));
        tblMeter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblMeter.Dock = DockStyle.Fill;
        tblMeter.RowCount = 11;
        for (int i = 0; i < 11; i++)
        {
            tblMeter.RowStyles.Add(new RowStyle(SizeType.Percent, 9.09F));
        }

        lblMeterPick.AutoSize = true;
        lblMeterPick.Dock = DockStyle.Fill;
        lblMeterPick.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMeterPick.Text = "العداد المحدد:";
        lblMeterPick.TextAlign = ContentAlignment.MiddleLeft;
        tblMeter.Controls.Add(lblMeterPick, 0, 0);

        cmbMeters.Dock = DockStyle.Fill;
        cmbMeters.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMeters.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        cmbMeters.TabIndex = 6;
        tblMeter.Controls.Add(cmbMeters, 1, 0);

        AddRow(tblMeter, lblMNumT, "رقم العداد:", lblMNumV, 1);
        AddRow(tblMeter, lblMTypeT, "نوع العداد:", lblMTypeV, 2);
        AddRow(tblMeter, lblMDirT, "اتجاه القراءة:", lblMDirV, 3);
        AddRow(tblMeter, lblMBranchT, "الفرع:", lblMBranchV, 4);
        AddRow(tblMeter, lblMAreaT, "المنطقة:", lblMAreaV, 5);
        AddRow(tblMeter, lblMLastReadT, "آخر قراءة:", lblMLastReadV, 6);
        AddRow(tblMeter, lblMLastReadDateT, "تاريخ آخر قراءة:", lblMLastReadDateV, 7);
        AddRow(tblMeter, lblMLastConsT, "آخر استهلاك:", lblMLastConsV, 8);
        AddRow(tblMeter, lblMCumulT, "الاستهلاك التراكمي:", lblMCumulV, 9);

        lblMeterNotice.AutoSize = false;
        lblMeterNotice.Dock = DockStyle.Fill;
        lblMeterNotice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMeterNotice.ForeColor = Color.Firebrick;
        lblMeterNotice.TextAlign = ContentAlignment.MiddleRight;
        lblMeterNotice.Visible = false;
        tblMeter.Controls.Add(lblMeterNotice, 0, 10);
        tblMeter.SetColumnSpan(lblMeterNotice, 2);

        // 
        // pnlActions
        // 
        pnlActions.BackColor = Color.FromArgb(232, 238, 244);
        pnlActions.Controls.Add(pnlActionsFlow);
        pnlActions.Dock = DockStyle.Top;
        pnlActions.Location = new Point(0, 216);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(1120, 44);
        pnlActions.TabIndex = 3;

        // 
        // pnlActionsFlow
        // 
        pnlActionsFlow.Dock = DockStyle.Fill;
        pnlActionsFlow.FlowDirection = FlowDirection.LeftToRight;
        pnlActionsFlow.Padding = new Padding(8, 4, 8, 0);
        pnlActionsFlow.RightToLeft = RightToLeft.Yes;
        pnlActionsFlow.WrapContents = false;

        // lblPeriod
        lblPeriod.AutoSize = true;
        lblPeriod.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPeriod.Margin = new Padding(2, 10, 2, 0);
        lblPeriod.Text = "فترة الفاتورة:";

        // nudBillingYear
        nudBillingYear.Font = new Font("Segoe UI", 9.5F);
        nudBillingYear.Location = new Point(0, 0);
        nudBillingYear.Margin = new Padding(0, 5, 4, 0);
        nudBillingYear.Maximum = 2040m;
        nudBillingYear.Minimum = 2015m;
        nudBillingYear.Size = new Size(76, 24);
        nudBillingYear.TabIndex = 7;
        nudBillingYear.Value = 2026m;

        // cmbBillingMonth
        cmbBillingMonth.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBillingMonth.Font = new Font("Segoe UI", 9.5F);
        cmbBillingMonth.Margin = new Padding(4, 5, 12, 0);
        cmbBillingMonth.Size = new Size(130, 24);
        cmbBillingMonth.TabIndex = 8;

        // btnCreateReading
        btnCreateReading.BackColor = Color.FromArgb(0, 122, 204);
        btnCreateReading.FlatStyle = FlatStyle.Flat;
        btnCreateReading.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnCreateReading.ForeColor = Color.White;
        btnCreateReading.Margin = new Padding(8, 4, 4, 3);
        btnCreateReading.Size = new Size(140, 32);
        btnCreateReading.TabIndex = 9;
        btnCreateReading.Text = "إدخال قراءة";
        btnCreateReading.UseVisualStyleBackColor = false;

        // btnCreateInvoice
        btnCreateInvoice.BackColor = Color.FromArgb(0, 128, 0);
        btnCreateInvoice.FlatStyle = FlatStyle.Flat;
        btnCreateInvoice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnCreateInvoice.ForeColor = Color.White;
        btnCreateInvoice.Margin = new Padding(4, 4, 4, 3);
        btnCreateInvoice.Size = new Size(130, 32);
        btnCreateInvoice.TabIndex = 10;
        btnCreateInvoice.Text = "إنشاء فاتورة";
        btnCreateInvoice.UseVisualStyleBackColor = false;

        // btnPayInvoice
        btnPayInvoice.BackColor = Color.FromArgb(0, 122, 204);
        btnPayInvoice.FlatStyle = FlatStyle.Flat;
        btnPayInvoice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnPayInvoice.ForeColor = Color.White;
        btnPayInvoice.Margin = new Padding(4, 4, 4, 3);
        btnPayInvoice.Size = new Size(130, 32);
        btnPayInvoice.TabIndex = 11;
        btnPayInvoice.Text = "سداد فاتورة";
        btnPayInvoice.UseVisualStyleBackColor = false;

        // btnReversePayment
        btnReversePayment.BackColor = Color.FromArgb(156, 47, 28);
        btnReversePayment.FlatStyle = FlatStyle.Flat;
        btnReversePayment.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnReversePayment.ForeColor = Color.White;
        btnReversePayment.Margin = new Padding(4, 4, 4, 3);
        btnReversePayment.Size = new Size(130, 32);
        btnReversePayment.TabIndex = 12;
        btnReversePayment.Text = "عكس دفعة";
        btnReversePayment.UseVisualStyleBackColor = false;

        pnlActionsFlow.Controls.Add(lblPeriod);
        pnlActionsFlow.Controls.Add(nudBillingYear);
        pnlActionsFlow.Controls.Add(cmbBillingMonth);
        pnlActionsFlow.Controls.Add(btnCreateReading);
        pnlActionsFlow.Controls.Add(btnCreateInvoice);
        pnlActionsFlow.Controls.Add(btnPayInvoice);
        pnlActionsFlow.Controls.Add(btnReversePayment);

        // 
        // tblGrids
        // 
        tblGrids.ColumnCount = 1;
        tblGrids.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblGrids.Controls.Add(pnlInvArea, 0, 0);
        tblGrids.Controls.Add(pnlPayArea, 0, 1);
        tblGrids.Dock = DockStyle.Fill;
        tblGrids.Location = new Point(0, 260);
        tblGrids.Name = "tblGrids";
        tblGrids.RowCount = 2;
        tblGrids.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblGrids.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblGrids.Size = new Size(1120, 376);
        tblGrids.TabIndex = 4;

        // 
        // pnlInvArea
        // 
        pnlInvArea.Controls.Add(dgvOpenInvoices);
        pnlInvArea.Controls.Add(lblNoInvoices);
        pnlInvArea.Controls.Add(pnlInvSummary);
        pnlInvArea.Dock = DockStyle.Fill;
        pnlInvArea.Margin = new Padding(6, 2, 6, 3);

        // 
        // lblNoInvoices
        // 
        lblNoInvoices.Dock = DockStyle.Top;
        lblNoInvoices.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNoInvoices.ForeColor = Color.DimGray;
        lblNoInvoices.Height = 26;
        lblNoInvoices.Text = "لا توجد فواتير مفتوحة للعميل.";
        lblNoInvoices.TextAlign = ContentAlignment.MiddleCenter;
        lblNoInvoices.Visible = false;

        // 
        // dgvOpenInvoices
        // 
        dgvCellStyleHeader.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvCellStyleHeader.BackColor = Color.FromArgb(235, 240, 245);
        dgvCellStyleHeader.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        dgvCellStyleHeader.ForeColor = Color.Black;
        dgvCellStyleHeader.WrapMode = DataGridViewTriState.False;
        dgvOpenInvoices.ColumnHeadersDefaultCellStyle = dgvCellStyleHeader;
        dgvOpenInvoices.ColumnHeadersHeight = 30;
        dgvOpenInvoices.Dock = DockStyle.Fill;
        dgvOpenInvoices.Name = "dgvOpenInvoices";
        dgvOpenInvoices.RowHeadersVisible = false;
        dgvOpenInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOpenInvoices.MultiSelect = false;
        dgvOpenInvoices.ReadOnly = true;
        dgvOpenInvoices.AllowUserToAddRows = false;
        dgvOpenInvoices.AllowUserToDeleteRows = false;
        dgvOpenInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOpenInvoices.StandardTab = true;
        dgvOpenInvoices.TabIndex = 0;

        // 
        // pnlInvSummary
        // 
        pnlInvSummary.BackColor = Color.FromArgb(245, 247, 250);
        pnlInvSummary.BorderStyle = BorderStyle.FixedSingle;
        pnlInvSummary.Controls.Add(pnlInvSummaryFlow);
        pnlInvSummary.Dock = DockStyle.Bottom;
        pnlInvSummary.Height = 44;
        pnlInvSummary.Name = "pnlInvSummary";

        // 
        // pnlInvSummaryFlow
        // 
        pnlInvSummaryFlow.Dock = DockStyle.Fill;
        pnlInvSummaryFlow.FlowDirection = FlowDirection.LeftToRight;
        pnlInvSummaryFlow.Padding = new Padding(6, 8, 6, 0);
        pnlInvSummaryFlow.RightToLeft = RightToLeft.Yes;
        pnlInvSummaryFlow.WrapContents = false;

        AddSummaryPair(pnlInvSummaryFlow, lblInvTotalT, "الإجمالي:", lblInvTotalV, UiTheme.TextPrimary, 9F);
        AddSummaryPair(pnlInvSummaryFlow, lblInvPaidT, "المدفوع:", lblInvPaidV, UiTheme.Success, 9F);
        AddSummaryPair(pnlInvSummaryFlow, lblInvBalT, "المتبقي:", lblInvBalV, UiTheme.Danger, 10F);
        AddSummaryPair(pnlInvSummaryFlow, lblInvStatusT, "الحالة:", lblInvStatusV, UiTheme.Accent, 9.5F);
        lblInvCurrencyNote.AutoSize = true;
        lblInvCurrencyNote.Font = smallGrayStyle;
        lblInvCurrencyNote.ForeColor = Color.Gray;
        lblInvCurrencyNote.Margin = new Padding(8, 6, 2, 0);
        lblInvCurrencyNote.Text = "(المبالغ بالريال السعودي)";
        pnlInvSummaryFlow.Controls.Add(lblInvCurrencyNote);

        // 
        // pnlPayArea
        // 
        pnlPayArea.Controls.Add(dgvPayments);
        pnlPayArea.Controls.Add(lblNoPayments);
        pnlPayArea.Controls.Add(pnlPaySummary);
        pnlPayArea.Dock = DockStyle.Fill;
        pnlPayArea.Margin = new Padding(6, 2, 6, 3);

        // 
        // lblNoPayments
        // 
        lblNoPayments.Dock = DockStyle.Top;
        lblNoPayments.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNoPayments.ForeColor = Color.DimGray;
        lblNoPayments.Height = 26;
        lblNoPayments.Text = "لا توجد دفعات مسجلة للعميل.";
        lblNoPayments.TextAlign = ContentAlignment.MiddleCenter;
        lblNoPayments.Visible = false;

        // 
        // dgvPayments
        // 
        dgvPayments.ColumnHeadersDefaultCellStyle = dgvCellStyleHeader;
        dgvPayments.ColumnHeadersHeight = 30;
        dgvPayments.Dock = DockStyle.Fill;
        dgvPayments.Name = "dgvPayments";
        dgvPayments.RowHeadersVisible = false;
        dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPayments.MultiSelect = false;
        dgvPayments.ReadOnly = true;
        dgvPayments.AllowUserToAddRows = false;
        dgvPayments.AllowUserToDeleteRows = false;
        dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPayments.StandardTab = true;
        dgvPayments.TabIndex = 0;

        // 
        // pnlPaySummary
        // 
        pnlPaySummary.BackColor = Color.FromArgb(245, 247, 250);
        pnlPaySummary.BorderStyle = BorderStyle.FixedSingle;
        pnlPaySummary.Controls.Add(pnlPaySummaryFlow);
        pnlPaySummary.Dock = DockStyle.Bottom;
        pnlPaySummary.Height = 44;
        pnlPaySummary.Name = "pnlPaySummary";

        // 
        // pnlPaySummaryFlow
        // 
        pnlPaySummaryFlow.Dock = DockStyle.Fill;
        pnlPaySummaryFlow.FlowDirection = FlowDirection.LeftToRight;
        pnlPaySummaryFlow.Padding = new Padding(6, 8, 6, 0);
        pnlPaySummaryFlow.RightToLeft = RightToLeft.Yes;
        pnlPaySummaryFlow.WrapContents = false;

        AddSummaryPair(pnlPaySummaryFlow, lblPayAmtT, "المبلغ:", lblPayAmtV, UiTheme.Success, 9F);
        AddSummaryPair(pnlPaySummaryFlow, lblPayMethodT, "طريقة الدفع:", lblPayMethodV, UiTheme.TextPrimary, 9F);
        AddSummaryPair(pnlPaySummaryFlow, lblPayReceiptT, "رقم الإيصال:", lblPayReceiptV, UiTheme.TextPrimary, 9F);
        AddSummaryPair(pnlPaySummaryFlow, lblPayRevT, "معكوسة؟:", lblPayRevV, UiTheme.Danger, 9F);
        AddSummaryPair(pnlPaySummaryFlow, lblPayReasonT, "سبب العكس:", lblPayReasonV, UiTheme.Danger, 9F);
        lblPayCurrencyNote.AutoSize = true;
        lblPayCurrencyNote.Font = smallGrayStyle;
        lblPayCurrencyNote.ForeColor = Color.Gray;
        lblPayCurrencyNote.Margin = new Padding(8, 6, 2, 0);
        lblPayCurrencyNote.Text = "(المبالغ بالريال السعودي)";
        pnlPaySummaryFlow.Controls.Add(lblPayCurrencyNote);

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { tslblStatus, tslblCounts, tspbProgress });
        statusStrip.Location = new Point(0, 636);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1120, 24);
        statusStrip.TabIndex = 5;

        // tslblStatus
        tslblStatus.Name = "tslblStatus";
        tslblStatus.Size = new Size(40, 19);
        tslblStatus.Text = "جاهز";

        // tslblCounts
        tslblCounts.Name = "tslblCounts";
        tslblCounts.Size = new Size(965, 19);
        tslblCounts.Spring = true;
        tslblCounts.TextAlign = ContentAlignment.MiddleLeft;

        // tspbProgress
        tspbProgress.Name = "tspbProgress";
        tspbProgress.Size = new Size(100, 18);
        tspbProgress.Style = ProgressBarStyle.Marquee;
        tspbProgress.Visible = false;

        // 
        // FieldCollectionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 16F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        ClientSize = new Size(1120, 660);
        Controls.Add(tblGrids);
        Controls.Add(pnlActions);
        Controls.Add(tblCards);
        Controls.Add(pnlSearch);
        Controls.Add(pnlHeader);
        Controls.Add(statusStrip);
        Font = new Font("Segoe UI", 9.5F);
        KeyPreview = true;
        MinimumSize = new Size(960, 600);
        Name = "FieldCollectionForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "التحصيل الميداني - WaterStation";

        pnlHeader.ResumeLayout(false);
        pnlSearch.ResumeLayout(false);
        pnlSearchFlow.ResumeLayout(false);
        pnlSearchFlow.PerformLayout();
        tblCards.ResumeLayout(false);
        grpCustomer.ResumeLayout(false);
        tblCustomer.ResumeLayout(false);
        tblCustomer.PerformLayout();
        grpMeter.ResumeLayout(false);
        tblMeter.ResumeLayout(false);
        tblMeter.PerformLayout();
        pnlActions.ResumeLayout(false);
        pnlActionsFlow.ResumeLayout(false);
        pnlActionsFlow.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudBillingYear).EndInit();
        tblGrids.ResumeLayout(false);
        pnlInvArea.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvOpenInvoices).EndInit();
        pnlInvSummary.ResumeLayout(false);
        pnlInvSummaryFlow.ResumeLayout(false);
        pnlInvSummaryFlow.PerformLayout();
        pnlPayArea.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
        pnlPaySummary.ResumeLayout(false);
        pnlPaySummaryFlow.ResumeLayout(false);
        pnlPaySummaryFlow.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private static void AddCell(TableLayoutPanel table, Label label, string text, int column, int row, bool bold = false)
    {
        label.AutoSize = true;
        label.Dock = DockStyle.Fill;
        label.Font = bold
            ? new Font("Segoe UI", 9F, FontStyle.Bold)
            : new Font("Segoe UI", 8.5F);
        label.ForeColor = bold ? UiTheme.TextPrimary : UiTheme.TextSecondary;
        label.Text = text;
        label.TextAlign = ContentAlignment.MiddleLeft;
        table.Controls.Add(label, column, row);
    }

    private static void AddRow(TableLayoutPanel table, Label title, string titleText, Label value, int row)
    {
        title.AutoSize = true;
        title.Dock = DockStyle.Fill;
        title.Font = new Font("Segoe UI", 8.5F);
        title.ForeColor = UiTheme.TextSecondary;
        title.Text = titleText;
        title.TextAlign = ContentAlignment.MiddleLeft;
        table.Controls.Add(title, 0, row);

        value.AutoSize = true;
        value.Dock = DockStyle.Fill;
        value.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        value.ForeColor = UiTheme.TextPrimary;
        value.Text = "—";
        value.TextAlign = ContentAlignment.MiddleLeft;
        table.Controls.Add(value, 1, row);
    }

    private static void AddSummaryPair(FlowLayoutPanel flow, Label title, string titleText, Label value, Color valueColor, float fontSize)
    {
        title.AutoSize = true;
        title.Font = new Font("Segoe UI", 9F);
        title.ForeColor = UiTheme.TextSecondary;
        title.Margin = new Padding(6, 5, 2, 0);
        title.Text = titleText;

        value.AutoSize = true;
        value.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
        value.ForeColor = valueColor;
        value.Margin = new Padding(2, 4, 10, 0);
        value.Text = "—";

        flow.Controls.Add(title);
        flow.Controls.Add(value);
    }
}