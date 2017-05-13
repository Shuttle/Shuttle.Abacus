using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class AdditionOperation : Operation
    {
        public AdditionOperation(IValueSource source) : base(source)
        {
        }

        public override string Symbol => "+";

        public override string Name => "Addition";

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return total + operand;
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }
    }
}
