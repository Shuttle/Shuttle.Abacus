using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
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
