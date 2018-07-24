using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixQuery : IMatrixQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IKeyStore _keyStore;
        private readonly IMatrixQueryFactory _matrixQueryFactory;

        public MatrixQuery(IDatabaseGateway databaseGateway, IMatrixQueryFactory matrixQueryFactory, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseGateway, nameof(databaseGateway));
            Guard.AgainstNull(matrixQueryFactory, nameof(matrixQueryFactory));
            Guard.AgainstNull(keyStore, nameof(keyStore));

            _databaseGateway = databaseGateway;
            _matrixQueryFactory = matrixQueryFactory;
            _keyStore = keyStore;
        }

        public IEnumerable<DataRow> Search(MatrixSearchSpecification specification)
        {
            return _databaseGateway.GetRowsUsing(_matrixQueryFactory.Search(specification));
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_matrixQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_matrixQueryFactory.Get(id));
        }

        public void Registered(Guid id, string name, string columnArgumentName, string rowArgumentName,
            string dataTypeName)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.RemoveElements(id));
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.RemoveConstraints(id));
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Remove(id));

            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Add(id, name, columnArgumentName, rowArgumentName,
                dataTypeName));
        }

        public void ConstraintAdded(Guid id, int sequenceNumber, string axis, string comparison, string value)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.ConstraintAdded(id, sequenceNumber, axis, comparison,
                value));
        }

        public void ElementAdded(Guid id, int column, int row, string value)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.ElementAdded(id, column, row, value));
        }
    }
}