using Microsoft.Data.SqlClient;
using WaterStation.Infrastructure;
using WaterStation.Services;

namespace WaterStation.Forms;

/// <summary>
/// Create-only customer dialog. All persistence goes through CustomerService.CreateAsync.
/// Update/Delete are intentionally not offered (no safe backend procedure exists).
/// </summary>
public sealed partial class CustomerEditForm : Form
{
    private readonly CustomerService _customerService;
    private UiBusy? _busy;
    private CancellationTokenSource? _cts;
    private bool _isSaving;

    public CustomerEditForm(CustomerService customerService)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        InitializeComponent();
        ApplyUiTheme();
        ConfigureStatusComboBox();
        RegisterEventHandlers();
        ApplyAccessibleNames();
        UpdateButtonState();
    }

    private void ApplyAccessibleNames()
    {
        txtCustomerNumber.AccessibleName = "رقم العميل";
        txtFullName.AccessibleName = "الاسم الكامل";
        txtPhone.AccessibleName = "رقم الجوال";
        nudFamilyCount.AccessibleName = "عدد أفراد الأسرة";
        cmbStatus.AccessibleName = "حالة العميل";
        txtAddress.AccessibleName = "العنوان";
        txtNotes.AccessibleName = "ملاحظات";
        btnSave.AccessibleName = "حفظ العميل";
        btnCancel.AccessibleName = "إلغاء";
    }

    private void UpdateButtonState()
    {
        btnSave.Enabled = !_isSaving
            && txtCustomerNumber.Text.Trim().Length > 0
            && txtFullName.Text.Trim().Length > 0;
    }

    private void ApplyUiTheme()
    {
        UiTheme.ApplyFormSurface(this);
        UiTheme.StylePrimaryButton(btnSave);
        UiTheme.StyleTertiaryButton(btnCancel);
    }

    private void ConfigureStatusComboBox()
    {
        cmbStatus.Items.Add(new StatusItem((byte)1, "نشط"));
        cmbStatus.Items.Add(new StatusItem((byte)0, "غير نشط"));
        cmbStatus.SelectedIndex = 0;
    }

    private void RegisterEventHandlers()
    {
        btnSave.Click += async (s, e) => await SaveAsync();
        btnCancel.Click += (s, e) =>
        {
            if (!_isSaving)
            {
                DialogResult = DialogResult.Cancel;
            }
        };

        txtCustomerNumber.TextChanged += (s, e) => UpdateButtonState();
        txtFullName.TextChanged += (s, e) => UpdateButtonState();

        KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Escape && !_isSaving)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
            }
        };

        Shown += (s, e) =>
        {
            txtCustomerNumber.Focus();
            txtCustomerNumber.SelectAll();
        };
    }

    private async Task SaveAsync()
    {
        if (_isSaving)
        {
            return;
        }

        var customerNumber = txtCustomerNumber.Text.Trim();
        var fullName = txtFullName.Text.Trim();
        var phone = NullIfEmpty(txtPhone.Text.Trim());
        var address = NullIfEmpty(txtAddress.Text.Trim());
        var notes = NullIfEmpty(txtNotes.Text.Trim());
        var familyCount = nudFamilyCount.Value == 0 ? (int?)null : (int)nudFamilyCount.Value;
        var status = ((StatusItem)cmbStatus.SelectedItem!).Value;

        if (customerNumber.Length == 0)
        {
            UiMessages.Warning("يجب إدخال رقم العميل.", "بيانات ناقصة");
            txtCustomerNumber.Focus();
            return;
        }

        if (fullName.Length == 0)
        {
            UiMessages.Warning("يجب إدخال الاسم الكامل.", "بيانات ناقصة");
            txtFullName.Focus();
            return;
        }

        _busy ??= new UiBusy(
            new Control[] { btnSave, btnCancel, txtCustomerNumber, txtFullName, txtPhone, txtAddress, txtNotes, nudFamilyCount, cmbStatus },
            pbSaving,
            ctl => { },
            string.Empty);

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        _isSaving = true;
        _busy.Begin(string.Empty);
        try
        {
            await _customerService.CreateAsync(
                customerNumber,
                fullName,
                status,
                phone,
                address,
                familyCount,
                notes,
                createdBy: null,
                cancellationToken: token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            DialogResult = DialogResult.OK;
        }
        catch (OperationCanceledException)
        {
        }
        catch (SqlException sqlEx) when (sqlEx.Number == 2601 || sqlEx.Number == 2627)
        {
            UiMessages.Warning("رقم العميل مسجل مسبقاً في النظام، يرجى استخدام رقم آخر.", "رقم مكرر");
            txtCustomerNumber.Focus();
            txtCustomerNumber.SelectAll();
        }
        catch (SqlException sqlEx) when (sqlEx.Number == 8152 || sqlEx.Number == 2628)
        {
            UiMessages.Warning("أحد الحقول أطول من المسموح به، يرجى تقصير النص.", "بيانات طويلة");
        }
        catch (SqlException sqlEx)
        {
            UiMessages.Warning($"حدث خطأ من قاعدة البيانات أثناء حفظ العميل:\n{sqlEx.Message}", "خطأ في قاعدة البيانات");
        }
        catch (Exception ex)
        {
            UiMessages.Error($"حدث خطأ غير متوقع أثناء حفظ العميل:\n{ex.Message}");
        }
        finally
        {
            _isSaving = false;
            _busy.End(string.Empty);
            UpdateButtonState();
            _cts?.Dispose();
            _cts = null;
        }
    }

    private static string? NullIfEmpty(string value) => value.Length == 0 ? null : value;

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isSaving)
        {
            e.Cancel = true;
            return;
        }

        _cts?.Cancel();
        _cts?.Dispose();
        _busy?.Dispose();
        base.OnFormClosing(e);
    }

    private sealed class StatusItem
    {
        public StatusItem(byte value, string label)
        {
            Value = value;
            Label = label;
        }

        public byte Value { get; }

        public string Label { get; }

        public override string ToString() => Label;
    }
}