namespace WaterStation.Models;

public sealed class MeterReading
{
    public long MeterReadingId { get; init; }
    public int MeterId { get; init; }
    public string MeterNumber { get; init; } = string.Empty;
    public DateOnly ReadingDate { get; init; }
    public decimal ReadingValue { get; init; }
    public decimal? PreviousReading { get; init; }
    public decimal? Consumption { get; init; }
    public decimal? CumulativeConsumption { get; init; }
    public bool IsReverseMeter { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public int? CreatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? UpdatedBy { get; init; }
}
