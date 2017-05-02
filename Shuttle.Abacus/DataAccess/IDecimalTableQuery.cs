using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IDecimalTableQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        DataTable ConstrainedDecimalValues(Guid id);
        DataTable DecimalTableReport(Guid decimalTableId);
    }
}
