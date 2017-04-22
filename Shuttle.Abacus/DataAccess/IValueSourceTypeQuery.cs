using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess
{
    public interface IValueSourceTypeQuery
    {
        IQueryResult All();
        IEnumerable<ValueSourceTypeDTO> AllDTOs();
    }
}
