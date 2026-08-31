namespace WaterStation.Models;

public sealed class Customer
{
    public int CustomerId { get; init; }
    public string CustomerNumber { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public string? Address { get; init; }
    public int? FamilyMembersCount { get; init; }
    public byte Status { get; init; }
    public string? Notes { get; init; }
    public DateTime? CreatedAt { get; init; }
    public int? CreatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? UpdatedBy { get; init; }
}

