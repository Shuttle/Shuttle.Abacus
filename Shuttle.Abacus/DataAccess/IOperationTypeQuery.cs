using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface IOperationTypeQuery
    {
        IQueryResult All();
        IEnumerable<OperationTypeDTO> AllDTOs();
    }
}
