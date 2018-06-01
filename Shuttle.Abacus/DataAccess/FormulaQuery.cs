using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaQuery : IFormulaQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IFormulaQueryFactory _queryFactory;

        public FormulaQuery(IDatabaseGateway databaseGateway, IFormulaQueryFactory queryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(queryFactory, "queryFactory");

            _databaseGateway = databaseGateway;
            _queryFactory = queryFactory;
        }

        public IEnumerable<DataRow> Operations(Guid formulaId)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.Operations(formulaId));
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_queryFactory.Get(id));
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.All());
        }

        public void Registered(Guid formulaId, string name)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Registered(formulaId, name));
        }

        public void Remove(Guid formulaId)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Remove(formulaId));
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveOperations(formulaId));
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveConstraints(formulaId));
        }

        public void Rename(Guid formulaId, string name)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Renamed(formulaId, name));
        }

        public void RemoveOperations(Guid formulaId)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveOperations(formulaId));
        }

        public void AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource,
            string valueSelection)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.AddOperation(formulaId, sequenceNumber, operation, valueSource,
                valueSelection));
        }

        public void RemoveConstraints(Guid formulaId)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveConstraints(formulaId));
        }

        public void AddConstraint(Guid formulaId, int sequenceNumber, string argumentName, string comparison,
            string value)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.AddConstraint(formulaId, sequenceNumber, argumentName,
                comparison, value));
        }

        public IEnumerable<DataRow> Constraints(Guid formulaId)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.Constraints(formulaId));
        }
    }
}