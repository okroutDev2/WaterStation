namespace WaterStation.Models;

public sealed class PaymentReversal
{
    public long PaymentReversalId { get; init; }
    public long PaymentId { get; init; }
    public long? InvoiceId { get; init; }
    public string? InvoiceNumber { get; init; }
    public DateTime ReversalDate { get; init; }
    public decimal? Amount { get; init; }
    public string Reason { get; init; } = string.Empty;
    public DateTime? CreatedAt { get; init; }
    public int? CreatedBy { get; init; }
    public int? ReversedBy { get; init; }
    public decimal? TotalAmount { get; init; }
    public decimal? PaidAmount { get; init; }
    public decimal? BalanceAmount { get; init; }
    public byte? Status { get; init; }
    public string? StatusName { get; init; }
}

