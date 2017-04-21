using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class RoundingOperation : FormulaOperation
    {
        public RoundingOperation(IValueSource source)
            : base(source)
        {
        }

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return decimal.Round(total, Convert.ToInt32(operand), MidpointRounding.ToEven);
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }

        public override string Symbol
        {
            get { return "round"; }
        }

        public override string Name
        {
            get { return "Rounding"; }
        }

        public override FormulaOperation Copy()
        {
            return new RoundingOperation(ValueSource.Copy());
        }
    }
}
