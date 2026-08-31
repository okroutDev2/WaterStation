using Microsoft.Data.SqlClient;
using WaterStation.Models;

namespace WaterStation.Services;

internal static class ModelMappers
{
    public static Customer ToCustomer(SqlDataReader reader) => new()
    {
        CustomerId = reader.GetInt32OrDefault("CustomerId"),
        CustomerNumber = reader.GetStringOrEmpty("CustomerNumber"),
        FullName = reader.GetStringOrEmpty("FullName"),
        Phone = reader.GetNullableString("Phone"),
        Address = reader.GetNullableString("Address"),
        FamilyMembersCount = reader.GetNullableInt32("FamilyMembersCount"),
        Status = reader.HasColumn("CustomerStatus") ? reader.GetByteOrDefault("CustomerStatus") : reader.GetByteOrDefault("Status"),
        Notes = reader.GetNullableString("CustomerNotes") ?? reader.GetNullableString("Notes"),
        CreatedAt = reader.GetNullableDateTime("CreatedAt"),
        CreatedBy = reader.GetNullableInt32("CreatedBy"),
        UpdatedAt = reader.GetNullableDateTime("UpdatedAt"),
        UpdatedBy = reader.GetNullableInt32("UpdatedBy")
    };

    public static Meter ToMeter(SqlDataReader reader) => new()
    {
        MeterId = reader.GetInt32OrDefault("MeterId"),
        MeterNumber = reader.GetStringOrEmpty("MeterNumber"),
        CustomerId = reader.GetInt32OrDefault("CustomerId"),
        CustomerNumber = reader.GetNullableString("CustomerNumber"),
        FullName = reader.GetNullableString("FullName"),
        Phone = reader.GetNullableString("Phone"),
        Address = reader.GetNullableString("Address"),
        BranchId = reader.GetInt32OrDefault("BranchId"),
        BranchCode = reader.GetNullableString("BranchCode"),
        BranchName = reader.GetNullableString("BranchName"),
        AreaId = reader.GetNullableInt32("AreaId"),
        AreaCode = reader.GetNullableString("AreaCode"),
        AreaName = reader.GetNullableString("AreaName"),
        MeterTypeId = reader.GetInt32OrDefault("MeterTypeId"),
        MeterTypeCode = reader.GetNullableString("MeterTypeCode"),
        MeterTypeName = reader.GetNullableString("MeterTypeName"),
        ReadingDirection = reader.GetNullableByte("ReadingDirection"),
        ReadingDirectionName = reader.GetNullableString("ReadingDirectionName"),
        InstallationDate = reader.GetDateOnlyOrDefault("InstallationDate"),
        InstallationReading = reader.GetDecimalOrDefault("InstallationReading"),
        Status = reader.HasColumn("MeterStatus") ? reader.GetByteOrDefault("MeterStatus") : reader.GetByteOrDefault("Status"),
        RemovalDate = reader.GetNullableDateOnly("RemovalDate"),
        RemovalReading = reader.GetNullableDecimal("RemovalReading"),
        Notes = reader.GetNullableString("MeterNotes") ?? reader.GetNullableString("Notes"),
        CreatedAt = reader.HasColumn("MeterCreatedAt") ? reader.GetNullableDateTime("MeterCreatedAt") : reader.GetNullableDateTime("CreatedAt"),
        CreatedBy = reader.HasColumn("MeterCreatedBy") ? reader.GetNullableInt32("MeterCreatedBy") : reader.GetNullableInt32("CreatedBy"),
        UpdatedAt = reader.HasColumn("MeterUpdatedAt") ? reader.GetNullableDateTime("MeterUpdatedAt") : reader.GetNullableDateTime("UpdatedAt"),
        UpdatedBy = reader.HasColumn("MeterUpdatedBy") ? reader.GetNullableInt32("MeterUpdatedBy") : reader.GetNullableInt32("UpdatedBy"),
        LastReadingDate = reader.GetNullableDateOnly("LastReadingDate") ?? reader.GetNullableDateOnly("ReadingDate"),
        LastReadingValue = reader.GetNullableDecimal("LastReadingValue") ?? reader.GetNullableDecimal("ReadingValue"),
        LastConsumption = reader.GetNullableDecimal("LastConsumption") ?? reader.GetNullableDecimal("Consumption"),
        CumulativeConsumption = reader.GetNullableDecimal("CumulativeConsumption"),
        LastIsReverseMeter = reader.GetNullableBoolean("LastIsReverseMeter") ?? reader.GetNullableBoolean("IsReverseMeter")
    };

