namespace WaterStation.Models;

public sealed class Meter
{
    public int MeterId { get; init; }
    public string MeterNumber { get; init; } = string.Empty;
    public int CustomerId { get; init; }
    public string? CustomerNumber { get; init; }
    public string? FullName { get; init; }
    public string? Phone { get; init; }
    public string? Address { get; init; }
    public int BranchId { get; init; }
    public string? BranchCode { get; init; }
    public string? BranchName { get; init; }
    public int? AreaId { get; init; }
    public string? AreaCode { get; init; }
    public string? AreaName { get; init; }
    public int MeterTypeId { get; init; }
    public string? MeterTypeCode { get; init; }
    public string? MeterTypeName { get; init; }
    public byte? ReadingDirection { get; init; }
    public string? ReadingDirectionName { get; init; }
    public DateOnly InstallationDate { get; init; }
    public decimal InstallationReading { get; init; }
    public byte Status { get; init; }
    public DateOnly? RemovalDate { get; init; }
    public decimal? RemovalReading { get; init; }
    public string? Notes { get; init; }
    public DateTime? CreatedAt { get; init; }
    public int? CreatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? UpdatedBy { get; init; }
    public DateOnly? LastReadingDate { get; init; }
    public decimal? LastReadingValue { get; init; }
    public decimal? LastConsumption { get; init; }
    public decimal? CumulativeConsumption { get; init; }
    public bool? LastIsReverseMeter { get; init; }
}

