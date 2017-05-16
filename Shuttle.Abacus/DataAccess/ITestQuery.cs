using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ITestQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        IEnumerable<DataRow> ArgumentValues(Guid id);
    }
}
