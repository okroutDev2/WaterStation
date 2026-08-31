namespace WaterStation.Models;

/// <summary>
/// A single row from [Billing].[vw_InvoiceBalances] as-is (read-only report data).
/// </summary>
public sealed class InvoiceBalance
{
    public long InvoiceId { get; init; }
    public string InvoiceNumber { get; init; } = string.Empty;
    public int CustomerId { get; init; }
    public int MeterId { get; init; }
    public short? BillingYear { get; init; }
    public byte? BillingMonth { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal PaidAmount { get; init; }
    public decimal BalanceAmount { get; init; }
    public decimal TransferredAmount { get; init; }
    public decimal TransferredByHistory { get; init; }
    public decimal OutstandingAmount { get; init; }
    public bool IsTransferred { get; init; }
    public byte Status { get; init; }
}