using System.Text.Json;

namespace WaterStation.Infrastructure;

/// <summary>
/// Centralized SQL Server connection settings for the application.
///
/// The effective connection string is the built-in default, overridable by a
/// non-secret "ConnectionStrings:WaterStation" entry in an optional
/// "appsettings.json" placed next to the executable (e.g. to point a deployed
/// copy at a different server). Only Windows Integrated Security strings are
/// accepted; any entry containing a password is rejected and the default used.
/// </summary>
public static class DatabaseSettings
{
    public const string DefaultConnectionString =
        "Server=ABODE\\MSSQLSERVER16;Database=WaterStationDB;Integrated Security=True;TrustServerCertificate=True;";

    static DatabaseSettings()
    {
        ExternalConnectionString = TryLoadExternalConnectionString();
    }

    /// <summary>
    /// The effective connection string: external override if one is valid, else the default.
    /// The connection settings (Encrypt / TrustServerCertificate / CommandTimeout) are
    /// intentionally left at their documented values and are never rewritten by the app.
    /// </summary>
    public static string ConnectionString => ExternalConnectionString ?? DefaultConnectionString;

    private static readonly string? ExternalConnectionString;

    private static string? TryLoadExternalConnectionString()
    {
        try
        {
            var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(configPath))
            {
                return null;
            }

            using var stream = File.OpenRead(configPath);
            using var document = JsonDocument.Parse(stream);

            if (!document.RootElement.TryGetProperty("ConnectionStrings", out var connections) ||
                !connections.TryGetProperty("WaterStation", out var entry))
            {
                return null;
            }

            var text = entry.GetString();
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            text = text.Trim();

            // Integrated Security only: never accept (or store) credentials in config.
            if (ContainsIgnoreCase(text, "Password=") || ContainsIgnoreCase(text, "Pwd=") ||
                ContainsIgnoreCase(text, "User Id=") || ContainsIgnoreCase(text, "UserID="))
            {
                return null;
            }

            return text;
        }
        catch (JsonException)
        {
            return null;
        }
        catch (IOException)
        {
            return null;
        }
        catch (UnauthorizedAccessException)
        {
            return null;
        }
    }

    private static bool ContainsIgnoreCase(string value, string token) =>
        value.Contains(token, StringComparison.OrdinalIgnoreCase);
}
