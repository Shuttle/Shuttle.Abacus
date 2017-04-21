using System;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess
{
    public class SystemUserQuery :ISystemUserQuery
    {
        public IQueryResult All()
        {
            return QueryProcessor.Execute(SystemUserQueries.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(SystemUserQueries.Get(id));
        }

        public IQueryResult GetPermissions(Guid id)
        {
            return QueryProcessor.Execute(SystemUserQueries.GetPermissions(id));
        }
    }
}
