using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixQuery : IMatrixQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMatrixQueryFactory _matrixQueryFactory;

        public MatrixQuery(IDatabaseGateway databaseGateway, IMatrixQueryFactory matrixQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(matrixQueryFactory, "matrixQueryFactory");

            _databaseGateway = databaseGateway;
            _matrixQueryFactory = matrixQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_matrixQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_matrixQueryFactory.Get(id));
        }

        public DataTable GetValues(Guid id)
        {
            return _databaseGateway.GetDataTableFor(_matrixQueryFactory.ConstrainedDecimalValues(id));
        }

        public DataTable Report(Guid id)
        {
            return _databaseGateway.GetDataTableFor(_matrixQueryFactory.Report(id));
        }
    }
}