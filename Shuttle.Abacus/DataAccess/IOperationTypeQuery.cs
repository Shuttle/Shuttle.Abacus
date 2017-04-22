using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess
{
    public interface IOperationTypeQuery
    {
        IQueryResult All();
        IEnumerable<OperationTypeDTO> AllDTOs();
    }
}
