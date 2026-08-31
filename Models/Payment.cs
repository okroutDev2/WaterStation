namespace WaterStation.Models;

public sealed class Payment
{
    public long PaymentId { get; init; }
    public long? ReceiptId { get; init; }
    public string? ReceiptNumber { get; init; }
    public long InvoiceId { get; init; }
    public string? InvoiceNumber { get; init; }
    public int? CustomerId { get; init; }
    public DateTime PaymentDate { get; init; }
    public decimal Amount { get; init; }
    public int? PaymentMethodId { get; init; }
    public string? PaymentMethodCode { get; init; }
    public string? PaymentMethodName { get; init; }
    public string? ReferenceNumber { get; init; }
    public string? Notes { get; init; }
    public decimal? TotalAmount { get; init; }
    public decimal? PaidAmount { get; init; }
    public decimal? BalanceAmount { get; init; }
    public byte? Status { get; init; }
    public string? StatusName { get; init; }
    public bool IsReversed { get; init; }
    public long? PaymentReversalId { get; init; }
    public DateTime? ReversalDate { get; init; }
    public string? ReversalReason { get; init; }
}

