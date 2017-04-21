using System;
using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.Data
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
