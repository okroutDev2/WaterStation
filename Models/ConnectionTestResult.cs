namespace WaterStation.Models;

/// <summary>
/// Result of a read-only connection test against Data/Database.cs.
/// </summary>
public sealed class ConnectionTestResult
{
    public bool IsSuccess { get; init; }
    public TimeSpan Elapsed { get; init; }
    public string? ErrorMessage { get; init; }

    public long ElapsedMilliseconds => (long)Math.Round(Elapsed.TotalMilliseconds, MidpointRounding.AwayFromZero);
}