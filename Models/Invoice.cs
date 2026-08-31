namespace WaterStation.Models;

public sealed class Invoice
{
    public long InvoiceId { get; init; }
    public string InvoiceNumber { get; init; } = string.Empty;
    public int CustomerId { get; init; }
    public string? CustomerNumber { get; init; }
    public string? FullName { get; init; }
    public string? Phone { get; init; }
    public int? MeterId { get; init; }
    public string? MeterNumber { get; init; }
    public short? BillingYear { get; init; }
    public byte? BillingMonth { get; init; }
    public DateOnly? InvoiceDate { get; init; }
    public DateOnly? DueDate { get; init; }
    public decimal? PreviousReading { get; init; }
    public decimal? CurrentReading { get; init; }
    public decimal? UnitsConsumed { get; init; }
    public decimal? WaterAmount { get; init; }
    public decimal? SubscriptionAmount { get; init; }
    public decimal? PenaltyAmount { get; init; }
    public decimal? ArrearsAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal PaidAmount { get; init; }
    public decimal BalanceAmount { get; init; }
    public decimal? TransferredAmount { get; init; }
    public decimal? OutstandingAmount { get; init; }
    public bool? IsTransferred { get; init; }
    public byte Status { get; init; }
    public string? StatusName { get; init; }
    public string? Notes { get; init; }
}

