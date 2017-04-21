using System;

namespace Abacus.Data
{
    public interface ISystemUserQuery
    {
        IQueryResult All();
        IQueryResult Get(Guid id);
        IQueryResult GetPermissions(Guid id);
    }
}
