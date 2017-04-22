using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ILimitQuery
    {
        IEnumerable<DataRow> AllForOwner(Guid ownerId);
        DataRow Get(Guid limitId);
    }
}
