using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface IValueSourceTypeQuery
    {
        IQueryResult All();
        IEnumerable<ValueSourceTypeDTO> AllDTOs();
    }
}
