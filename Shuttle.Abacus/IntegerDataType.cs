using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class IntegerDataType : DataType
    {
        private readonly int _value;

        public IntegerDataType(string text)
        {
            ValueString = text;

            _value = int.Parse(text);

            Value = _value;
        }

        public IntegerDataType(int value)
        {
            _value = value;

            Value = value;
            ValueString = Convert.ToString(value);
        }

        public override string Name => "Integer";

        public override int CompareTo(DataType other)
        {
            Guard.AgainstNull(other, nameof(other));

            int otherValue;

            if (!int.TryParse(other.ValueString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleDataTypes, GetType().Name,
                    other.GetType().Name));
            }

            return _value.CompareTo(otherValue);
        }

        public override string Text()
        {
            return _value.ToString("#,##0");
        }
    }
}