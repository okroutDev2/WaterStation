using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Models;

namespace WaterStation.Services;

/// <summary>
/// Provides summary counters for the main dashboard using existing database views only.
/// </summary>
public sealed class OverviewService : ServiceBase
{
    private const string OverviewSql = """
        SELECT
            (SELECT COUNT(DISTINCT [CustomerId]) FROM [Core].[vw_CustomerMeters]),
            (SELECT COUNT(*) FROM [Core].[vw_ActiveMeters]),
            (SELECT COUNT(*) FROM [Billing].[vw_OpenInvoices]),
            (SELECT ISNULL(SUM([BalanceAmount]), 0.00) FROM [Billing].[vw_OpenInvoices]);
        """;

    public OverviewService(Database database) : base(database)
    {
    }

    public async Task<OverviewSnapshot> GetOverviewAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(OverviewSql, connection);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return new OverviewSnapshot(0, 0, 0, 0.00m);
        }

        return new OverviewSnapshot(
            reader.GetInt32(0),
            reader.GetInt32(1),
            reader.GetInt32(2),
            reader.GetDecimal(3));
    }
}