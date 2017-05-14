using System;

namespace Shuttle.Abacus.Domain
{
    public class ConstantValueSource : IValueSource, IValueSelectionHolder
    {
        private readonly decimal value;

        public ConstantValueSource(decimal value)
        {
            this.value = value;
        }

        public string ValueSelection => Convert.ToString(value);

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return value;
        }

        public string Name => "Constant";

        public object Text => Convert.ToString(value);

        public IValueSource Copy()
        {
            return new ConstantValueSource(value);
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (decimal)", value);
        }
    }
}
