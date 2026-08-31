namespace WaterStation.Models;

public sealed class Receipt
{
    public long ReceiptId { get; init; }
    public string ReceiptNumber { get; init; } = string.Empty;
    public long PaymentId { get; init; }
    public long? InvoiceId { get; init; }
    public string? InvoiceNumber { get; init; }
    public short? BillingYear { get; init; }
    public byte? BillingMonth { get; init; }
    public int CustomerId { get; init; }
    public string? CustomerNumber { get; init; }
    public string? FullName { get; init; }
    public string? Phone { get; init; }
    public string? Address { get; init; }
    public int? MeterId { get; init; }
    public string? MeterNumber { get; init; }
    public DateTime ReceiptDate { get; init; }
    public DateTime? PaymentDate { get; init; }
    public decimal Amount { get; init; }
    public string? PaymentMethodCode { get; init; }
    public string? PaymentMethodName { get; init; }
    public string? ReferenceNumber { get; init; }
    public decimal? TotalAmount { get; init; }
    public decimal? PaidAmount { get; init; }
    public decimal? BalanceAmount { get; init; }
    public byte? Status { get; init; }
    public string? StatusName { get; init; }
    public bool IsReversed { get; init; }
    public long? PaymentReversalId { get; init; }
    public DateTime? ReversalDate { get; init; }
    public decimal? ReversedAmount { get; init; }
    public string? ReversalReason { get; init; }
    public DateTime? CreatedAt { get; init; }
    public int? CreatedBy { get; init; }
    public string? Notes { get; init; }
}

