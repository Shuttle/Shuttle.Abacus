using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMatrixQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        bool Contains(string name);
    }
}