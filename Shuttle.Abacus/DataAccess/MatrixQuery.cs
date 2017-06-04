using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public class MatrixQuery : IMatrixQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMatrixQueryFactory _matrixQueryFactory;
        private readonly IKeyStore _keyStore;

        public MatrixQuery(IDatabaseGateway databaseGateway, IMatrixQueryFactory matrixQueryFactory, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(matrixQueryFactory, "matrixQueryFactory");
            Guard.AgainstNull(keyStore, "keyStore");

            _databaseGateway = databaseGateway;
            _matrixQueryFactory = matrixQueryFactory;
            _keyStore = keyStore;
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

        public bool Contains(string name)
        {
            return _keyStore.Contains(Matrix.Key(name));
        }
    }
}