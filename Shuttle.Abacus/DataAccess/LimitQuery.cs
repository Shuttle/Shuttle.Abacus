using System;

namespace Shuttle.Abacus.DataAccess
{
    public class LimitQuery :ILimitQuery
    {
        public IQueryResult AllForOwner(Guid ownerId)
        {
            return QueryProcessor.Execute(LimitQueryFactory.AllForOwner(ownerId));
        }

        public IQueryResult Get(Guid limitId)
        {
            return QueryProcessor.Execute(LimitQueryFactory.Get(limitId));
        }
    }
}