    public static Meter ToMeterFromCustomerMeters(SqlDataReader reader) => new()
    {
        MeterId = reader.GetInt32OrDefault("MeterId"),
        MeterNumber = reader.GetStringOrEmpty("MeterNumber"),
        CustomerId = reader.GetInt32OrDefault("CustomerId"),
        CustomerNumber = reader.GetNullableString("CustomerNumber"),
        FullName = reader.GetNullableString("FullName"),
        Phone = reader.GetNullableString("Phone"),
        Address = reader.GetNullableString("Address"),
        BranchId = reader.GetInt32OrDefault("BranchId"),
        BranchCode = reader.GetNullableString("BranchCode"),
        BranchName = reader.GetNullableString("BranchName"),
        AreaId = reader.GetNullableInt32("AreaId"),
        AreaCode = reader.GetNullableString("AreaCode"),
        AreaName = reader.GetNullableString("AreaName"),
        MeterTypeId = reader.GetInt32OrDefault("MeterTypeId"),
        MeterTypeCode = reader.GetNullableString("MeterTypeCode"),
        MeterTypeName = reader.GetNullableString("MeterTypeName"),
        ReadingDirection = reader.GetNullableByte("ReadingDirection"),
        ReadingDirectionName = null,
        InstallationDate = reader.GetDateOnlyOrDefault("InstallationDate"),
        InstallationReading = reader.GetDecimalOrDefault("InstallationReading"),
        Status = reader.GetByteOrDefault("MeterStatus"),
        RemovalDate = reader.GetNullableDateOnly("RemovalDate"),
        RemovalReading = reader.GetNullableDecimal("RemovalReading"),
        Notes = reader.GetNullableString("MeterNotes"),
        CreatedAt = reader.GetNullableDateTime("MeterCreatedAt"),
        CreatedBy = reader.GetNullableInt32("MeterCreatedBy"),
        UpdatedAt = reader.GetNullableDateTime("MeterUpdatedAt"),
        UpdatedBy = reader.GetNullableInt32("MeterUpdatedBy")
    };

    public static Meter ToMeterFromActiveMeters(SqlDataReader reader) => new()
    {
        MeterId = reader.GetInt32OrDefault("MeterId"),
        MeterNumber = reader.GetStringOrEmpty("MeterNumber"),
        CustomerId = reader.GetInt32OrDefault("CustomerId"),
        CustomerNumber = reader.GetNullableString("CustomerNumber"),
        FullName = reader.GetNullableString("FullName"),
        Phone = reader.GetNullableString("Phone"),
        Address = reader.GetNullableString("Address"),
        BranchId = reader.GetInt32OrDefault("BranchId"),
        BranchCode = reader.GetNullableString("BranchCode"),
        BranchName = reader.GetNullableString("BranchName"),
        AreaId = reader.GetNullableInt32("AreaId"),
        AreaCode = reader.GetNullableString("AreaCode"),
        AreaName = reader.GetNullableString("AreaName"),
        MeterTypeId = reader.GetInt32OrDefault("MeterTypeId"),
        MeterTypeCode = reader.GetNullableString("MeterTypeCode"),
        MeterTypeName = reader.GetNullableString("MeterTypeName"),
        ReadingDirection = reader.GetNullableByte("ReadingDirection"),
        ReadingDirectionName = null,
        InstallationDate = reader.GetDateOnlyOrDefault("InstallationDate"),
        InstallationReading = reader.GetDecimalOrDefault("InstallationReading"),
        Status = reader.GetByteOrDefault("Status"),
        RemovalDate = null,
        RemovalReading = null,
        Notes = reader.GetNullableString("Notes"),
        CreatedAt = reader.GetNullableDateTime("CreatedAt"),
        CreatedBy = reader.GetNullableInt32("CreatedBy"),
        UpdatedAt = reader.GetNullableDateTime("UpdatedAt"),
        UpdatedBy = reader.GetNullableInt32("UpdatedBy")
    };

    public static MeterReading ToMeterReading(SqlDataReader reader) => new()
    {
        MeterReadingId = reader.GetInt64OrDefault("MeterReadingId"),
        MeterId = reader.GetInt32OrDefault("MeterId"),
        MeterNumber = reader.GetNullableString("MeterNumber") ?? string.Empty,
        ReadingDate = reader.GetDateOnlyOrDefault("ReadingDate"),
        ReadingValue = reader.GetDecimalOrDefault("ReadingValue"),
        PreviousReading = reader.GetNullableDecimal("PreviousReading"),
        Consumption = reader.GetNullableDecimal("Consumption"),
        Notes = reader.GetNullableString("Notes"),
        CreatedAt = reader.GetDateTimeOrDefault("CreatedAt"),
        CreatedBy = reader.GetNullableInt32("CreatedBy"),
        UpdatedAt = reader.GetNullableDateTime("UpdatedAt"),
        UpdatedBy = reader.GetNullableInt32("UpdatedBy")
    };

