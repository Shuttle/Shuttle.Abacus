using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class PercentageOperation : Operation
    {
        public PercentageOperation(IValueSource source)
            : base(source)
        {
        }

        public override string Symbol => "%";

        public override string Name => "Percentage";

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return total*operand/100;
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }
    }
}
