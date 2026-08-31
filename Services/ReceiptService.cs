using System.Data;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Models;

namespace WaterStation.Services;

public sealed class ReceiptService : ServiceBase
{
    private const string ReceiptColumns = "[ReceiptId], [ReceiptNumber], [PaymentId], [InvoiceId], [InvoiceNumber], [BillingYear], [BillingMonth], [CustomerId], [CustomerNumber], [FullName], [Phone], [Address], [MeterId], [MeterNumber], [ReceiptDate], [PaymentDate], [PaymentAmount], [PaymentMethodCode], [PaymentMethodName], [ReferenceNumber], [TotalAmount], [PaidAmount], [BalanceAmount], [Status], [StatusName], [IsReversed], [PaymentReversalId], [ReversalDate], [ReversedAmount], [ReversalReason], [CreatedAt], [CreatedBy], [Notes]";
    private const string ReceiptByPaymentIdSql = $"SELECT {ReceiptColumns} FROM [Billing].[vw_Receipt] WHERE [PaymentId] = @PaymentId;";
    private const string ReceiptByReceiptIdSql = $"SELECT {ReceiptColumns} FROM [Billing].[vw_Receipt] WHERE [ReceiptId] = @ReceiptId;";
    private const string ReceiptsByCustomerIdSql = $"SELECT {ReceiptColumns} FROM [Billing].[vw_Receipt] WHERE [CustomerId] = @CustomerId ORDER BY [ReceiptDate] DESC, [ReceiptId] DESC;";
    private const string PaymentReceiptSql = $"SELECT {ReceiptColumns} FROM [Billing].[vw_PaymentReceipt] WHERE [PaymentId] = @PaymentId;";
    private const string RecentReceiptsSql = $"SELECT TOP (@TopN) {ReceiptColumns} FROM [Billing].[vw_Receipt] ORDER BY [ReceiptDate] DESC, [ReceiptId] DESC;";
    private const string AllReceiptsSql = $"SELECT {ReceiptColumns} FROM [Billing].[vw_Receipt] ORDER BY [ReceiptDate] DESC, [ReceiptId] DESC;";

    public ReceiptService(Database database) : base(database)
    {
    }

    public async Task<Receipt?> GetReceiptByPaymentIdAsync(long paymentId, CancellationToken cancellationToken = default) =>
        await GetSingleReceiptAsync(ReceiptByPaymentIdSql, "@PaymentId", SqlDbType.BigInt, paymentId, cancellationToken);

    public async Task<Receipt?> GetReceiptByReceiptIdAsync(long receiptId, CancellationToken cancellationToken = default) =>
        await GetSingleReceiptAsync(ReceiptByReceiptIdSql, "@ReceiptId", SqlDbType.BigInt, receiptId, cancellationToken);

    public async Task<Receipt?> GetPaymentReceiptAsync(long paymentId, CancellationToken cancellationToken = default) =>
        await GetSingleReceiptAsync(PaymentReceiptSql, "@PaymentId", SqlDbType.BigInt, paymentId, cancellationToken);

    public async Task<IReadOnlyList<Receipt>> GetReceiptsByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(ReceiptsByCustomerIdSql, connection);
        command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var receipts = new List<Receipt>();

        while (await reader.ReadAsync(cancellationToken))
        {
            receipts.Add(ModelMappers.ToReceipt(reader));
        }

        return receipts;
    }

    public async Task<IReadOnlyList<Receipt>> GetRecentReceiptsAsync(int topN = 200, CancellationToken cancellationToken = default)
    {
        var count = Math.Clamp(topN, 1, 1000);

        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(RecentReceiptsSql, connection);
        command.Parameters.Add("@TopN", SqlDbType.Int).Value = count;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var receipts = new List<Receipt>();

        while (await reader.ReadAsync(cancellationToken))
        {
            receipts.Add(ModelMappers.ToReceipt(reader));
        }

        return receipts;
    }

    public async Task<IReadOnlyList<Receipt>> GetReceiptsAsync(CancellationToken cancellationToken = default) =>
        await ReadReceiptsAsync(AllReceiptsSql, cancellationToken);

    private async Task<IReadOnlyList<Receipt>> ReadReceiptsAsync(string commandText, CancellationToken cancellationToken)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(commandText, connection);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var receipts = new List<Receipt>();

        while (await reader.ReadAsync(cancellationToken))
        {
            receipts.Add(ModelMappers.ToReceipt(reader));
        }

        return receipts;
    }

    private async Task<Receipt?> GetSingleReceiptAsync(
        string commandText,
        string parameterName,
        SqlDbType dbType,
        object value,
        CancellationToken cancellationToken)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(commandText, connection);
        command.Parameters.Add(parameterName, dbType).Value = value;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? ModelMappers.ToReceipt(reader) : null;
    }
}

