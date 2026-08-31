namespace WaterStation.Models;

public sealed class InvoiceLine
{
    public long InvoiceLineId { get; init; }
    public long InvoiceId { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Amount { get; init; }
}
