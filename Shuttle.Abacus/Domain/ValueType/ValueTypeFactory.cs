using System;

namespace Shuttle.Abacus.Domain
{
    public class ValueTypeFactory : IValueTypeFactory
    {
        public ValueType Create(string type, string value)
        {
            switch (type.ToLowerInvariant())
            {
                case "boolean":
                {
                    return new BooleanValueType(value);
                }
                case "datetime":
                {
                    return new DateTimeValueType(value);
                }
                case "decimal":
                {
                    return new ConstantValueType(value);
                }
                case "integer":
                {
                    return new IntegerValueType(value);
                }
                case "text":
                {
                    return new TextValueType(value);
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}