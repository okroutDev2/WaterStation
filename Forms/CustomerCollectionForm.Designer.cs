#nullable enable

namespace WaterStation.Forms;

partial class CustomerCollectionForm
{
    private System.ComponentModel.IContainer? components = null;

    // Header & Search
    private Panel pnlTop = null!;
    private GroupBox grpSearch = null!;
    private Label lblCustomerNumber = null!;
    private TextBox txtCustomerNumber = null!;
    private Label lblMeterNumber = null!;
    private TextBox txtMeterNumber = null!;
    private Label lblCustomerId = null!;
    private TextBox txtCustomerId = null!;
    private Button btnSearch = null!;
    private Button btnClear = null!;
    private Label lblSearchHint = null!;

    // Customer Info Panel
    private GroupBox grpCustomerInfo = null!;
    private Label lblCustNumTitle = null!;
    private Label lblCustNumValue = null!;
    private Label lblCustNameTitle = null!;
    private Label lblCustNameValue = null!;
    private Label lblCustPhoneTitle = null!;
    private Label lblCustPhoneValue = null!;
    private Label lblCustAddressTitle = null!;
    private Label lblCustAddressValue = null!;
    private Label lblCustStatusTitle = null!;
    private Label lblCustStatusValue = null!;

    // Tab Control & DataGrids
    private TabControl tabDetails = null!;
    private TabPage tabOpenInvoices = null!;
    private TabPage tabMeters = null!;
    private TabPage tabPayments = null!;

    // Open Invoices Tab Controls
    private DataGridView dgvOpenInvoices = null!;
    private Panel pnlInvoiceSummary = null!;
    private Label lblSummaryTotalTitle = null!;
    private Label lblSummaryTotalVal = null!;
    private Label lblSummaryPaidTitle = null!;
    private Label lblSummaryPaidVal = null!;
    private Label lblSummaryBalanceTitle = null!;
    private Label lblSummaryBalanceVal = null!;
    private Label lblSummaryStatusTitle = null!;
    private Label lblSummaryStatusVal = null!;
    private Button btnPayInvoice = null!;


    // Meters Tab Controls
    private DataGridView dgvMeters = null!;
    private Panel pnlMetersActions = null!;
    private Button btnEnterReading = null!;

    // Payments Tab Controls
    private DataGridView dgvPayments = null!;
    private Panel pnlPaymentsActions = null!;
    private Button btnReversePayment = null!;

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

        pnlTop = new Panel();
        grpSearch = new GroupBox();
        lblCustomerNumber = new Label();
        txtCustomerNumber = new TextBox();
        lblMeterNumber = new Label();
        txtMeterNumber = new TextBox();
        lblCustomerId = new Label();
        txtCustomerId = new TextBox();
        btnSearch = new Button();
        btnClear = new Button();
        lblSearchHint = new Label();

        grpCustomerInfo = new GroupBox();
        lblCustNumTitle = new Label();
        lblCustNumValue = new Label();
        lblCustNameTitle = new Label();
        lblCustNameValue = new Label();
        lblCustPhoneTitle = new Label();
        lblCustPhoneValue = new Label();
        lblCustAddressTitle = new Label();
        lblCustAddressValue = new Label();
        lblCustStatusTitle = new Label();
        lblCustStatusValue = new Label();

        tabDetails = new TabControl();
        tabOpenInvoices = new TabPage();
        dgvOpenInvoices = new DataGridView();
        pnlInvoiceSummary = new Panel();
        lblSummaryTotalTitle = new Label();
        lblSummaryTotalVal = new Label();
        lblSummaryPaidTitle = new Label();
        lblSummaryPaidVal = new Label();
        lblSummaryBalanceTitle = new Label();
        lblSummaryBalanceVal = new Label();
        lblSummaryStatusTitle = new Label();
        lblSummaryStatusVal = new Label();
        btnPayInvoice = new Button();


        tabMeters = new TabPage();
        dgvMeters = new DataGridView();
        pnlMetersActions = new Panel();
        btnEnterReading = new Button();

        tabPayments = new TabPage();
        dgvPayments = new DataGridView();
        pnlPaymentsActions = new Panel();
        btnReversePayment = new Button();

        statusStrip = new StatusStrip();
        tslblStatus = new ToolStripStatusLabel();
        tslblCounts = new ToolStripStatusLabel();
        tspbProgress = new ToolStripProgressBar();

