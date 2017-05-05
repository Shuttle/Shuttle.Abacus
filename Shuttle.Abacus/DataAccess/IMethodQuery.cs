using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMethodQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
    }
}