using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQuery :IFormulaQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IFormulaQueryFactory _formulaQueryFactory;
        private readonly IDataTableRepository<OperationDTO> _operationRepository;

        public FormulaQuery(IDatabaseGateway databaseGateway, IFormulaQueryFactory formulaQueryFactory, IDataTableRepository<OperationDTO> operationRepository)
        {
            _databaseGateway = databaseGateway;
            _formulaQueryFactory = formulaQueryFactory;
            _operationRepository = operationRepository;
        }

        public IEnumerable<DataRow> AllForOwner(Guid ownerId)
        {
            return _databaseGateway.GetRowsUsing(_formulaQueryFactory.AllForOwner(ownerId));
        }

        public IEnumerable<OperationDTO> OperationDTOs(Guid formulaId)
        {
                return _operationRepository.FetchAllUsing(_formulaQueryFactory.GetOperations(formulaId));
        }

        public IEnumerable<DataRow> Operations(Guid formulaId)
        {
            return QueryProcessor.Execute(_formulaQueryFactory.GetOperations(formulaId));
        }

        public DataRow Get(Guid id)
        {
            return QueryProcessor.Execute(_formulaQueryFactory.Get(id));
        }
    }
}
