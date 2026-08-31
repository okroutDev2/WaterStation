namespace WaterStation.Models;

/// <summary>
/// Represents the four result sets returned by Billing.GetCustomerCollectionScreen:
/// 1. Customer information
/// 2. Customer meters and their last reading/consumption
/// 3. Open invoices
/// 4. Payment history, receipts, and payment reversals
/// </summary>
public sealed class CustomerCollectionScreenResult
{
    public Customer? Customer { get; init; }
    public IReadOnlyList<Meter> Meters { get; init; } = Array.Empty<Meter>();
    public IReadOnlyList<Invoice> OpenInvoices { get; init; } = Array.Empty<Invoice>();
    public IReadOnlyList<Payment> Payments { get; init; } = Array.Empty<Payment>();
}

