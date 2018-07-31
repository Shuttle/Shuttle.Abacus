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

        public IEnumerable<DataRow> Constraints(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_matrixQueryFactory.Constraints(id));
        }

        public IEnumerable<DataRow> Elements(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_matrixQueryFactory.Elements(id));
        }

        public void Registered(Guid id, string name, Guid? columnArgumentId, Guid rowArgumentId,
            string dataTypeName)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.Add(id, name, columnArgumentId, rowArgumentId,
                dataTypeName));
        }

        public void ConstraintRegistered(Guid matrixId, string axis, int index, Guid id, string comparison, string value)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.ConstraintRegistered(matrixId, axis, index,
                id, comparison, value));
        }

        public void ElementRegistered(Guid matrixId, int column, int row, Guid id, string value)
        {
            _databaseGateway.ExecuteUsing(_matrixQueryFactory.ElementRegistered(matrixId, column, row, id, value));
        }
    }
}