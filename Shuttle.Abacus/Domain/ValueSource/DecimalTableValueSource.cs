using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class DecimalTableValueSource :
        IValueSource,
        IValueSelectionHolder,
        ISpecification<IMethodContext>
    {
        private readonly DecimalTable decimalTable;

        public DecimalTableValueSource(DecimalTable decimalTable)
        {
            this.decimalTable = decimalTable;
        }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            return decimalTable.IsSatisfiedBy(collectionMethodContext);
        }

        public string ValueSelection
        {
            get { return decimalTable.Id.ToString("n"); }
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return decimalTable.Get(methodContext).Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            var rate = decimalTable.Find(methodContext);

            return rate != null
                       ? string.Format("{0} (from rate table '{1}' - {2})", rate.Value, decimalTable.Name,
                                       rate.Description(methodContext))
                       : string.Empty;
        }

        public string Name
        {
            get { return "DecimalTable"; }
        }

        public object Text
        {
            get { return decimalTable.Name; }
        }

        public IValueSource Copy()
        {
            return new DecimalTableValueSource(decimalTable);
        }
    }
}
