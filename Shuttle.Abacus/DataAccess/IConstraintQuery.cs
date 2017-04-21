using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface IConstraintQuery
    {
        IEnumerable<ConstraintTypeDTO> ConstraintTypes();
        IEnumerable<ConstraintDTO> DTOsForOwner(Guid ownerId);
        IQueryResult QueryAllForOwner(Guid ownerId);
    }
}
