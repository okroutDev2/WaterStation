using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

/// <summary>
/// Read-only screen listing the most recent receipts with filtering options.
/// </summary>
public sealed class ReceiptsViewForm : RecordsViewForm<Receipt>
{
    private readonly ReceiptService _receiptService;

    public ReceiptsViewForm(ReceiptService receiptService)
    {
        _receiptService = receiptService ?? throw new ArgumentNullException(nameof(receiptService));
    }

    protected override string ScreenTitle => "الإيصالات - WaterStation";

    protected override string SearchCaption => "بحث برقم الإيصال أو رقم/اسم العميل أو رقم الفاتورة:";

    protected override string SearchPlaceholder => "رقم الإيصال / رقم العميل / اسم العميل / رقم الفاتورة";

    protected override void ConfigureGrid()
    {
        dgvList.AccessibleName = "جدول الإيصالات";
        dgvList.Columns.Add(Column(nameof(Receipt.ReceiptNumber), "رقم الإيصال", 95));
        dgvList.Columns.Add(Column(nameof(Receipt.ReceiptDate), "تاريخ الإيصال", 100, "yyyy-MM-dd HH:mm"));
        dgvList.Columns.Add(Column(nameof(Receipt.CustomerNumber), "رقم العميل", 80));
        dgvList.Columns.Add(Column(nameof(Receipt.FullName), "اسم العميل", 140));
        dgvList.Columns.Add(Column(nameof(Receipt.InvoiceNumber), "رقم الفاتورة", 90));
        dgvList.Columns.Add(Column(nameof(Receipt.Amount), "مبلغ الدفعة", 80, "N2"));
        dgvList.Columns.Add(Column(nameof(Receipt.PaymentMethodName), "طريقة الدفع", 90));
        dgvList.Columns.Add(Column(nameof(Receipt.ReferenceNumber), "رقم المرجع", 90));
        dgvList.Columns.Add(Column(nameof(Receipt.StatusName), "حالة الفاتورة", 80));
    }

    protected override async Task<IReadOnlyList<Receipt>> LoadCoreAsync(string? filter, CancellationToken cancellationToken)
    {
        var receipts = await _receiptService.GetRecentReceiptsAsync(topN: 300, cancellationToken);
        if (string.IsNullOrWhiteSpace(filter))
        {
            return receipts;
        }

        return receipts
            .Where(r => Contains(r.ReceiptNumber, filter)
                        || Contains(r.CustomerNumber, filter)
                        || Contains(r.FullName, filter)
                        || Contains(r.InvoiceNumber, filter))
            .ToList();
    }

    private static bool Contains(string? value, string filter) =>
        !string.IsNullOrWhiteSpace(value) && value.Contains(filter, StringComparison.OrdinalIgnoreCase);
}