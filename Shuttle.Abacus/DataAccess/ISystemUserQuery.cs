using System;

namespace Shuttle.Abacus.DataAccess
{
    public interface ISystemUserQuery
    {
        IQueryResult All();
        IQueryResult Get(Guid id);
        IQueryResult GetPermissions(Guid id);
    }
}
