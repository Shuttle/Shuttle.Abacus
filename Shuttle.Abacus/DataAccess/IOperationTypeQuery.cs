using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IOperationTypeQuery
    {
        IEnumerable<DataRow> All();
        IEnumerable<OperationTypeDTO> AllDTOs();
    }
}