    public static Invoice ToInvoice(SqlDataReader reader) => new()
    {
        InvoiceId = reader.GetInt64OrDefault("InvoiceId"),
        InvoiceNumber = reader.GetStringOrEmpty("InvoiceNumber"),
        CustomerId = reader.GetInt32OrDefault("CustomerId"),
        CustomerNumber = reader.GetNullableString("CustomerNumber"),
        FullName = reader.GetNullableString("FullName"),
        Phone = reader.GetNullableString("Phone"),
        MeterId = reader.GetNullableInt32("MeterId"),
        MeterNumber = reader.GetNullableString("MeterNumber"),
        BillingYear = reader.GetNullableInt16("BillingYear"),
        BillingMonth = reader.GetNullableByte("BillingMonth"),
        InvoiceDate = reader.GetNullableDateOnly("InvoiceDate"),
        DueDate = reader.GetNullableDateOnly("DueDate"),
        PreviousReading = reader.GetNullableDecimal("PreviousReading"),
        CurrentReading = reader.GetNullableDecimal("CurrentReading"),
        UnitsConsumed = reader.GetNullableDecimal("UnitsConsumed"),
        WaterAmount = reader.GetNullableDecimal("WaterAmount"),
        SubscriptionAmount = reader.GetNullableDecimal("SubscriptionAmount"),
        PenaltyAmount = reader.GetNullableDecimal("PenaltyAmount"),
        ArrearsAmount = reader.GetNullableDecimal("ArrearsAmount"),
        TotalAmount = reader.GetDecimalOrDefault("TotalAmount"),
        PaidAmount = reader.GetDecimalOrDefault("PaidAmount"),
        BalanceAmount = reader.GetNullableDecimal("BalanceAmount") ?? reader.GetDecimalOrDefault("Balance"),
        TransferredAmount = reader.GetNullableDecimal("TransferredAmount"),
        OutstandingAmount = reader.GetNullableDecimal("OutstandingAmount"),
        IsTransferred = reader.GetNullableBoolean("IsTransferred"),
        Status = reader.GetByteOrDefault("Status"),
        StatusName = reader.GetNullableString("StatusName"),
        Notes = reader.GetNullableString("Notes")
    };

    public static InvoiceBalance ToInvoiceBalance(SqlDataReader reader) => new()
    {
        InvoiceId = reader.GetInt64OrDefault("InvoiceId"),
        InvoiceNumber = reader.GetStringOrEmpty("InvoiceNumber"),
        CustomerId = reader.GetInt32OrDefault("CustomerId"),
        MeterId = reader.GetInt32OrDefault("MeterId"),
        BillingYear = reader.GetNullableInt16("BillingYear"),
        BillingMonth = reader.GetNullableByte("BillingMonth"),
        TotalAmount = reader.GetDecimalOrDefault("TotalAmount"),
        PaidAmount = reader.GetDecimalOrDefault("PaidAmount"),
        BalanceAmount = reader.GetDecimalOrDefault("BalanceAmount"),
        TransferredAmount = reader.GetDecimalOrDefault("TransferredAmount"),
        TransferredByHistory = reader.GetDecimalOrDefault("TransferredByHistory"),
        OutstandingAmount = reader.GetDecimalOrDefault("OutstandingAmount"),
        IsTransferred = reader.GetBooleanOrDefault("IsTransferred"),
        Status = reader.GetByteOrDefault("Status")
    };

    public static Payment ToPayment(SqlDataReader reader) => new()
    {
        PaymentId = reader.GetInt64OrDefault("PaymentId"),
        ReceiptId = reader.GetNullableInt64("ReceiptId"),
        ReceiptNumber = reader.GetNullableString("ReceiptNumber"),
        InvoiceId = reader.GetInt64OrDefault("InvoiceId"),
        InvoiceNumber = reader.GetNullableString("InvoiceNumber"),
        CustomerId = reader.GetNullableInt32("CustomerId"),
        PaymentDate = reader.GetDateTimeOrDefault("PaymentDate"),
        Amount = reader.GetNullableDecimal("PaymentAmount") ?? reader.GetNullableDecimal("Amount") ?? reader.GetNullableDecimal("ReceiptAmount") ?? decimal.Zero,
        PaymentMethodId = reader.GetNullableInt32("PaymentMethodId"),
        PaymentMethodCode = reader.GetNullableString("PaymentMethodCode"),
        PaymentMethodName = reader.GetNullableString("PaymentMethodName"),
        ReferenceNumber = reader.GetNullableString("ReferenceNumber"),
        Notes = reader.GetNullableString("PaymentNotes") ?? reader.GetNullableString("Notes"),
        TotalAmount = reader.GetNullableDecimal("TotalAmount"),
        PaidAmount = reader.GetNullableDecimal("PaidAmount"),
        BalanceAmount = reader.GetNullableDecimal("BalanceAmount"),
        Status = reader.GetNullableByte("Status"),
        StatusName = reader.GetNullableString("StatusName"),
        IsReversed = reader.GetBooleanOrDefault("IsReversed"),
        PaymentReversalId = reader.GetNullableInt64("PaymentReversalId"),
        ReversalDate = reader.GetNullableDateTime("ReversalDate"),
        ReversalReason = reader.GetNullableString("ReversalReason") ?? reader.GetNullableString("Reason")
    };

