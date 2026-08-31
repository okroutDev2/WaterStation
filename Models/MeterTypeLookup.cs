namespace WaterStation.Models;

public sealed class MeterTypeLookup
{
    public int MeterTypeId { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public byte ReadingDirection { get; init; }
    public bool IsActive { get; init; }

    public override string ToString() => string.IsNullOrWhiteSpace(Code) ? Name : $"{Code} - {Name}";
}
