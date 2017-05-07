using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQuery :IFormulaQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IFormulaQueryFactory _formulaQueryFactory;

        public FormulaQuery(IDatabaseGateway databaseGateway, IFormulaQueryFactory formulaQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(formulaQueryFactory, "formulaQueryFactory");

            _databaseGateway = databaseGateway;
            _formulaQueryFactory = formulaQueryFactory;
        }

        public IEnumerable<DataRow> Operations(Guid formulaId)
        {
            return _databaseGateway.GetRowsUsing(_formulaQueryFactory.GetOperations(formulaId));
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_formulaQueryFactory.Get(id));
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_formulaQueryFactory.All());
        }
    }
}
