using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IArgumentQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        IEnumerable<DataRow> GetValues(Guid id);
        DataRow Get(string name);
    }
}
