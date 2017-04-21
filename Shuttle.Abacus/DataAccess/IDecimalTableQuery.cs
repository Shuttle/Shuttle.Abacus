using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface IDecimalTableQuery
    {
        IQueryResult All();
        IEnumerable<DecimalTableDTO> AllDTOs();
        IQueryResult Get(Guid id);
        DataTable ConstrainedDecimalValues(Guid id);
        IQueryResult Name(Guid id);
        DataTable QueryDecimalTable(Guid decimalTableId);
    }
}
