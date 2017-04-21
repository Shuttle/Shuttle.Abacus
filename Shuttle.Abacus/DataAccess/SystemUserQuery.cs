using System;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public class SystemUserQuery : DataQuery, ISystemUserQuery
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
