using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public interface IValueSourceTypeQuery
    {
        IQueryResult All();
        IEnumerable<ValueSourceTypeDTO> AllDTOs();
    }
}
