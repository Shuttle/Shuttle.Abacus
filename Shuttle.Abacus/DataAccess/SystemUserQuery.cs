using System;

namespace Shuttle.Abacus.DataAccess
{
    public class SystemUserQuery :ISystemUserQuery
    {
        public IQueryResult All()
        {
            return QueryProcessor.Execute(SystemUserQueryFactory.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(SystemUserQueryFactory.Get(id));
        }

        public IQueryResult GetPermissions(Guid id)
        {
            return QueryProcessor.Execute(SystemUserQueryFactory.GetPermissions(id));
        }
    }
}
