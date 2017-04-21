using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class MultiplicationOperation : FormulaOperation
    {
        public MultiplicationOperation(IValueSource source)
            : base(source)
        {
        }

        public override string Symbol
        {
            get { return "*"; }
        }

        public override string Name
        {
            get { return "Multiplication"; }
        }

        public override decimal Execute(decimal total, decimal operand)
        {
            Guard.AgainstNull(operand, "context");

            return total*operand;
        }

        public override decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        {
            return ValueSource.Operand(methodContext, calculationContext);
        }

        public override FormulaOperation Copy()
        {
            return new MultiplicationOperation(ValueSource.Copy());
        }
    }
}
