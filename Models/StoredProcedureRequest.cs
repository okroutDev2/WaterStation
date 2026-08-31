using System.Data;

namespace WaterStation.Models;

/// <summary>
/// Describes values supplied to an existing stored procedure without embedding SQL text.
/// </summary>
public sealed class StoredProcedureRequest
{
    public IReadOnlyCollection<StoredProcedureParameter> Parameters { get; init; } = Array.Empty<StoredProcedureParameter>();
}

public sealed class StoredProcedureParameter
{
    public required string Name { get; init; }
    public required SqlDbType DbType { get; init; }
    public object? Value { get; init; }
    public ParameterDirection Direction { get; init; } = ParameterDirection.Input;
    public int? Size { get; init; }
    public byte? Precision { get; init; }
    public byte? Scale { get; init; }

    public static StoredProcedureParameter Input(string name, SqlDbType dbType, object? value, int? size = null, byte? precision = null, byte? scale = null) => new()
    {
        Name = name,
        DbType = dbType,
        Value = value,
        Direction = ParameterDirection.Input,
        Size = size,
        Precision = precision,
        Scale = scale
    };

    public static StoredProcedureParameter Output(string name, SqlDbType dbType, int? size = null, byte? precision = null, byte? scale = null) => new()
    {
        Name = name,
        DbType = dbType,
        Direction = ParameterDirection.Output,
        Size = size,
        Precision = precision,
        Scale = scale
    };
}

public sealed class StoredProcedureExecutionResult
{
    public int RowsAffected { get; init; }
    public IReadOnlyDictionary<string, object?> OutputValues { get; init; } = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

    public T? GetOutputValue<T>(string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parameterName);

        if (OutputValues.TryGetValue(parameterName, out var value) && value is not null and not DBNull)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        var normalizedKey = parameterName.StartsWith('@') ? parameterName[1..] : "@" + parameterName;
        if (OutputValues.TryGetValue(normalizedKey, out var altValue) && altValue is not null and not DBNull)
        {
            return (T)Convert.ChangeType(altValue, typeof(T));
        }

        return default;
    }
}

