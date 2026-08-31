using System.Diagnostics;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Infrastructure;
using WaterStation.Models;

namespace WaterStation.Services;

/// <summary>
/// Provides read-only connection information and a read-only connection test.
/// Never returns secrets (passwords/keys) and never executes commands or stored procedures.
/// </summary>
public sealed class ConnectionService
{
    private readonly Database _database;

    public ConnectionService(Database database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    /// <summary>
    /// Parses the current connection string (read-only) into display-safe information.
    /// No secret is ever exposed.
    /// </summary>
    public ConnectionInfo GetConnectionInfo()
    {
        var builder = new SqlConnectionStringBuilder(DatabaseSettings.ConnectionString);
        bool windowsAuth = builder.IntegratedSecurity;

        return new ConnectionInfo
        {
            Server = builder.DataSource ?? string.Empty,
            Database = builder.InitialCatalog ?? string.Empty,
            AuthenticationMode = windowsAuth
                ? "التحقق المدمج من ويندوز"
                : "مصادقة SQL Server",
            UsesWindowsAuthentication = windowsAuth,
            HasCredentials = !windowsAuth && !string.IsNullOrWhiteSpace(builder.UserID)
        };
    }

    /// <summary>
    /// Opens a connection through Data/Database.cs only. Executes nothing (no stored
    /// procedures, no SELECT, no INSERT/UPDATE/DELETE). Reports success/failure and latency.
    /// </summary>
    public async Task<ConnectionTestResult> TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            using var connection = await _database.OpenConnectionAsync(cancellationToken);
            stopwatch.Stop();
            return new ConnectionTestResult { IsSuccess = true, Elapsed = stopwatch.Elapsed, ErrorMessage = null };
        }
        catch (OperationCanceledException)
        {
            stopwatch.Stop();
            throw;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            return new ConnectionTestResult { IsSuccess = false, Elapsed = stopwatch.Elapsed, ErrorMessage = ex.Message };
        }
    }
}