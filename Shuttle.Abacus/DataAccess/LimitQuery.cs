using System;

namespace Abacus.Data
{
    public class LimitQuery : DataQuery, ILimitQuery
    {
        public IQueryResult AllForOwner(Guid ownerId)
        {
            return QueryProcessor.Execute(LimitQueries.AllForOwner(ownerId));
        }

        public IQueryResult Get(Guid limitId)
        {
            return QueryProcessor.Execute(LimitQueries.Get(limitId));
        }
    }
}