    public static Receipt ToReceipt(SqlDataReader reader) => new()
    {
        ReceiptId = reader.GetInt64OrDefault("ReceiptId") != 0L ? reader.GetInt64OrDefault("ReceiptId") : reader.GetInt64OrDefault("PaymentId"),
        ReceiptNumber = reader.GetStringOrEmpty("ReceiptNumber"),
        PaymentId = reader.GetInt64OrDefault("PaymentId"),
        InvoiceId = reader.GetNullableInt64("InvoiceId"),
        InvoiceNumber = reader.GetNullableString("InvoiceNumber"),
        BillingYear = reader.GetNullableInt16("BillingYear"),
        BillingMonth = reader.GetNullableByte("BillingMonth"),
        CustomerId = reader.GetInt32OrDefault("CustomerId"),
        CustomerNumber = reader.GetNullableString("CustomerNumber"),
        FullName = reader.GetNullableString("FullName"),
        Phone = reader.GetNullableString("Phone"),
        Address = reader.GetNullableString("Address"),
        MeterId = reader.GetNullableInt32("MeterId"),
        MeterNumber = reader.GetNullableString("MeterNumber"),
        ReceiptDate = reader.GetNullableDateTime("ReceiptDate") ?? reader.GetNullableDateTime("PaymentDate") ?? reader.GetDateTimeOrDefault("CreatedAt"),
        PaymentDate = reader.GetNullableDateTime("PaymentDate"),
        Amount = reader.GetNullableDecimal("PaymentAmount") ?? reader.GetNullableDecimal("Amount") ?? reader.GetNullableDecimal("ReceiptAmount") ?? decimal.Zero,
        PaymentMethodCode = reader.GetNullableString("PaymentMethodCode"),
        PaymentMethodName = reader.GetNullableString("PaymentMethodName"),
        ReferenceNumber = reader.GetNullableString("ReferenceNumber"),
        TotalAmount = reader.GetNullableDecimal("TotalAmount"),
        PaidAmount = reader.GetNullableDecimal("PaidAmount"),
        BalanceAmount = reader.GetNullableDecimal("BalanceAmount"),
        Status = reader.GetNullableByte("Status"),
        StatusName = reader.GetNullableString("StatusName"),
        IsReversed = reader.GetBooleanOrDefault("IsReversed"),
        PaymentReversalId = reader.GetNullableInt64("PaymentReversalId"),
        ReversalDate = reader.GetNullableDateTime("ReversalDate"),
        ReversedAmount = reader.GetNullableDecimal("ReversedAmount"),
        ReversalReason = reader.GetNullableString("ReversalReason") ?? reader.GetNullableString("Reason"),
        CreatedAt = reader.GetNullableDateTime("CreatedAt"),
        CreatedBy = reader.GetNullableInt32("CreatedBy"),
        Notes = reader.GetNullableString("Notes")
    };

    public static PaymentReversal ToPaymentReversal(SqlDataReader reader) => new()
    {
        PaymentReversalId = reader.GetInt64OrDefault("PaymentReversalId"),
        PaymentId = reader.GetInt64OrDefault("PaymentId"),
        InvoiceId = reader.GetNullableInt64("InvoiceId"),
        InvoiceNumber = reader.GetNullableString("InvoiceNumber"),
        ReversalDate = reader.GetDateTimeOrDefault("ReversalDate"),
        Amount = reader.GetNullableDecimal("ReversedAmount") ?? reader.GetNullableDecimal("Amount") ?? reader.GetNullableDecimal("PaymentAmount"),
        Reason = reader.GetNullableString("ReversalReason") ?? reader.GetStringOrEmpty("Reason"),
        CreatedAt = reader.GetNullableDateTime("CreatedAt"),
        CreatedBy = reader.GetNullableInt32("CreatedBy"),
        ReversedBy = reader.GetNullableInt32("ReversedBy"),
        TotalAmount = reader.GetNullableDecimal("TotalAmount"),
        PaidAmount = reader.GetNullableDecimal("PaidAmount"),
        BalanceAmount = reader.GetNullableDecimal("BalanceAmount"),
        Status = reader.GetNullableByte("Status"),
        StatusName = reader.GetNullableString("StatusName")
    };
}

