using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public interface IConstraintQuery
    {
        IEnumerable<DataRow> AllForOwner(Guid ownerId);
        void GetOwned(IConstraintOwner owner);
        void SaveOwned(IConstraintOwner owner);
    }
}