using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class SubtractionOperation : Operation
    {
        public SubtractionOperation(IValueSource source)
            : base(source)
        {
        }

        public override string Symbol => "-";

        public override string Name => "Subtraction";

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return total - operand;
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }
    }
}
