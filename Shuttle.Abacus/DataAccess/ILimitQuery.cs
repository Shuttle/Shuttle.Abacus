using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public interface ILimitQuery
    {
        IEnumerable<DataRow> AllForOwner(Guid ownerId);
        DataRow Get(Guid limitId);
        void PopulateOwner(ILimitOwner owner);
    }
}
