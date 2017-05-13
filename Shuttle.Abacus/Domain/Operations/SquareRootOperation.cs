using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class SquareRootOperation : Operation
    {
        public SquareRootOperation(IValueSource source)
            : base(source)
        {
        }

        public override string Symbol => "sqrt";

        public override string Name => "SquareRoot";

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return Convert.ToDecimal(Math.Sqrt((double) operand));
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }
    }
}
