using WaterStation.Infrastructure;
using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

/// <summary>
/// Read-only screen listing open invoices with the option to pay the selected invoice.
/// </summary>
public sealed class InvoicesViewForm : RecordsViewForm<Invoice>
{
    private readonly BillingService _billingService;

    public InvoicesViewForm(BillingService billingService)
    {
        _billingService = billingService ?? throw new ArgumentNullException(nameof(billingService));
    }

    protected override string ScreenTitle => "الفواتير المفتوحة - WaterStation";

    protected override string SearchCaption => "بحث برقم الفاتورة أو رقم أو اسم العميل:";

    protected override string SearchPlaceholder => "رقم الفاتورة / رقم العميل / اسم العميل";

    protected override string? ActionButtonText => "سداد الفاتورة";

    protected override void ConfigureGrid()
    {
        dgvList.AccessibleName = "جدول الفواتير المفتوحة";
        dgvList.Columns.Add(Column(nameof(Invoice.InvoiceNumber), "رقم الفاتورة", 110));
        dgvList.Columns.Add(Column(nameof(Invoice.CustomerNumber), "رقم العميل", 80));
        dgvList.Columns.Add(Column(nameof(Invoice.FullName), "اسم العميل", 150));
        dgvList.Columns.Add(Column(nameof(Invoice.MeterNumber), "رقم العداد", 80));
        dgvList.Columns.Add(Column(nameof(Invoice.BillingYear), "السنة", 50, "0"));
        dgvList.Columns.Add(Column(nameof(Invoice.BillingMonth), "الشهر", 50, "0"));
        dgvList.Columns.Add(Column(nameof(Invoice.InvoiceDate), "تاريخ الفاتورة", 85, "yyyy-MM-dd"));
        dgvList.Columns.Add(Column(nameof(Invoice.UnitsConsumed), "الاستهلاك", 70, "N3"));
        dgvList.Columns.Add(Column(nameof(Invoice.TotalAmount), "الإجمالي", 75, "N2"));
        dgvList.Columns.Add(Column(nameof(Invoice.PaidAmount), "المدفوع", 70, "N2"));
        dgvList.Columns.Add(Column(nameof(Invoice.BalanceAmount), "المتبقي", 75, "N2"));
        dgvList.Columns.Add(Column(nameof(Invoice.StatusName), "الحالة", 80));
    }

    protected override async Task<IReadOnlyList<Invoice>> LoadCoreAsync(string? filter, CancellationToken cancellationToken)
    {
        var invoices = await _billingService.GetOpenInvoicesAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(filter))
        {
            return invoices;
        }

        return invoices
            .Where(i => Contains(i.InvoiceNumber, filter)
                        || Contains(i.CustomerNumber, filter)
                        || Contains(i.FullName, filter))
            .ToList();
    }

    protected override async Task OnActionClickedAsync(CancellationToken cancellationToken)
    {
        if (dgvList.CurrentRow?.DataBoundItem is not Invoice selectedInvoice)
        {
            UiMessages.Warning("يرجى تحديد فاتورة من الجدول أولاً.", "تنبيه");
            return;
        }

        if (selectedInvoice.BalanceAmount <= 0m)
        {
            UiMessages.Information("لا يوجد رصيد متبقي لهذه الفاتورة.", "معلومات");
            return;
        }

        using var paymentForm = new PaymentForm(selectedInvoice);
        if (paymentForm.ShowDialog(this) == DialogResult.OK)
        {
            await PerformSearchAsync();
        }
    }

    private static bool Contains(string? value, string filter) =>
        !string.IsNullOrWhiteSpace(value) && value.Contains(filter, StringComparison.OrdinalIgnoreCase);
}