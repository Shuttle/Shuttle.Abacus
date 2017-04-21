using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface IFormulaQuery
    {
        IQueryResult AllForOwner(Guid ownerId);
        IEnumerable<OperationDTO> OperationDTOs(Guid formulaId);
        IQueryResult Operations(Guid formulaId);
        IQueryResult Description(Guid formulaId);
        IQueryResult Get(Guid id);
        IQueryResult OperationsSummary(Guid formulaId);
    }
}
