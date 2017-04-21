using System;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess
{
    public class LimitQuery :ILimitQuery
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
