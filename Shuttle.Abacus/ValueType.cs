using System;

namespace Shuttle.Abacus
{
    public abstract class ValueType : IComparable<ValueType>
    {
        public static ValueType Null = new NullValueType();

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
                : $"{ValueString} ({Convert.ToString(Value)})";
        }
    }
}