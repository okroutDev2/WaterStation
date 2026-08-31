using System.Data;
using WaterStation.Data;
using WaterStation.Models;

namespace WaterStation.Services;

public sealed class PaymentService : ServiceBase
{
    public PaymentService(Database database) : base(database)
    {
    }

    public async Task<StoredProcedureExecutionResult> PayInvoiceAsync(
        StoredProcedureRequest request,
        CancellationToken cancellationToken = default) =>
        await ExecuteStoredProcedureAsync("Billing.PayInvoice", request, cancellationToken);

    public async Task<StoredProcedureExecutionResult> PayInvoiceAsync(
        long invoiceId,
        decimal amount,
        int paymentMethodId,
        DateTime paymentDate,
        string? referenceNumber = null,
        string? notes = null,
        int? createdBy = null,
        CancellationToken cancellationToken = default)
    {
        var request = new StoredProcedureRequest
        {
            Parameters =
            [
                StoredProcedureParameter.Input("@InvoiceId", SqlDbType.BigInt, invoiceId),
                StoredProcedureParameter.Input("@Amount", SqlDbType.Decimal, amount, precision: 18, scale: 2),
                StoredProcedureParameter.Input("@PaymentMethodId", SqlDbType.Int, paymentMethodId),
                StoredProcedureParameter.Input("@PaymentDate", SqlDbType.DateTime2, paymentDate),
                StoredProcedureParameter.Input("@ReferenceNumber", SqlDbType.NVarChar, referenceNumber, size: 100),
                StoredProcedureParameter.Input("@Notes", SqlDbType.NVarChar, notes, size: 1000),
                StoredProcedureParameter.Input("@CreatedBy", SqlDbType.Int, createdBy)
            ]
        };

        return await ExecuteStoredProcedureAsync("Billing.PayInvoice", request, cancellationToken);
    }

    public async Task<StoredProcedureExecutionResult> ReversePaymentAsync(
        StoredProcedureRequest request,
        CancellationToken cancellationToken = default) =>
        await ExecuteStoredProcedureAsync("Billing.ReversePayment", request, cancellationToken);

    public async Task<StoredProcedureExecutionResult> ReversePaymentAsync(
        long paymentId,
        string reason,
        int? createdBy = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        var request = new StoredProcedureRequest
        {
            Parameters =
            [
                StoredProcedureParameter.Input("@PaymentId", SqlDbType.BigInt, paymentId),
                StoredProcedureParameter.Input("@Reason", SqlDbType.NVarChar, reason, size: 1000),
                StoredProcedureParameter.Input("@CreatedBy", SqlDbType.Int, createdBy)
            ]
        };

        return await ExecuteStoredProcedureAsync("Billing.ReversePayment", request, cancellationToken);
    }
}

