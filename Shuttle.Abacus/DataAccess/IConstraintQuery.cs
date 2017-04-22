using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IConstraintQuery
    {
        IEnumerable<DataRow> QueryAllForOwner(Guid ownerId);
    }
}