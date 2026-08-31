#nullable enable

namespace WaterStation.Forms;

partial class CustomerEditForm
{
    private System.ComponentModel.IContainer? components = null;

    private Label lblNumberCaption = null!;
    private TextBox txtCustomerNumber = null!;
    private Label lblNameCaption = null!;
    private TextBox txtFullName = null!;
    private Label lblPhoneCaption = null!;
    private TextBox txtPhone = null!;
    private Label lblFamilyCaption = null!;
    private NumericUpDown nudFamilyCount = null!;
    private Label lblStatusCaption = null!;
    private ComboBox cmbStatus = null!;
    private Label lblAddressCaption = null!;
    private TextBox txtAddress = null!;
    private Label lblNotesCaption = null!;
    private TextBox txtNotes = null!;
    private Panel pnlActions = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;
    private ProgressBar pbSaving = null!;

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
        lblNumberCaption = new Label();
        txtCustomerNumber = new TextBox();
        lblNameCaption = new Label();
        txtFullName = new TextBox();
        lblPhoneCaption = new Label();
        txtPhone = new TextBox();
        lblFamilyCaption = new Label();
        nudFamilyCount = new NumericUpDown();
        lblStatusCaption = new Label();
        cmbStatus = new ComboBox();
        lblAddressCaption = new Label();
        txtAddress = new TextBox();
        lblNotesCaption = new Label();
        txtNotes = new TextBox();
        pnlActions = new Panel();
        pbSaving = new ProgressBar();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)nudFamilyCount).BeginInit();
        pnlActions.SuspendLayout();
        SuspendLayout();

        // 
        // lblNumberCaption
        // 
        lblNumberCaption.AutoSize = true;
        lblNumberCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNumberCaption.Location = new Point(474, 24);
        lblNumberCaption.Name = "lblNumberCaption";
        lblNumberCaption.Size = new Size(80, 17);
        lblNumberCaption.TabIndex = 0;
        lblNumberCaption.Text = "رقم العميل (*)";

        // 
        // txtCustomerNumber
        // 
        txtCustomerNumber.Font = new Font("Segoe UI", 9.5F);
        txtCustomerNumber.Location = new Point(244, 20);
        txtCustomerNumber.MaxLength = 30;
        txtCustomerNumber.Name = "txtCustomerNumber";
        txtCustomerNumber.Size = new Size(224, 24);
        txtCustomerNumber.TabIndex = 1;

        // 
        // lblNameCaption
        // 
        lblNameCaption.AutoSize = true;
        lblNameCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNameCaption.Location = new Point(474, 59);
        lblNameCaption.Name = "lblNameCaption";
        lblNameCaption.Size = new Size(82, 17);
        lblNameCaption.TabIndex = 2;
        lblNameCaption.Text = "الاسم الكامل (*)";

        // 
        // txtFullName
        // 
        txtFullName.Font = new Font("Segoe UI", 9.5F);
        txtFullName.Location = new Point(244, 55);
        txtFullName.MaxLength = 250;
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(224, 24);
        txtFullName.TabIndex = 3;

        // 
        // lblPhoneCaption
        // 
        lblPhoneCaption.AutoSize = true;
        lblPhoneCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPhoneCaption.Location = new Point(474, 94);
        lblPhoneCaption.Name = "lblPhoneCaption";
        lblPhoneCaption.Size = new Size(46, 17);
        lblPhoneCaption.TabIndex = 4;
        lblPhoneCaption.Text = "الهاتف:";

        // 
        // txtPhone
        // 
        txtPhone.Font = new Font("Segoe UI", 9.5F);
        txtPhone.Location = new Point(244, 90);
        txtPhone.MaxLength = 50;
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(224, 24);
        txtPhone.TabIndex = 5;

        // 
        // lblFamilyCaption
        // 
        lblFamilyCaption.AutoSize = true;
        lblFamilyCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblFamilyCaption.Location = new Point(474, 129);
        lblFamilyCaption.Name = "lblFamilyCaption";
        lblFamilyCaption.Size = new Size(69, 17);
        lblFamilyCaption.TabIndex = 6;
        lblFamilyCaption.Text = "عدد الأفراد:";

        // 
        // nudFamilyCount
        // 
        nudFamilyCount.Font = new Font("Segoe UI", 9.5F);
        nudFamilyCount.Location = new Point(330, 125);
        nudFamilyCount.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        nudFamilyCount.Name = "nudFamilyCount";
        nudFamilyCount.Size = new Size(80, 24);
        nudFamilyCount.TabIndex = 7;
        nudFamilyCount.TextAlign = HorizontalAlignment.Center;

        // 
        // lblStatusCaption
        // 
        lblStatusCaption.AutoSize = true;
        lblStatusCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblStatusCaption.Location = new Point(238, 129);
        lblStatusCaption.Name = "lblStatusCaption";
        lblStatusCaption.Size = new Size(49, 17);
        lblStatusCaption.TabIndex = 8;
        lblStatusCaption.Text = "الحالة:";

        // 
        // cmbStatus
        // 
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Font = new Font("Segoe UI", 9.5F);
        cmbStatus.Location = new Point(146, 125);
        cmbStatus.Name = "cmbStatus";
        cmbStatus.Size = new Size(86, 24);
        cmbStatus.TabIndex = 9;

        // 
        // lblAddressCaption
        // 
        lblAddressCaption.AutoSize = true;
        lblAddressCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblAddressCaption.Location = new Point(474, 164);
        lblAddressCaption.Name = "lblAddressCaption";
        lblAddressCaption.Size = new Size(53, 17);
        lblAddressCaption.TabIndex = 10;
        lblAddressCaption.Text = "العنوان:";

        // 
        // txtAddress
        // 
        txtAddress.Font = new Font("Segoe UI", 9.5F);
        txtAddress.Location = new Point(244, 160);
        txtAddress.MaxLength = 500;
        txtAddress.Multiline = true;
        txtAddress.Name = "txtAddress";
        txtAddress.ScrollBars = ScrollBars.Vertical;
        txtAddress.Size = new Size(224, 64);
        txtAddress.TabIndex = 11;

        // 
        // lblNotesCaption
        // 
        lblNotesCaption.AutoSize = true;
        lblNotesCaption.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotesCaption.Location = new Point(474, 234);
        lblNotesCaption.Name = "lblNotesCaption";
        lblNotesCaption.Size = new Size(56, 17);
        lblNotesCaption.TabIndex = 12;
        lblNotesCaption.Text = "ملاحظات:";

        // 
        // txtNotes
        // 
        txtNotes.Font = new Font("Segoe UI", 9.5F);
        txtNotes.Location = new Point(244, 230);
        txtNotes.MaxLength = 1000;
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        txtNotes.ScrollBars = ScrollBars.Vertical;
        txtNotes.Size = new Size(224, 64);
        txtNotes.TabIndex = 13;

        // 
        // pnlActions
        // 
        pnlActions.Controls.Add(pbSaving);
        pnlActions.Controls.Add(btnSave);
        pnlActions.Controls.Add(btnCancel);
        pnlActions.Dock = DockStyle.Bottom;
        pnlActions.Location = new Point(0, 306);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(560, 54);
        pnlActions.TabIndex = 14;

        // 
        // pbSaving
        // 
        pbSaving.Location = new Point(100, 12);
        pbSaving.Name = "pbSaving";
        pbSaving.Size = new Size(360, 4);
        pbSaving.Style = ProgressBarStyle.Marquee;
        pbSaving.TabIndex = 0;
        pbSaving.Visible = false;

        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(0, 122, 204);
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(430, 12);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(118, 32);
        btnSave.TabIndex = 15;
        btnSave.Text = "حفظ العميل";
        btnSave.UseVisualStyleBackColor = false;

        // 
        // btnCancel
        // 
        btnCancel.BackColor = Color.FromArgb(240, 240, 240);
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 9F);
        btnCancel.Location = new Point(306, 12);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(118, 32);
        btnCancel.TabIndex = 16;
        btnCancel.Text = "إلغاء";
        btnCancel.UseVisualStyleBackColor = true;

        // 
        // CustomerEditForm
        // 
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(560, 360);
        Controls.Add(txtNotes);
        Controls.Add(lblNotesCaption);
        Controls.Add(txtAddress);
        Controls.Add(lblAddressCaption);
        Controls.Add(cmbStatus);
        Controls.Add(lblStatusCaption);
        Controls.Add(nudFamilyCount);
        Controls.Add(lblFamilyCaption);
        Controls.Add(txtPhone);
        Controls.Add(lblPhoneCaption);
        Controls.Add(txtFullName);
        Controls.Add(lblNameCaption);
        Controls.Add(txtCustomerNumber);
        Controls.Add(lblNumberCaption);
        Controls.Add(pnlActions);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "CustomerEditForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "إضافة عميل جديد - WaterStation";
        ((System.ComponentModel.ISupportInitialize)nudFamilyCount).EndInit();
        pnlActions.ResumeLayout(false);
        ResumeLayout(false);
    }
}