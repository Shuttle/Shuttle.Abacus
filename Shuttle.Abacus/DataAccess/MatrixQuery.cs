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
        private readonly IMatrixQueryFactory _queryFactory;

        public MatrixQuery(IDatabaseGateway databaseGateway, IMatrixQueryFactory queryFactory)
        {
            Guard.AgainstNull(databaseGateway, nameof(databaseGateway));
            Guard.AgainstNull(queryFactory, nameof(queryFactory));
            
            _databaseGateway = databaseGateway;
            _queryFactory = queryFactory;
        }

        public DataRow Find(MatrixSearchSpecification specification)
        {
            return _databaseGateway.GetSingleRowUsing(_queryFactory.Find(specification));
        }

        public IEnumerable<DataRow> Search(MatrixSearchSpecification specification)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.Search(specification));
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            var row = _databaseGateway.GetSingleRowUsing(_queryFactory.Get(id));

            if (row == null)
            {
                throw RecordNotFoundException.For("Matrix", id);
            }

            return row;
        }

        public IEnumerable<DataRow> Constraints(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.Constraints(id));
        }

        public IEnumerable<DataRow> Elements(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.Elements(id));
        }

        public void Registered(Guid id, string name, Guid? columnArgumentId, Guid rowArgumentId,
            string dataTypeName)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.Add(id, name, columnArgumentId, rowArgumentId,
                dataTypeName));
        }

        public void ConstraintRegistered(Guid matrixId, string axis, int index, Guid id, string comparison, string value)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.ConstraintRegistered(matrixId, axis, index,
                id, comparison, value));
        }

        public void ElementRegistered(Guid matrixId, int column, int row, Guid id, string value)
        {
            _databaseGateway.ExecuteUsing(_queryFactory.ElementRegistered(matrixId, column, row, id, value));
        }
    }
}