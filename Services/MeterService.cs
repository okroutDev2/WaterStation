using System.Data;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Models;

namespace WaterStation.Services;

public sealed class MeterService : ServiceBase
{
    private const string CustomerMetersColumns =
        "[CustomerId], [CustomerNumber], [FullName], [Phone], [Address], [MeterId], [MeterNumber], [BranchId], [BranchCode], [BranchName], [AreaId], [AreaCode], [AreaName], [MeterTypeId], [MeterTypeCode], [MeterTypeName], [ReadingDirection], [InstallationDate], [InstallationReading], [RemovalDate], [RemovalReading], [MeterStatus], [MeterNotes], [MeterCreatedAt], [MeterCreatedBy], [MeterUpdatedAt], [MeterUpdatedBy]";

    private const string ActiveMetersColumns =
        "[MeterId], [MeterNumber], [CustomerId], [CustomerNumber], [FullName], [Phone], [Address], [BranchId], [BranchCode], [BranchName], [AreaId], [AreaCode], [AreaName], [MeterTypeId], [MeterTypeCode], [MeterTypeName], [ReadingDirection], [InstallationDate], [InstallationReading], [Status], [Notes], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]";

    private const string ActiveMetersSql = $"SELECT {ActiveMetersColumns} FROM [Core].[vw_ActiveMeters];";
    private const string CustomerMetersSql = $"SELECT {CustomerMetersColumns} FROM [Core].[vw_CustomerMeters] WHERE [CustomerId] = @CustomerId;";
    private const string MeterByNumberSql = $"SELECT {CustomerMetersColumns} FROM [Core].[vw_CustomerMeters] WHERE [MeterNumber] = @MeterNumber ORDER BY [MeterId];";

    private const string MeterReadingsColumns =
        "[MeterReadingId], [MeterId], [MeterNumber], [ReadingDate], [ReadingValue], [PreviousReading], [Consumption], [Notes], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]";
    private const string MeterReadingsSql = $"SELECT {MeterReadingsColumns} FROM [Core].[vw_MeterReadings] ORDER BY [ReadingDate] DESC, [MeterReadingId] DESC;";

    public MeterService(Database database) : base(database)
    {
    }

    public async Task<IReadOnlyList<Meter>> GetActiveMetersAsync(CancellationToken cancellationToken = default) =>
        await ReadMetersAsync(ActiveMetersSql, null, ModelMappers.ToMeterFromActiveMeters, cancellationToken);

    public async Task<IReadOnlyList<Meter>> GetCustomerMetersAsync(int customerId, CancellationToken cancellationToken = default) =>
        await ReadMetersAsync(CustomerMetersSql, customerId, ModelMappers.ToMeterFromCustomerMeters, cancellationToken);

    public async Task<Meter?> GetByMeterNumberAsync(string meterNumber, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(meterNumber);

        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(MeterByNumberSql, connection);
        command.Parameters.Add("@MeterNumber", SqlDbType.NVarChar, 100).Value = meterNumber.Trim();

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? ModelMappers.ToMeterFromCustomerMeters(reader) : null;
    }

