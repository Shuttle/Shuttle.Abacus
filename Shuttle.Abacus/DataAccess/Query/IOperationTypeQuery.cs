using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public interface IOperationTypeQuery
    {
        IQueryResult All();
        IEnumerable<OperationTypeDTO> AllDTOs();
    }
}
