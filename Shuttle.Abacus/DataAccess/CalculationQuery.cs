using System;
using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class CalculationQuery :ICalculationQuery
    {
        private readonly IDataRowMapper<CalculationDTO> calculationDTOMapper;

        public CalculationQuery(IDataRowMapper<CalculationDTO> calculationDTOMapper)
        {
            this.calculationDTOMapper = calculationDTOMapper;
        }

        public IQueryResult AllForOwner(Guid ownerId)
        {
            return QueryProcessor.Execute(CalculationQueries.AllForOwner(ownerId));
        }

        public IQueryResult AllBeforeCalculation(Guid methodId, Guid calculationId)
        {
            return QueryProcessor.Execute(CalculationQueries.AllBeforeCalculation(methodId, calculationId));
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(CalculationQueries.Get(id));
        }

        public IQueryResult Name(Guid id)
        {
            return QueryProcessor.Execute(CalculationQueries.Name(id));
        }

        public IQueryResult AllForMethod(Guid methodId)
        {
            return QueryProcessor.Execute(CalculationQueries.AllForMethod(methodId));
        }

        public IEnumerable<CalculationDTO> DTOsBeforeCalculation(Guid methodId, Guid calculationId)
        {
            return calculationDTOMapper.MapFrom(AllBeforeCalculation(methodId, calculationId).Table);
        }

        public IEnumerable<CalculationDTO> DTOsForMethod(Guid methodId)
        {
            return calculationDTOMapper.MapFrom(AllForMethod(methodId).Table);
        }

        public IQueryResult AllForMethod(Guid methodId, Guid grabberCalculationId)
        {
            return QueryProcessor.Execute(CalculationQueries.AllForMethod(methodId, grabberCalculationId));
        }

        public IQueryResult GraphNodeArguments(Guid calculationId)
        {
            return QueryProcessor.Execute(CalculationQueries.GraphNodeArguments(calculationId));
        }
    }
}
