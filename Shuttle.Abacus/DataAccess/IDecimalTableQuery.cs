using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IDecimalTableQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        DataTable GetValues(Guid id);
        DataTable DecimalTableReport(Guid decimalTableId);
    }
}