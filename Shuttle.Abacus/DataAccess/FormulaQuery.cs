using System;
using System.Collections.Generic;
using Shuttle.Abacus.DataAccess.Definitions;
using Shuttle.Abacus.DataAccess.Query;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQuery :IFormulaQuery
    {
        private readonly IDataTableRepository<OperationDTO> operationRepository;

        public FormulaQuery(IDataTableRepository<OperationDTO> operationRepository)
        {
            this.operationRepository = operationRepository;
        }

        public IQueryResult AllForOwner(Guid ownerId)
        {
            return QueryProcessor.Execute(FormulaQueries.AllForOwner(ownerId));
        }

        public IEnumerable<OperationDTO> OperationDTOs(Guid formulaId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return operationRepository.FetchAllUsing(FormulaQueries.GetOperations(formulaId));
            }
        }

        public IQueryResult Operations(Guid formulaId)
        {
            return QueryProcessor.Execute(FormulaQueries.GetOperations(formulaId));
        }

        public IQueryResult Description(Guid formulaId)
        {
            return QueryProcessor.Execute(FormulaQueries.Description(formulaId));
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(FormulaQueries.Get(id));
        }

        public IQueryResult OperationsSummary(Guid formulaId)
        {
            return QueryProcessor.Execute(FormulaQueries.OperationsSummary(formulaId));
        }
    }
}
