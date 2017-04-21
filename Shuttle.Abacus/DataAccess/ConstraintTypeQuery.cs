using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class ConstraintTypeQuery : DataQuery, IConstraintTypeQuery
    {
        public IQueryResult All()
        {
            return QueryProcessor.Execute(ConstraintTypeQueries.All());
        }
    }
}
