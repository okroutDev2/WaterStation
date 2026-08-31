using Microsoft.Data.SqlClient;
using WaterStation.Infrastructure;

namespace WaterStation.Data;

/// <summary>
/// Creates configured SQL Server connections for data-access services.
/// </summary>
public sealed class Database
{
    public async Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(DatabaseSettings.ConnectionString);

        try
        {
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
        catch
        {
            await connection.DisposeAsync();
            throw;
        }
    }

    public SqlCommand CreateStoredProcedureCommand(SqlConnection connection, string storedProcedureName)
    {
        ArgumentNullException.ThrowIfNull(connection);
        ArgumentException.ThrowIfNullOrWhiteSpace(storedProcedureName);

        return new SqlCommand(storedProcedureName, connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
    }
}
