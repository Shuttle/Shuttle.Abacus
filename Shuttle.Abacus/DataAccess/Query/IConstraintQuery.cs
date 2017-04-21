using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public interface IConstraintQuery
    {
        IEnumerable<ConstraintTypeDTO> ConstraintTypes();
        IEnumerable<ConstraintDTO> DTOsForOwner(Guid ownerId);
        IQueryResult QueryAllForOwner(Guid ownerId);
    }
}
