using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IDecimalTableQuery
    {
        IEnumerable<DataRow> All();
        IEnumerable<DecimalTableDTO> AllDTOs();
        DataRow Get(Guid id);
        DataTable ConstrainedDecimalValues(Guid id);
        DataTable QueryDecimalTable(Guid decimalTableId);
    }
}