        pnlTop.SuspendLayout();
        grpSearch.SuspendLayout();
        grpCustomerInfo.SuspendLayout();
        tabDetails.SuspendLayout();
        tabOpenInvoices.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOpenInvoices).BeginInit();
        pnlInvoiceSummary.SuspendLayout();
        tabMeters.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvMeters).BeginInit();
        pnlMetersActions.SuspendLayout();
        tabPayments.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
        pnlPaymentsActions.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // 
        // pnlTop
        // 
        pnlTop.Controls.Add(grpCustomerInfo);
        pnlTop.Controls.Add(grpSearch);
        pnlTop.Dock = DockStyle.Top;
        pnlTop.Location = new Point(0, 0);
        pnlTop.Name = "pnlTop";
        pnlTop.Padding = new Padding(8);
        pnlTop.Size = new Size(1184, 185);
        pnlTop.TabIndex = 0;

        // 
        // grpSearch
        // 
        grpSearch.Controls.Add(lblSearchHint);
        grpSearch.Controls.Add(btnClear);
        grpSearch.Controls.Add(btnSearch);
        grpSearch.Controls.Add(txtCustomerId);
        grpSearch.Controls.Add(lblCustomerId);
        grpSearch.Controls.Add(txtMeterNumber);
        grpSearch.Controls.Add(lblMeterNumber);
        grpSearch.Controls.Add(txtCustomerNumber);
        grpSearch.Controls.Add(lblCustomerNumber);
        grpSearch.Dock = DockStyle.Top;
        grpSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpSearch.Location = new Point(8, 8);
        grpSearch.Name = "grpSearch";
        grpSearch.Size = new Size(1168, 85);
        grpSearch.TabIndex = 0;
        grpSearch.TabStop = false;
        grpSearch.Text = "معايير البحث";

        // lblCustomerNumber
        lblCustomerNumber.AutoSize = true;
        lblCustomerNumber.Font = new Font("Segoe UI", 9F);
        lblCustomerNumber.Location = new Point(1080, 26);
        lblCustomerNumber.Name = "lblCustomerNumber";
        lblCustomerNumber.Size = new Size(72, 15);
        lblCustomerNumber.TabIndex = 0;
        lblCustomerNumber.Text = "رقم العميل:";

        // txtCustomerNumber
        txtCustomerNumber.Font = new Font("Segoe UI", 9.5F);
        txtCustomerNumber.Location = new Point(935, 22);
        txtCustomerNumber.Name = "txtCustomerNumber";
        txtCustomerNumber.Size = new Size(140, 24);
        txtCustomerNumber.TabIndex = 1;

        // lblMeterNumber
        lblMeterNumber.AutoSize = true;
        lblMeterNumber.Font = new Font("Segoe UI", 9F);
        lblMeterNumber.Location = new Point(845, 26);
        lblMeterNumber.Name = "lblMeterNumber";
        lblMeterNumber.Size = new Size(68, 15);
        lblMeterNumber.TabIndex = 2;
        lblMeterNumber.Text = "رقم العداد:";

        // txtMeterNumber
        txtMeterNumber.Font = new Font("Segoe UI", 9.5F);
        txtMeterNumber.Location = new Point(700, 22);
        txtMeterNumber.Name = "txtMeterNumber";
        txtMeterNumber.Size = new Size(140, 24);
        txtMeterNumber.TabIndex = 3;

        // lblCustomerId
        lblCustomerId.AutoSize = true;
        lblCustomerId.Font = new Font("Segoe UI", 9F);
        lblCustomerId.Location = new Point(600, 26);
        lblCustomerId.Name = "lblCustomerId";
        lblCustomerId.Size = new Size(77, 15);
        lblCustomerId.TabIndex = 4;
        lblCustomerId.Text = "معرف العميل:";

        // txtCustomerId
        txtCustomerId.Font = new Font("Segoe UI", 9.5F);
        txtCustomerId.Location = new Point(485, 22);
        txtCustomerId.Name = "txtCustomerId";
        txtCustomerId.Size = new Size(110, 24);
        txtCustomerId.TabIndex = 5;

        // btnSearch
        btnSearch.BackColor = Color.FromArgb(0, 122, 204);
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSearch.ForeColor = Color.White;
        btnSearch.Location = new Point(350, 19);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(115, 30);
        btnSearch.TabIndex = 6;
        btnSearch.Text = "بحث";
        btnSearch.UseVisualStyleBackColor = false;

        // btnClear
        btnClear.BackColor = Color.FromArgb(240, 240, 240);
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9F);
        btnClear.Location = new Point(235, 19);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(100, 30);
        btnClear.TabIndex = 7;
        btnClear.Text = "مسح";
        btnClear.UseVisualStyleBackColor = true;

        // lblSearchHint
        lblSearchHint.AutoSize = true;
        lblSearchHint.Font = new Font("Segoe UI", 8.5F);
        lblSearchHint.ForeColor = Color.DimGray;
        lblSearchHint.Location = new Point(235, 55);
        lblSearchHint.Name = "lblSearchHint";
        lblSearchHint.Size = new Size(917, 15);
        lblSearchHint.TabIndex = 8;
        lblSearchHint.Text = "يمكنك البحث بأحد المعايير التالية حسب الأولوية: معرف العميل، أو رقم العميل، أو رقم العداد.";

        // 
        // grpCustomerInfo
        // 
        grpCustomerInfo.Controls.Add(lblCustStatusValue);
        grpCustomerInfo.Controls.Add(lblCustStatusTitle);
        grpCustomerInfo.Controls.Add(lblCustAddressValue);
        grpCustomerInfo.Controls.Add(lblCustAddressTitle);
        grpCustomerInfo.Controls.Add(lblCustPhoneValue);
        grpCustomerInfo.Controls.Add(lblCustPhoneTitle);
        grpCustomerInfo.Controls.Add(lblCustNameValue);
        grpCustomerInfo.Controls.Add(lblCustNameTitle);
        grpCustomerInfo.Controls.Add(lblCustNumValue);
        grpCustomerInfo.Controls.Add(lblCustNumTitle);
        grpCustomerInfo.Dock = DockStyle.Bottom;
        grpCustomerInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grpCustomerInfo.Location = new Point(8, 97);
        grpCustomerInfo.Name = "grpCustomerInfo";
        grpCustomerInfo.Size = new Size(1168, 80);
        grpCustomerInfo.TabIndex = 1;
        grpCustomerInfo.TabStop = false;
        grpCustomerInfo.Text = "بيانات العميل المحدد";

        // CustNum
        lblCustNumTitle.AutoSize = true;
        lblCustNumTitle.Font = new Font("Segoe UI", 9F);
        lblCustNumTitle.Location = new Point(1085, 28);
        lblCustNumTitle.Name = "lblCustNumTitle";
        lblCustNumTitle.Size = new Size(67, 15);
        lblCustNumTitle.TabIndex = 0;
        lblCustNumTitle.Text = "رقم العميل:";
        lblCustNumValue.AutoSize = true;
        lblCustNumValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustNumValue.ForeColor = Color.MidnightBlue;
        lblCustNumValue.Location = new Point(965, 28);
        lblCustNumValue.Name = "lblCustNumValue";
        lblCustNumValue.Size = new Size(20, 17);
        lblCustNumValue.TabIndex = 1;
        lblCustNumValue.Text = "—";

        // CustName
        lblCustNameTitle.AutoSize = true;
        lblCustNameTitle.Font = new Font("Segoe UI", 9F);
        lblCustNameTitle.Location = new Point(870, 28);
        lblCustNameTitle.Name = "lblCustNameTitle";
        lblCustNameTitle.Size = new Size(71, 15);
        lblCustNameTitle.TabIndex = 2;
        lblCustNameTitle.Text = "اسم العميل:";
        lblCustNameValue.AutoSize = true;
        lblCustNameValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustNameValue.ForeColor = Color.DarkSlateBlue;
        lblCustNameValue.Location = new Point(620, 28);
        lblCustNameValue.Name = "lblCustNameValue";
        lblCustNameValue.Size = new Size(20, 17);
        lblCustNameValue.TabIndex = 3;
        lblCustNameValue.Text = "—";

        // CustPhone
        lblCustPhoneTitle.AutoSize = true;
        lblCustPhoneTitle.Font = new Font("Segoe UI", 9F);
        lblCustPhoneTitle.Location = new Point(530, 28);
        lblCustPhoneTitle.Name = "lblCustPhoneTitle";
        lblCustPhoneTitle.Size = new Size(42, 15);
        lblCustPhoneTitle.TabIndex = 4;
        lblCustPhoneTitle.Text = "الهاتف:";
        lblCustPhoneValue.AutoSize = true;
        lblCustPhoneValue.Font = new Font("Segoe UI", 9.5F);
        lblCustPhoneValue.Location = new Point(410, 28);
        lblCustPhoneValue.Name = "lblCustPhoneValue";
        lblCustPhoneValue.Size = new Size(20, 17);
        lblCustPhoneValue.TabIndex = 5;
        lblCustPhoneValue.Text = "—";

        // CustAddress
        lblCustAddressTitle.AutoSize = true;
        lblCustAddressTitle.Font = new Font("Segoe UI", 9F);
        lblCustAddressTitle.Location = new Point(340, 28);
        lblCustAddressTitle.Name = "lblCustAddressTitle";
        lblCustAddressTitle.Size = new Size(45, 15);
        lblCustAddressTitle.TabIndex = 6;
        lblCustAddressTitle.Text = "العنوان:";
        lblCustAddressValue.AutoSize = true;
        lblCustAddressValue.Font = new Font("Segoe UI", 9.5F);
        lblCustAddressValue.Location = new Point(140, 28);
        lblCustAddressValue.Name = "lblCustAddressValue";
        lblCustAddressValue.Size = new Size(20, 17);
        lblCustAddressValue.TabIndex = 7;
        lblCustAddressValue.Text = "—";

        // CustStatus
        lblCustStatusTitle.AutoSize = true;
        lblCustStatusTitle.Font = new Font("Segoe UI", 9F);
        lblCustStatusTitle.Location = new Point(70, 28);
        lblCustStatusTitle.Name = "lblCustStatusTitle";
        lblCustStatusTitle.Size = new Size(41, 15);
        lblCustStatusTitle.TabIndex = 8;
        lblCustStatusTitle.Text = "الحالة:";
        lblCustStatusValue.AutoSize = true;
        lblCustStatusValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustStatusValue.ForeColor = Color.DarkGreen;
        lblCustStatusValue.Location = new Point(20, 28);
        lblCustStatusValue.Name = "lblCustStatusValue";
        lblCustStatusValue.Size = new Size(20, 17);
        lblCustStatusValue.TabIndex = 9;
        lblCustStatusValue.Text = "—";

        // 
        // tabDetails
        // 
        tabDetails.Controls.Add(tabOpenInvoices);
        tabDetails.Controls.Add(tabMeters);
        tabDetails.Controls.Add(tabPayments);
        tabDetails.Dock = DockStyle.Fill;
        tabDetails.Font = new Font("Segoe UI", 9.5F);
        tabDetails.Location = new Point(0, 185);
        tabDetails.Name = "tabDetails";
        tabDetails.SelectedIndex = 0;
        tabDetails.Size = new Size(1184, 467);
        tabDetails.TabIndex = 1;

        // 
        // tabOpenInvoices
        // 
        tabOpenInvoices.Controls.Add(dgvOpenInvoices);
        tabOpenInvoices.Controls.Add(pnlInvoiceSummary);
        tabOpenInvoices.Location = new Point(4, 25);
        tabOpenInvoices.Name = "tabOpenInvoices";
        tabOpenInvoices.Padding = new Padding(3);
        tabOpenInvoices.Size = new Size(1176, 438);
        tabOpenInvoices.TabIndex = 0;
        tabOpenInvoices.Text = "الفواتير المفتوحة (0)";
        tabOpenInvoices.UseVisualStyleBackColor = true;

        // 
        // dgvOpenInvoices
        // 
        dgvCellStyleHeader.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dgvCellStyleHeader.BackColor = Color.FromArgb(235, 240, 245);
        dgvCellStyleHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvCellStyleHeader.ForeColor = Color.Black;
        dgvCellStyleHeader.WrapMode = DataGridViewTriState.False;
        dgvOpenInvoices.ColumnHeadersDefaultCellStyle = dgvCellStyleHeader;
        dgvOpenInvoices.ColumnHeadersHeight = 32;
        dgvOpenInvoices.Dock = DockStyle.Fill;
        dgvOpenInvoices.Location = new Point(3, 3);
        dgvOpenInvoices.Name = "dgvOpenInvoices";
        dgvOpenInvoices.RowHeadersVisible = false;
        dgvOpenInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOpenInvoices.MultiSelect = false;
        dgvOpenInvoices.ReadOnly = true;
        dgvOpenInvoices.AllowUserToAddRows = false;
        dgvOpenInvoices.AllowUserToDeleteRows = false;
        dgvOpenInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOpenInvoices.Size = new Size(1170, 362);
        dgvOpenInvoices.TabIndex = 0;

        // 
        // pnlInvoiceSummary
        // 
        pnlInvoiceSummary.BackColor = Color.FromArgb(245, 247, 250);
        pnlInvoiceSummary.BorderStyle = BorderStyle.FixedSingle;
        pnlInvoiceSummary.Controls.Add(btnPayInvoice);
        pnlInvoiceSummary.Controls.Add(lblSummaryStatusVal);
        pnlInvoiceSummary.Controls.Add(lblSummaryStatusTitle);
        pnlInvoiceSummary.Controls.Add(lblSummaryBalanceVal);
        pnlInvoiceSummary.Controls.Add(lblSummaryBalanceTitle);
        pnlInvoiceSummary.Controls.Add(lblSummaryPaidVal);
        pnlInvoiceSummary.Controls.Add(lblSummaryPaidTitle);
        pnlInvoiceSummary.Controls.Add(lblSummaryTotalVal);
        pnlInvoiceSummary.Controls.Add(lblSummaryTotalTitle);
        pnlInvoiceSummary.Dock = DockStyle.Bottom;
        pnlInvoiceSummary.Location = new Point(3, 365);
        pnlInvoiceSummary.Name = "pnlInvoiceSummary";
        pnlInvoiceSummary.Size = new Size(1170, 70);
        pnlInvoiceSummary.TabIndex = 1;

        // btnPayInvoice
        btnPayInvoice.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        btnPayInvoice.BackColor = Color.FromArgb(0, 122, 204);
        btnPayInvoice.FlatStyle = FlatStyle.Flat;
        btnPayInvoice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnPayInvoice.ForeColor = Color.White;
        btnPayInvoice.Location = new Point(15, 16);
        btnPayInvoice.Name = "btnPayInvoice";
        btnPayInvoice.Size = new Size(140, 36);
        btnPayInvoice.TabIndex = 8;
        btnPayInvoice.Text = "سداد الفاتورة";
        btnPayInvoice.UseVisualStyleBackColor = false;


        // Summary Total
        lblSummaryTotalTitle.AutoSize = true;
        lblSummaryTotalTitle.Font = new Font("Segoe UI", 9F);
        lblSummaryTotalTitle.Location = new Point(1060, 24);
        lblSummaryTotalTitle.Name = "lblSummaryTotalTitle";
        lblSummaryTotalTitle.Size = new Size(84, 15);
        lblSummaryTotalTitle.TabIndex = 0;
        lblSummaryTotalTitle.Text = "إجمالي الفاتورة:";
        lblSummaryTotalVal.AutoSize = true;
        lblSummaryTotalVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblSummaryTotalVal.ForeColor = Color.Black;
        lblSummaryTotalVal.Location = new Point(950, 21);
        lblSummaryTotalVal.Name = "lblSummaryTotalVal";
        lblSummaryTotalVal.Size = new Size(41, 21);
        lblSummaryTotalVal.TabIndex = 1;
        lblSummaryTotalVal.Text = "0.00";

        // Summary Paid
        lblSummaryPaidTitle.AutoSize = true;
        lblSummaryPaidTitle.Font = new Font("Segoe UI", 9F);
        lblSummaryPaidTitle.Location = new Point(810, 24);
        lblSummaryPaidTitle.Name = "lblSummaryPaidTitle";
        lblSummaryPaidTitle.Size = new Size(84, 15);
        lblSummaryPaidTitle.TabIndex = 2;
        lblSummaryPaidTitle.Text = "المبلغ المدفوع:";
        lblSummaryPaidVal.AutoSize = true;
        lblSummaryPaidVal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblSummaryPaidVal.ForeColor = Color.Green;
        lblSummaryPaidVal.Location = new Point(700, 21);
        lblSummaryPaidVal.Name = "lblSummaryPaidVal";
        lblSummaryPaidVal.Size = new Size(41, 21);
        lblSummaryPaidVal.TabIndex = 3;
        lblSummaryPaidVal.Text = "0.00";

        // Summary Balance
        lblSummaryBalanceTitle.AutoSize = true;
        lblSummaryBalanceTitle.Font = new Font("Segoe UI", 9F);
        lblSummaryBalanceTitle.Location = new Point(540, 24);
        lblSummaryBalanceTitle.Name = "lblSummaryBalanceTitle";
        lblSummaryBalanceTitle.Size = new Size(87, 15);
        lblSummaryBalanceTitle.TabIndex = 4;
        lblSummaryBalanceTitle.Text = "المبلغ المتبقي:";
        lblSummaryBalanceVal.AutoSize = true;
        lblSummaryBalanceVal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblSummaryBalanceVal.ForeColor = Color.DarkRed;
        lblSummaryBalanceVal.Location = new Point(410, 20);
        lblSummaryBalanceVal.Name = "lblSummaryBalanceVal";
        lblSummaryBalanceVal.Size = new Size(47, 25);
        lblSummaryBalanceVal.TabIndex = 5;
        lblSummaryBalanceVal.Text = "0.00";

        // Summary Status
        lblSummaryStatusTitle.AutoSize = true;
        lblSummaryStatusTitle.Font = new Font("Segoe UI", 9F);
        lblSummaryStatusTitle.Location = new Point(270, 24);
        lblSummaryStatusTitle.Name = "lblSummaryStatusTitle";
        lblSummaryStatusTitle.Size = new Size(72, 15);
        lblSummaryStatusTitle.TabIndex = 6;
        lblSummaryStatusTitle.Text = "حالة الفاتورة:";
        lblSummaryStatusVal.AutoSize = true;
        lblSummaryStatusVal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblSummaryStatusVal.ForeColor = Color.Navy;
        lblSummaryStatusVal.Location = new Point(140, 21);
        lblSummaryStatusVal.Name = "lblSummaryStatusVal";
        lblSummaryStatusVal.Size = new Size(24, 20);
        lblSummaryStatusVal.TabIndex = 7;
        lblSummaryStatusVal.Text = "—";

