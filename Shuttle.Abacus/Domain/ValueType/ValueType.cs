using System;

namespace Shuttle.Abacus.Domain
{
    public abstract class ValueType : IComparable<ValueType>
    {
        public static ValueType Null = new NullValueType();

        protected ValueType(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; protected set; }
        public string ValueString { get; protected set; }
        public object Value { get; protected set; }

        public abstract string AnswerType { get; }

        public virtual bool IsNull => false;

        public abstract int CompareTo(ValueType other);

        public abstract string DisplayString();

        public virtual string Description()
        {
            return ValueString == Convert.ToString(Value)
                       ? ValueString
                       : string.Format("{0} ({1})", ValueString, Convert.ToString(Value));
        }

        public static ValueType Create(string type, string name, string value)
        {
            switch (type.ToLowerInvariant())
            {
                case "boolean":
                    {
                        return new BooleanValueType(name, value);
                    }
                case "datetime":
                    {
                        return new DateTimeValueType(name, value);
                    }
                case "decimal":
                    {
                        return new ConstantValueType(name, value);
                    }
                case "integer":
                    {
                        return new IntegerValueType(name, value);
                    }
                case "text":
                    {
                        return new TextValueType(name, value);
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
        }
    }
}