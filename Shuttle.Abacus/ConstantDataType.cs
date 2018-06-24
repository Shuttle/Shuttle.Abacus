using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ConstantDataType : DataType
    {
        private readonly decimal _value;

        public ConstantDataType(decimal value)
        {
            _value = value;

            ValueString = Convert.ToString(value);
            Value = value;
        }

        public ConstantDataType(string text)
        {
            ValueString = text;

            _value = decimal.Parse(text);

            Value = _value;
        }

        public override string Name => "Constant";

        public override int CompareTo(DataType other)
        {
            Guard.AgainstNull(other, nameof(other));

            decimal otherValue;

            if (!decimal.TryParse(other.ValueString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleDataTypes, GetType().Name,
                    other.GetType().Name));
            }

            return _value.CompareTo(otherValue);
        }

        public override string Text()
        {
            return _value.ToString("N");
        }
    }
}