using System.Data;
using Microsoft.Data.SqlClient;
using WaterStation.Data;
using WaterStation.Models;

namespace WaterStation.Services;

public abstract class ServiceBase
{
    protected ServiceBase(Database database)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
    }

    protected Database Database { get; }

    protected static void AddParameters(SqlCommand command, IEnumerable<StoredProcedureParameter> parameters)
    {
        foreach (var definition in parameters)
        {
            var parameter = command.Parameters.Add(definition.Name, definition.DbType);
            parameter.Direction = definition.Direction;
            parameter.Value = definition.Value ?? DBNull.Value;

            if (definition.Size.HasValue)
            {
                parameter.Size = definition.Size.Value;
            }

            if (definition.Precision.HasValue)
            {
                parameter.Precision = definition.Precision.Value;
            }

            if (definition.Scale.HasValue)
            {
                parameter.Scale = definition.Scale.Value;
            }
        }
    }

    protected async Task<StoredProcedureExecutionResult> ExecuteStoredProcedureAsync(
        string storedProcedureName,
        StoredProcedureRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storedProcedureName);
        ArgumentNullException.ThrowIfNull(request);

        using var connection = await Database.OpenConnectionAsync(cancellationToken);
        using var command = Database.CreateStoredProcedureCommand(connection, storedProcedureName);
        AddParameters(command, request.Parameters);

        var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);
        var outputValues = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

        foreach (SqlParameter parameter in command.Parameters)
        {
            if (parameter.Direction is ParameterDirection.Output or ParameterDirection.InputOutput or ParameterDirection.ReturnValue)
            {
                var val = parameter.Value is DBNull ? null : parameter.Value;
                outputValues[parameter.ParameterName] = val;
                var trimmed = parameter.ParameterName.TrimStart('@');
                outputValues[trimmed] = val;
            }
        }

        return new StoredProcedureExecutionResult
        {
            RowsAffected = rowsAffected,
            OutputValues = outputValues
        };
    }
}

