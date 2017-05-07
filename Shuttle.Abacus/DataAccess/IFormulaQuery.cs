using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQuery
    {
        IEnumerable<DataRow> Operations(Guid formulaId);
        DataRow Get(Guid id);
        IEnumerable<DataRow> All();
    }
}