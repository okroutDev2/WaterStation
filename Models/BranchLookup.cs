namespace WaterStation.Models;

public sealed class BranchLookup
{
    public int BranchId { get; init; }
    public string BranchCode { get; init; } = string.Empty;
    public string BranchName { get; init; } = string.Empty;
    public bool IsActive { get; init; }

    public override string ToString() => string.IsNullOrWhiteSpace(BranchCode) ? BranchName : $"{BranchCode} - {BranchName}";
}
