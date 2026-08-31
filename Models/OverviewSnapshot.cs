namespace WaterStation.Models;

/// <summary>
/// Read-only snapshot of the summary counters shown on the main dashboard.
/// </summary>
public sealed record OverviewSnapshot(
    int CustomerCount,
    int ActiveMeterCount,
    int OpenInvoiceCount,
    decimal OpenBalanceTotal);