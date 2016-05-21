using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class SquareRootOperation : FormulaOperation
    {
        public SquareRootOperation(IValueSource source)
            : base(source)
        {
        }

        public override string Symbol
        {
            get { return "sqrt"; }
        }

        public override string Name
        {
            get { return "SquareRoot"; }
        }

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return Convert.ToDecimal(Math.Sqrt((double) operand));
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }

        public override FormulaOperation Copy()
        {
            return new SquareRootOperation(ValueSource.Copy());
        }
    }
}
