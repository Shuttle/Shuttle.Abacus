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

        public FormulaQuery(IDatabaseGateway databaseGateway,
            IFormulaQueryFactory queryFactory)
        {
            Guard.AgainstNull(databaseGateway, nameof(databaseGateway));
            Guard.AgainstNull(queryFactory, nameof(queryFactory));

            _databaseGateway = databaseGateway;
            _queryFactory = queryFactory;
        }

        public IEnumerable<DataRow> Operations(Guid formulaId)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.Operations(formulaId));
        }

        public DataRow Get(Guid id)
        {
            var row = _databaseGateway.GetSingleRowUsing(_queryFactory.Get(id));

            if (row == null)
            {
                throw EntityNotFoundException.For("Formula", id);
            }

            return row;
        }

        public IEnumerable<DataRow> Search(FormulaSearchSpecification specification)
        {
            Guard.AgainstNull(specification, nameof(specification));

            return _databaseGateway.GetRowsUsing(_queryFactory.Search(specification));
        }

        public void Registered(Guid formulaId, string name)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Registered(formulaId, name));
        }

        public void Remove(Guid formulaId)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Remove(formulaId));
        }

        public void Rename(Guid formulaId, string name)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Rename(formulaId, name));
        }

        public void RemoveOperation(Guid operationId)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveOperation(operationId));
        }

        public void AddOperation(Guid operationId, Guid formulaId, int sequenceNumber, string operation,
            string valueProviderName, string inputParameter)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.AddOperation(operationId, formulaId, sequenceNumber, operation,
                valueProviderName, inputParameter));
        }

        public void RemoveConstraint(Guid constraintId)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.RemoveConstraint(constraintId));
        }

        public void AddConstraint(Guid constraintId, Guid formulaId, string argumentName, string comparison,
            string value)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.AddConstraint(constraintId, formulaId, argumentName, comparison,
                value));
        }

        public IEnumerable<DataRow> Constraints(Guid formulaId)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.Constraints(formulaId));
        }
    }
}