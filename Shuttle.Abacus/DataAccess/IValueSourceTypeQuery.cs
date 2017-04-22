using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IValueSourceTypeQuery
    {
        IEnumerable<DataRow> All();
        IEnumerable<ValueSourceTypeDTO> AllDTOs();
    }
}
