using System.Data;
using Microsoft.Data.SqlClient;

namespace WaterStation.Services;

internal static class SqlDataReaderExtensions
{
    public static bool HasColumn(this SqlDataReader reader, string columnName)
    {
        for (var index = 0; index < reader.FieldCount; index++)
        {
            if (string.Equals(reader.GetName(index), columnName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public static string GetStringOrEmpty(this SqlDataReader reader, string columnName) =>
        reader.GetNullableString(columnName) ?? string.Empty;

    public static string? GetNullableString(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToString(value) : null;

    public static short GetInt16OrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableInt16(columnName) ?? 0;

    public static short? GetNullableInt16(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToInt16(value) : null;

    public static int GetInt32OrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableInt32(columnName) ?? 0;

    public static int? GetNullableInt32(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToInt32(value) : null;

    public static long GetInt64OrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableInt64(columnName) ?? 0L;

    public static long? GetNullableInt64(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToInt64(value) : null;

    public static decimal GetDecimalOrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableDecimal(columnName) ?? decimal.Zero;

    public static decimal? GetNullableDecimal(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToDecimal(value) : null;

    public static byte GetByteOrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableByte(columnName) ?? (byte)0;

    public static byte? GetNullableByte(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToByte(value) : null;

    public static bool GetBooleanOrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableBoolean(columnName) ?? false;

    public static bool? GetNullableBoolean(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToBoolean(value) : null;

    public static DateTime GetDateTimeOrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableDateTime(columnName) ?? default;

    public static DateTime? GetNullableDateTime(this SqlDataReader reader, string columnName) =>
        reader.TryGetValue(columnName, out var value) && value is not null ? Convert.ToDateTime(value) : null;

    public static DateOnly GetDateOnlyOrDefault(this SqlDataReader reader, string columnName) =>
        reader.GetNullableDateOnly(columnName) ?? default;

    public static DateOnly? GetNullableDateOnly(this SqlDataReader reader, string columnName)
    {
        if (reader.TryGetValue(columnName, out var value) && value is not null)
        {
            if (value is DateOnly dateOnly)
            {
                return dateOnly;
            }

            if (value is DateTime dateTime)
            {
                return DateOnly.FromDateTime(dateTime);
            }

            if (DateOnly.TryParse(Convert.ToString(value), out var parsedDate))
            {
                return parsedDate;
            }
        }

        return null;
    }

    public static bool TryGetValue(this SqlDataReader reader, string columnName, out object? value)
    {
        value = null;

        for (var index = 0; index < reader.FieldCount; index++)
        {
            if (string.Equals(reader.GetName(index), columnName, StringComparison.OrdinalIgnoreCase))
            {
                if (!reader.IsDBNull(index))
                {
                    value = reader.GetValue(index);
                    return true;
                }

                return false;
            }
        }

        return false;
    }
}

