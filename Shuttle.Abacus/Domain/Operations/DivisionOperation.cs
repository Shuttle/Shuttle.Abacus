using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class DivisionOperation : Operation
    {
        public DivisionOperation(IValueSource source)
            : base(source)
        {
        }

        public override string Symbol
        {
            get { return "/"; }
        }

        public override string Name
        {
            get { return "Division"; }
        }

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return total/operand;
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }
    }
}