    // New: read-only reference data access for Branches / Areas / MeterTypes
    public async Task<IReadOnlyList<BranchLookup>> GetBranchesAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"SELECT [BranchId], [BranchCode], [BranchName], ISNULL([IsActive], 1) AS IsActive FROM [Core].[Branches] WHERE ISNULL([IsActive], 1) = 1 ORDER BY [BranchName];";
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(sql, connection);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var list = new List<BranchLookup>();
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new BranchLookup
            {
                BranchId = reader.GetInt32(0),
                BranchCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                BranchName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                IsActive = !reader.IsDBNull(3) && reader.GetBoolean(3)
            });
        }

        return list;
    }

    public async Task<IReadOnlyList<AreaLookup>> GetAreasByBranchAsync(int branchId, CancellationToken cancellationToken = default)
    {
        const string sql = @"SELECT [AreaId], [BranchId], [AreaCode], [AreaName], ISNULL([IsActive], 1) AS IsActive FROM [Core].[Areas] WHERE [BranchId] = @BranchId AND ISNULL([IsActive], 1) = 1 ORDER BY [AreaName];";
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchId;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var list = new List<AreaLookup>();
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new AreaLookup
            {
                AreaId = reader.GetInt32(0),
                BranchId = reader.GetInt32(1),
                AreaCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                AreaName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                IsActive = !reader.IsDBNull(4) && reader.GetBoolean(4)
            });
        }

        return list;
    }

    public async Task<IReadOnlyList<MeterTypeLookup>> GetMeterTypesAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"SELECT [MeterTypeId], [Code], [Name], [ReadingDirection], ISNULL([IsActive], 1) AS IsActive FROM [Core].[MeterTypes] WHERE ISNULL([IsActive], 1) = 1 ORDER BY [Name];";
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(sql, connection);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var list = new List<MeterTypeLookup>();
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new MeterTypeLookup
            {
                MeterTypeId = reader.GetInt32(0),
                Code = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                ReadingDirection = reader.IsDBNull(3) ? (byte)0 : reader.GetByte(3),
                IsActive = !reader.IsDBNull(4) && reader.GetBoolean(4)
            });
        }

        return list;
    }

    public async Task<StoredProcedureExecutionResult> AddMeterAsync(
        StoredProcedureRequest request,
        CancellationToken cancellationToken = default) =>
        await ExecuteStoredProcedureAsync("Core.AddMeter", request, cancellationToken);

    public async Task<StoredProcedureExecutionResult> AddMeterAsync(
        int customerId,
        int branchId,
        int? areaId,
        int meterTypeId,
        DateOnly installationDate,
        decimal installationReading,
        string? notes = null,
        int? createdBy = null,
        CancellationToken cancellationToken = default)
    {
        var request = new StoredProcedureRequest
        {
            Parameters =
            [
                StoredProcedureParameter.Input("@CustomerId", SqlDbType.Int, customerId),
                StoredProcedureParameter.Input("@BranchId", SqlDbType.Int, branchId),
                StoredProcedureParameter.Input("@AreaId", SqlDbType.Int, areaId),
                StoredProcedureParameter.Input("@MeterTypeId", SqlDbType.Int, meterTypeId),
                StoredProcedureParameter.Input("@InstallationDate", SqlDbType.Date, installationDate.ToDateTime(TimeOnly.MinValue)),
                StoredProcedureParameter.Input("@InstallationReading", SqlDbType.Decimal, installationReading, precision: 18, scale: 3),
                StoredProcedureParameter.Input("@Notes", SqlDbType.NVarChar, notes, size: 4000),
                StoredProcedureParameter.Input("@CreatedBy", SqlDbType.Int, createdBy),
                StoredProcedureParameter.Output("@MeterId", SqlDbType.Int),
                StoredProcedureParameter.Output("@MeterNumber", SqlDbType.BigInt)
            ]
        };

        return await ExecuteStoredProcedureAsync("Core.AddMeter", request, cancellationToken);
    }

    public async Task<StoredProcedureExecutionResult> CreateMeterReadingAsync(
        StoredProcedureRequest request,
        CancellationToken cancellationToken = default) =>
        await ExecuteStoredProcedureAsync("Core.CreateMeterReading", request, cancellationToken);

    public async Task<StoredProcedureExecutionResult> CreateMeterReadingAsync(
        int meterId,
        DateOnly readingDate,
        decimal readingValue,
        string? notes = null,
        int? createdBy = null,
        CancellationToken cancellationToken = default)
    {
        var request = new StoredProcedureRequest
        {
            Parameters =
            [
                StoredProcedureParameter.Input("@MeterId", SqlDbType.Int, meterId),
                StoredProcedureParameter.Input("@ReadingDate", SqlDbType.Date, readingDate.ToDateTime(TimeOnly.MinValue)),
                StoredProcedureParameter.Input("@ReadingValue", SqlDbType.Decimal, readingValue, precision: 18, scale: 3),
                StoredProcedureParameter.Input("@Notes", SqlDbType.NVarChar, notes, size: 2000),
                StoredProcedureParameter.Input("@CreatedBy", SqlDbType.Int, createdBy)
            ]
        };

        return await ExecuteStoredProcedureAsync("Core.CreateMeterReading", request, cancellationToken);
    }

    public async Task<IReadOnlyList<MeterReading>> GetMeterReadingsAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(MeterReadingsSql, connection);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var readings = new List<MeterReading>();

        while (await reader.ReadAsync(cancellationToken))
        {
            readings.Add(ModelMappers.ToMeterReading(reader));
        }

        return readings;
    }

    private async Task<IReadOnlyList<Meter>> ReadMetersAsync(string commandText, int? customerId, Func<SqlDataReader, Meter> mapper, CancellationToken cancellationToken)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(commandText, connection);

        if (customerId is not null)
        {
            command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId.Value;
        }

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var meters = new List<Meter>();

        while (await reader.ReadAsync(cancellationToken))
        {
            meters.Add(mapper(reader));
        }

        return meters;
    }
}

