using System.Data;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Models;

namespace WaterStation.Services;

public sealed class BillingService : ServiceBase
{
    private const string OpenInvoicesColumns = "[InvoiceId], [InvoiceNumber], [CustomerId], [CustomerNumber], [FullName], [Phone], [MeterId], [MeterNumber], [BillingYear], [BillingMonth], [PreviousReading], [CurrentReading], [UnitsConsumed], [WaterAmount], [SubscriptionAmount], [PenaltyAmount], [ArrearsAmount], [TotalAmount], [PaidAmount], [BalanceAmount], [Status], [StatusName], [InvoiceDate]";
    private const string InvoiceBalancesColumns = "[InvoiceId], [InvoiceNumber], [CustomerId], [MeterId], [BillingYear], [BillingMonth], [TotalAmount], [PaidAmount], [BalanceAmount], [TransferredAmount], [TransferredByHistory], [OutstandingAmount], [IsTransferred], [Status]";
    private const string OpenInvoicesSql = $"SELECT {OpenInvoicesColumns} FROM [Billing].[vw_OpenInvoices] ORDER BY [InvoiceDate] DESC, [InvoiceId] DESC;";
    private const string InvoiceBalancesSql = $"SELECT {InvoiceBalancesColumns} FROM [Billing].[vw_InvoiceBalances] ORDER BY [BillingYear] DESC, [BillingMonth] DESC, [InvoiceId] DESC;";

    public BillingService(Database database) : base(database)
    {
    }

    public async Task<StoredProcedureExecutionResult> CreateInvoiceAsync(
        StoredProcedureRequest request,
        CancellationToken cancellationToken = default) =>
        await ExecuteStoredProcedureAsync("Billing.CreateInvoice", request, cancellationToken);

    public async Task<StoredProcedureExecutionResult> CreateInvoiceAsync(
        int customerId,
        int meterId,
        short billingYear,
        byte billingMonth,
        int? createdBy = null,
        CancellationToken cancellationToken = default)
    {
        var request = new StoredProcedureRequest
        {
            Parameters =
            [
                StoredProcedureParameter.Input("@CustomerId", SqlDbType.Int, customerId),
                StoredProcedureParameter.Input("@MeterId", SqlDbType.Int, meterId),
                StoredProcedureParameter.Input("@BillingYear", SqlDbType.SmallInt, billingYear),
                StoredProcedureParameter.Input("@BillingMonth", SqlDbType.TinyInt, billingMonth),
                StoredProcedureParameter.Input("@CreatedBy", SqlDbType.Int, createdBy)
            ]
        };

        return await ExecuteStoredProcedureAsync("Billing.CreateInvoice", request, cancellationToken);
    }

    public async Task<IReadOnlyList<Invoice>> GetOpenInvoicesAsync(CancellationToken cancellationToken = default) =>
        await ReadInvoicesAsync(OpenInvoicesSql, cancellationToken);

    public async Task<IReadOnlyList<InvoiceBalance>> GetInvoiceBalancesAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(InvoiceBalancesSql, connection);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var balances = new List<InvoiceBalance>();

        while (await reader.ReadAsync(cancellationToken))
        {
            balances.Add(ModelMappers.ToInvoiceBalance(reader));
        }

        return balances;
    }

    public async Task<CustomerCollectionScreenResult> GetCustomerCollectionScreenAsync(
        int? customerId = null,
        string? customerNumber = null,
        string? meterNumber = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = Database.CreateStoredProcedureCommand(connection, "Billing.GetCustomerCollectionScreen");

        var customerIdParam = command.Parameters.Add("@CustomerId", SqlDbType.Int);
        customerIdParam.Value = customerId.HasValue ? customerId.Value : DBNull.Value;

        var customerNumberParam = command.Parameters.Add("@CustomerNumber", SqlDbType.NVarChar, 100);
        customerNumberParam.Value = string.IsNullOrWhiteSpace(customerNumber) ? DBNull.Value : customerNumber.Trim();

        var meterNumberParam = command.Parameters.Add("@MeterNumber", SqlDbType.NVarChar, 100);
        meterNumberParam.Value = string.IsNullOrWhiteSpace(meterNumber) ? DBNull.Value : meterNumber.Trim();

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        Customer? customer = null;
        var meters = new List<Meter>();
        var invoices = new List<Invoice>();
        var payments = new List<Payment>();

        // Result Set 1: Customer Info
        if (await reader.ReadAsync(cancellationToken))
        {
            customer = ModelMappers.ToCustomer(reader);
        }

        // Result Set 2: Customer Meters + Last Reading
        if (await reader.NextResultAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                meters.Add(ModelMappers.ToMeter(reader));
            }
        }

        // Result Set 3: Open Invoices
        if (await reader.NextResultAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                invoices.Add(ModelMappers.ToInvoice(reader));
            }
        }

        // Result Set 4: Payment History, Receipts, and Reversals
        if (await reader.NextResultAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                payments.Add(ModelMappers.ToPayment(reader));
            }
        }

        return new CustomerCollectionScreenResult
        {
            Customer = customer,
            Meters = meters,
            OpenInvoices = invoices,
            Payments = payments
        };
    }

    private async Task<IReadOnlyList<Invoice>> ReadInvoicesAsync(string commandText, CancellationToken cancellationToken)
    {
        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = new SqlCommand(commandText, connection);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var invoices = new List<Invoice>();

        while (await reader.ReadAsync(cancellationToken))
        {
            invoices.Add(ModelMappers.ToInvoice(reader));
        }

        return invoices;
    }
}

