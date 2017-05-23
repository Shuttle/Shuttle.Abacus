using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class ConstantValueType : ValueType
    {
        private readonly decimal value;

        public ConstantValueType(string argumentName, decimal value) : base(argumentName)
        {
            this.value = value;

            ValueString = Convert.ToString(value);
            Value = value;
        }

        public ConstantValueType(string argumentName, string text) : base(argumentName)
        {
            ValueString = text;

            value = decimal.Parse(text);

            Value = value;
        }

        public override string AnswerType => "Constant";

        public override int CompareTo(ValueType other)
        {
            Guard.AgainstNull(other, "other");

            decimal otherValue;

            if (!decimal.TryParse(other.ValueString, out otherValue))
            {
                throw new InvalidCastException(string.Format(Resources.IncompatibleCalculationValues, GetType().Name,
                                                             other.GetType().Name));
            }

            return value.CompareTo(otherValue);
        }

        public override string DisplayString()
        {
            return value.ToString("N");
        }
    }
}