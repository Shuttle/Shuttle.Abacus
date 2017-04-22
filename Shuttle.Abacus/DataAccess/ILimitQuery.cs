using System;

namespace Shuttle.Abacus.DataAccess
{
    public interface ILimitQuery
    {
        IQueryResult AllForOwner(Guid ownerId);
        IQueryResult Get(Guid limitId);
    }
}
