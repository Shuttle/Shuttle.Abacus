using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public abstract class Operation :
        ISpecification<IMethodContext>
    {
        protected Operation(IValueSource source)
        {
            Guard.AgainstNull(source, "source");

            ValueSource = source;
        }

        public IValueSource ValueSource { get; private set; }

        public abstract string Symbol { get; }
        public abstract string Name { get; }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var specification = ValueSource as ISpecification<IMethodContext>;

            return specification == null || specification.IsSatisfiedBy(collectionMethodContext);
        }

        public abstract decimal Execute(decimal total, decimal operand);
        public abstract decimal Operand(IMethodContext methodContext, IFormulaCalculationContext calculationContext);
    }
}
