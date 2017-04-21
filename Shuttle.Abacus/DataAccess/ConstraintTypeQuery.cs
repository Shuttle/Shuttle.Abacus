using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess
{
    public class ConstraintTypeQuery :IConstraintTypeQuery
    {
        public IQueryResult All()
        {
            return QueryProcessor.Execute(ConstraintTypeQueries.All());
        }
    }
}
