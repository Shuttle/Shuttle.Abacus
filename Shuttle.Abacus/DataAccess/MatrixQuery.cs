using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Contract;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixQuery : IMatrixQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMatrixQueryFactory _matrixQueryFactory;

        public MatrixQuery(IDatabaseGateway databaseGateway, IMatrixQueryFactory matrixQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, nameof(databaseGateway));
            Guard.AgainstNull(matrixQueryFactory, nameof(matrixQueryFactory));
            
            _databaseGateway = databaseGateway;
            _matrixQueryFactory = matrixQueryFactory;
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

        public void Registered(Guid id, string name, Guid? columnArgumentId, Guid rowArgumentId,
            string dataTypeName)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Add(id, name, columnArgumentId, rowArgumentId,
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