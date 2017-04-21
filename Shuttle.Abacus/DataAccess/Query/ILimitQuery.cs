using System;

namespace Abacus.Data
{
    public interface ILimitQuery
    {
        IQueryResult AllForOwner(Guid ownerId);
        IQueryResult Get(Guid limitId);
    }
}