//
        // tabMeters
        // 
        tabMeters.Controls.Add(dgvMeters);
        tabMeters.Controls.Add(pnlMetersActions);
        tabMeters.Location = new Point(4, 25);
        tabMeters.Name = "tabMeters";
        tabMeters.Padding = new Padding(3);
        tabMeters.Size = new Size(1176, 438);
        tabMeters.TabIndex = 1;
        tabMeters.Text = "عدادات العميل (0)";
        tabMeters.UseVisualStyleBackColor = true;

        // 
        // dgvMeters
        // 
        dgvMeters.ColumnHeadersDefaultCellStyle = dgvCellStyleHeader;
        dgvMeters.ColumnHeadersHeight = 32;
        dgvMeters.Dock = DockStyle.Fill;
        dgvMeters.Location = new Point(3, 3);
        dgvMeters.Name = "dgvMeters";
        dgvMeters.RowHeadersVisible = false;
        dgvMeters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMeters.MultiSelect = false;
        dgvMeters.ReadOnly = true;
        dgvMeters.AllowUserToAddRows = false;
        dgvMeters.AllowUserToDeleteRows = false;
        dgvMeters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMeters.Size = new Size(1170, 385);
        dgvMeters.TabIndex = 0;

        // 
        // pnlMetersActions
        // 
        pnlMetersActions.BackColor = Color.FromArgb(245, 247, 250);
        pnlMetersActions.BorderStyle = BorderStyle.FixedSingle;
        pnlMetersActions.Controls.Add(btnEnterReading);
        pnlMetersActions.Dock = DockStyle.Bottom;
        pnlMetersActions.Location = new Point(3, 388);
        pnlMetersActions.Name = "pnlMetersActions";
        pnlMetersActions.Size = new Size(1170, 47);
        pnlMetersActions.TabIndex = 1;

        // btnEnterReading
        btnEnterReading.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEnterReading.BackColor = Color.FromArgb(0, 122, 204);
        btnEnterReading.FlatStyle = FlatStyle.Flat;
        btnEnterReading.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnEnterReading.ForeColor = Color.White;
        btnEnterReading.Location = new Point(12, 6);
        btnEnterReading.Name = "btnEnterReading";
        btnEnterReading.Size = new Size(160, 34);
        btnEnterReading.TabIndex = 0;
        btnEnterReading.Text = "إدخال قراءة";
        btnEnterReading.UseVisualStyleBackColor = false;
        btnEnterReading.Visible = false;

        // 
        // tabPayments
        // 
        tabPayments.Controls.Add(dgvPayments);
        tabPayments.Controls.Add(pnlPaymentsActions);
        tabPayments.Location = new Point(4, 25);
        tabPayments.Name = "tabPayments";
        tabPayments.Padding = new Padding(3);
        tabPayments.Size = new Size(1176, 438);
        tabPayments.TabIndex = 2;
        tabPayments.Text = "سجل المدفوعات والإيصالات والعكس (0)";
        tabPayments.UseVisualStyleBackColor = true;

        // 
        // dgvPayments
        // 
        dgvPayments.ColumnHeadersDefaultCellStyle = dgvCellStyleHeader;
        dgvPayments.ColumnHeadersHeight = 32;
        dgvPayments.Dock = DockStyle.Fill;
        dgvPayments.Location = new Point(3, 3);
        dgvPayments.Name = "dgvPayments";
        dgvPayments.RowHeadersVisible = false;
        dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPayments.MultiSelect = false;
        dgvPayments.ReadOnly = true;
        dgvPayments.AllowUserToAddRows = false;
        dgvPayments.AllowUserToDeleteRows = false;
        dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPayments.Size = new Size(1170, 382);
        dgvPayments.TabIndex = 0;

        // 
        // pnlPaymentsActions
        // 
        pnlPaymentsActions.BackColor = Color.FromArgb(245, 247, 250);
        pnlPaymentsActions.BorderStyle = BorderStyle.FixedSingle;
        pnlPaymentsActions.Controls.Add(btnReversePayment);
        pnlPaymentsActions.Dock = DockStyle.Bottom;
        pnlPaymentsActions.Location = new Point(3, 388);
        pnlPaymentsActions.Name = "pnlPaymentsActions";
        pnlPaymentsActions.Size = new Size(1170, 47);
        pnlPaymentsActions.TabIndex = 1;

        // btnReversePayment
        btnReversePayment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnReversePayment.BackColor = Color.FromArgb(156, 47, 28);
        btnReversePayment.FlatStyle = FlatStyle.Flat;
        btnReversePayment.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnReversePayment.ForeColor = Color.White;
        btnReversePayment.Location = new Point(12, 6);
        btnReversePayment.Name = "btnReversePayment";
        btnReversePayment.Size = new Size(150, 34);
        btnReversePayment.TabIndex = 0;
        btnReversePayment.Text = "عكس الدفعة";
        btnReversePayment.UseVisualStyleBackColor = false;
        btnReversePayment.Visible = false;

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { tslblStatus, tslblCounts, tspbProgress });
        statusStrip.Location = new Point(0, 652);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1184, 24);
        statusStrip.TabIndex = 2;

        // tslblStatus
        tslblStatus.Name = "tslblStatus";
        tslblStatus.Size = new Size(40, 19);
        tslblStatus.Text = "جاهز";

        // tslblCounts
        tslblCounts.Name = "tslblCounts";
        tslblCounts.Size = new Size(1029, 19);
        tslblCounts.Spring = true;
        tslblCounts.TextAlign = ContentAlignment.MiddleLeft;

        // tspbProgress
        tspbProgress.Name = "tspbProgress";
        tspbProgress.Size = new Size(100, 18);
        tspbProgress.Style = ProgressBarStyle.Marquee;
        tspbProgress.Visible = false;

        // 
        // CustomerCollectionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1184, 676);
        Controls.Add(tabDetails);
        Controls.Add(pnlTop);
        Controls.Add(statusStrip);
        Font = new Font("Segoe UI", 9.5F);
        KeyPreview = true;
        MinimumSize = new Size(950, 600);
        Name = "CustomerCollectionForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "شاشة تحصيل العميل - WaterStation";

        pnlTop.ResumeLayout(false);
        grpSearch.ResumeLayout(false);
        grpSearch.PerformLayout();
        grpCustomerInfo.ResumeLayout(false);
        grpCustomerInfo.PerformLayout();
        tabDetails.ResumeLayout(false);
        tabOpenInvoices.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvOpenInvoices).EndInit();
        pnlInvoiceSummary.ResumeLayout(false);
        pnlInvoiceSummary.PerformLayout();
        tabMeters.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvMeters).EndInit();
        pnlMetersActions.ResumeLayout(false);
        tabPayments.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
        pnlPaymentsActions.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
