using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQuery : IFormulaQuery
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

        public void Registered(Guid formulaId, string name)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Registered(formulaId, name));
        }

        public void Remove(Guid formulaId)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Remove(formulaId));
        }

        public void Rename(Guid formulaId, string name)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.Renamed(formulaId, name));
        }

        public void RemoveOperations(Guid formulaId)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.RemoveOperations(formulaId));
        }

        public void AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource, string valueSelection)
        {
            _databaseGateway.ExecuteUsing(_formulaQueryFactory.AddOperation(formulaId, sequenceNumber, operation, valueSource, valueSelection));
        }
    }
}