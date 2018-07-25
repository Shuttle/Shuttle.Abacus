using System;

namespace Shuttle.Abacus
{
    public class DataTypeFactory : IDataTypeFactory
    {
        public DataType Create(string name, string value)
        {
            switch (name.ToLowerInvariant())
            {
                case "boolean":
                {
                    return new BooleanDataType(value);
                }
                case "datetime":
                {
                    return new DateTimeDataType(value);
                }
                case "decimal":
                {
                    return new DecimalDataType(value);
                }
                case "integer":
                {
                    return new IntegerDataType(value);
                }
                case "text":
                {
                    return new TextDataType(value);
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}