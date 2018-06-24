using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class BooleanDataType : DataType
    {
        private readonly bool value;

        public BooleanDataType(bool value)
        {
            Value = value;
            ValueString = Convert.ToString(value);
        }

        public BooleanDataType(string text)
        {
            ValueString = text;

            switch (text.ToLower())
            {
                case "1":
                case "yes":
                case "true":
                case "y":
                case "t":
                {
                    value = true;
                    break;
                }
                default:
                {
                    value = false;
                    break;
                }
            }

            Value = value;
        }

        public override string Name => "Boolean";

        public override int CompareTo(DataType other)
        {
            Guard.AgainstNull(other, nameof(other));

            bool otherValue;

            if (!bool.TryParse(other.ValueString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleDataTypes, GetType().Name,
                    other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string Text()
        {
            return value ? "Yes" : "No";
        }
    }
}