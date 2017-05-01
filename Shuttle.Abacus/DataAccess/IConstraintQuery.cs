using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IConstraintQuery
    {
        IEnumerable<DataRow> QueryAllForOwner(Guid ownerId);
        IEnumerable<ConstraintDTO> DTOsForOwner(Guid ownerId);
        void GetOwned(IConstraintOwner owner);
        void SaveOwned(IConstraintOwner owner);
    }
}