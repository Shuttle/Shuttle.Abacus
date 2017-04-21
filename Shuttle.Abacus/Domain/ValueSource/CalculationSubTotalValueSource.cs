using System;

namespace Shuttle.Abacus.Domain
{
    public class CalculationSubTotalValueSource : IValueSource, ICalculationValueSource
    {
        private readonly Calculation calculation;
        private Guid calculationId;

        public CalculationSubTotalValueSource(Calculation calculation)
        {
            this.calculation = calculation;

            calculationId = calculation.Id;
        }

        public string ValueSelection
        {
            get { return calculationId.ToString("n"); }
        }

        public void AssignCalculationId(Guid id)
        {
            calculationId = id;
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return methodContext.GetSubTotal(calculation.Name).Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (from sub-total '{1}')", operand.ToString(Resources.FormatDecimal), calculation.Name);
        }

        public string Name
        {
            get { return "CalculationSubTotal"; }
        }

        public object Text
        {
            get { return calculation.Name; }
        }

        public IValueSource Copy()
        {
            return new CalculationSubTotalValueSource(calculation);
        }
    }
}
