using System;

namespace Shuttle.Abacus
{
    public class DecimalValueSource : IValueSource, IValueSelectionHolder
    {
        private readonly decimal value;

        public DecimalValueSource(decimal value)
        {
            this.value = value;
        }

        public string ValueSelection
        {
            get { return Convert.ToString(value); }
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return value;
        }

        public string Name
        {
            get { return "Decimal"; }
        }

        public object Text
        {
            get { return Convert.ToString(value); }
        }

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
