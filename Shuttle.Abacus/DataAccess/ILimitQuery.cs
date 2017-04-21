using System;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface ILimitQuery
    {
        IQueryResult AllForOwner(Guid ownerId);
        IQueryResult Get(Guid limitId);
    }
}
