using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class DecimalTableValueSource :
        IValueSource,
        IValueSelectionHolder,
        ISpecification<IMethodContext>
    {
        private readonly Matrix matrix;

        public DecimalTableValueSource(Matrix matrix)
        {
            this.matrix = matrix;
        }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            return matrix.IsSatisfiedBy(collectionMethodContext);
        }

        public string ValueSelection
        {
            get { return matrix.Id.ToString("n"); }
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            throw new NotImplementedException();
            //return matrix.Get(methodContext).Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            var rate = matrix.Find(methodContext);

            throw new NotImplementedException();
            //return rate != null
            //           ? string.Format("{0} (from rate table '{1}' - {2})", rate.Value, matrix.Name,
            //                           rate.Description(methodContext))
            //           : string.Empty;
        }

        public string Name
        {
            get { return "Matrix"; }
        }

        public object Text
        {
            get { return matrix.Name; }
        }

        public IValueSource Copy()
        {
            return new DecimalTableValueSource(matrix);
        }
    }
}
