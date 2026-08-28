namespace Shuttle.Abacus;

public class DataTypeFactory : IDataTypeFactory
{
    public DataType Create(string name, string value)
    {
        return name.ToLowerInvariant() switch
        {
            "boolean" => new BooleanDataType(value),
            "datetime" => new DateTimeDataType(value),
            "decimal" => new DecimalDataType(value),
            "integer" => new IntegerDataType(value),
            "text" => new TextDataType(value),
            _ => throw new InvalidOperationException($"Unknown data type '{name}'.")
        };
    }
}
