using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface ICalculationQuery
    {
        IQueryResult AllForOwner(Guid ownerId);
        IQueryResult AllBeforeCalculation(Guid methodId, Guid calculationId);
        IQueryResult Get(Guid id);
        IQueryResult Name(Guid id);
        IQueryResult AllForMethod(Guid methodId);
        IEnumerable<CalculationDTO> DTOsBeforeCalculation(Guid methodId, Guid calculationId);
        IEnumerable<CalculationDTO> DTOsForMethod(Guid methodId);
        IQueryResult AllForMethod(Guid methodId, Guid grabberCalculationId);
        IQueryResult GraphNodeArguments(Guid calculationId);
    }
}
