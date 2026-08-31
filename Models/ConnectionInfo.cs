namespace WaterStation.Models;

/// <summary>
/// Read-only view of the current connection settings (no secrets). Used by the settings screen.
/// </summary>
public sealed class ConnectionInfo
{
    public string Server { get; init; } = string.Empty;
    public string Database { get; init; } = string.Empty;
    public string AuthenticationMode { get; init; } = string.Empty;
    public bool UsesWindowsAuthentication { get; init; }
    public bool HasCredentials { get; init; }
}