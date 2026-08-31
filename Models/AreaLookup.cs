namespace WaterStation.Models;

public sealed class AreaLookup
{
    public int AreaId { get; init; }
    public int BranchId { get; init; }
    public string AreaCode { get; init; } = string.Empty;
    public string AreaName { get; init; } = string.Empty;
    public bool IsActive { get; init; }

    public override string ToString() => string.IsNullOrWhiteSpace(AreaCode) ? AreaName : $"{AreaCode} - {AreaName}";
}
