using System;

namespace Shuttle.Abacus.Domain
{
    public class DecimalValueSource : IValueSource, IValueSelectionHolder
    {
        private readonly decimal value;

        public DecimalValueSource(decimal value)
        {
            this.value = value;
        }

        public string ValueSelection => Convert.ToString(value);

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return value;
        }

        public string Name => "Decimal";

        public object Text => Convert.ToString(value);

        public IValueSource Copy()
        {
            return new DecimalValueSource(value);
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (decimal)", value);
        }
    }
}
