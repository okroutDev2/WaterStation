using System.Data;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Models;

namespace WaterStation.Services;

public sealed class CustomerService : ServiceBase
{
    private const string CustomerColumns = "[CustomerId], [CustomerNumber], [FullName], [Phone], [Address], [FamilyMembersCount], [CustomerStatus], [CustomerNotes]";
    private const string CustomerByIdSql = $"SELECT TOP (1) {CustomerColumns} FROM [Core].[vw_CustomerMeters] WHERE [CustomerId] = @CustomerId ORDER BY [CustomerId], [MeterId];";
    private const string CustomerByNumberSql = $"SELECT TOP (1) {CustomerColumns} FROM [Core].[vw_CustomerMeters] WHERE [CustomerNumber] = @CustomerNumber ORDER BY [CustomerId], [MeterId];";

    private const string CustomerSearchColumns =
        "[CustomerId], [CustomerNumber], [FullName], [Phone], [Address], [FamilyMembersCount], [CustomerStatus], [CustomerNotes]";
    private const string SearchCustomersSql = $"""
        SELECT DISTINCT {CustomerSearchColumns}
        FROM [Core].[vw_CustomerMeters]
        WHERE (@CustomerNumber IS NULL OR [CustomerNumber] LIKE @CustomerNumberPattern)
          AND (@Name IS NULL OR [FullName] LIKE @NamePattern)
        ORDER BY [FullName], [CustomerNumber];
        """;

    private const string CreateCustomerSql = """
        INSERT INTO [Core].[Customers]
        (
            [CustomerNumber],
            [FullName],
            [Phone],
            [Address],
            [FamilyMembersCount],
            [Status],
            [Notes],
            [CreatedBy]
        )
        VALUES
        (
            @CustomerNumber,
            @FullName,
            @Phone,
            @Address,
            @FamilyMembersCount,
            @Status,
            @Notes,
            @CreatedBy
        );
        SELECT CAST(SCOPE_IDENTITY() AS INT) AS CustomerId;
        """;

    public CustomerService(Database database) : base(database)
    {
    }

    public async Task<IReadOnlyList<Customer>> SearchCustomersAsync(
        string? customerNumber,
        string? name,
        CancellationToken cancellationToken = default)
    {
        var number = Normalize(customerNumber);
        var nameFilter = Normalize(name);

        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(SearchCustomersSql, connection);

        command.Parameters.Add("@CustomerNumber", SqlDbType.NVarChar, 60).Value = (object?)number ?? DBNull.Value;
        command.Parameters.Add("@CustomerNumberPattern", SqlDbType.NVarChar, 122).Value = (object?)EscapedLikePattern(number) ?? DBNull.Value;
        command.Parameters.Add("@Name", SqlDbType.NVarChar, 500).Value = (object?)nameFilter ?? DBNull.Value;
        command.Parameters.Add("@NamePattern", SqlDbType.NVarChar, 1002).Value = (object?)EscapedLikePattern(nameFilter) ?? DBNull.Value;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var customers = new List<Customer>();

        while (await reader.ReadAsync(cancellationToken))
        {
            customers.Add(ModelMappers.ToCustomer(reader));
        }

        return customers;
    }

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static string? EscapedLikePattern(string? value)
    {
        if (value is null)
        {
            return null;
        }

        var escaped = value
            .Replace("[", "[[]", StringComparison.Ordinal)
            .Replace("%", "[%]", StringComparison.Ordinal)
            .Replace("_", "[_]", StringComparison.Ordinal);

        return "%" + escaped + "%";
    }

    public async Task<Customer?> GetByIdAsync(int customerId, CancellationToken cancellationToken = default)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(CustomerByIdSql, connection);
        command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? ModelMappers.ToCustomer(reader) : null;
    }

    public async Task<Customer?> GetByCustomerNumberAsync(string customerNumber, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(customerNumber);

        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(CustomerByNumberSql, connection);
        command.Parameters.Add("@CustomerNumber", SqlDbType.NVarChar, 100).Value = customerNumber.Trim();

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? ModelMappers.ToCustomer(reader) : null;
    }

    public async Task<Customer> CreateAsync(
        string customerNumber,
        string fullName,
        byte status,
        string? phone = null,
        string? address = null,
        int? familyMembersCount = null,
        string? notes = null,
        int? createdBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(customerNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName);

        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(CreateCustomerSql, connection);
        command.Parameters.Add("@CustomerNumber", SqlDbType.NVarChar, 30).Value = customerNumber.Trim();
        command.Parameters.Add("@FullName", SqlDbType.NVarChar, 250).Value = fullName.Trim();
        command.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = (object?)phone ?? DBNull.Value;
        command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = (object?)address ?? DBNull.Value;
        command.Parameters.Add("@FamilyMembersCount", SqlDbType.Int).Value = (object?)familyMembersCount ?? DBNull.Value;
        command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = status;
        command.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = (object?)notes ?? DBNull.Value;
        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = (object?)createdBy ?? DBNull.Value;

        var customerId = (int)Convert.ChangeType(await command.ExecuteScalarAsync(cancellationToken), typeof(int));

        return new Customer
        {
            CustomerId = customerId,
            CustomerNumber = customerNumber.Trim(),
            FullName = fullName.Trim(),
            Phone = phone,
            Address = address,
            FamilyMembersCount = familyMembersCount,
            Status = status,
            Notes = notes,
            CreatedBy = createdBy
        };
    }
}

