using System;

namespace Shuttle.Abacus
{
    public abstract class DataType : IComparable<DataType>
    {
        public static DataType Null = new NullDataType();

        public string ValueString { get; protected set; }
        public object Value { get; protected set; }

        public abstract string Name { get; }

        public virtual bool IsNull => false;

        public abstract int CompareTo(DataType other);

        public abstract string Text();

        public virtual string Description()
        {
            return ValueString == Convert.ToString(Value)
                ? ValueString
                : $"{ValueString} ({Convert.ToString(Value)})";
        }
    }
}