using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class DecimalTableQuery : IDecimalTableQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDecimalTableQueryFactory _decimalTableQueryFactory;

        public DecimalTableQuery(IDatabaseGateway databaseGateway, IDecimalTableQueryFactory decimalTableQueryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(decimalTableQueryFactory, "decimalTableQueryFactory");

            _databaseGateway = databaseGateway;
            _decimalTableQueryFactory = decimalTableQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_decimalTableQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_decimalTableQueryFactory.Get(id));
        }

        public DataTable GetValues(Guid id)
        {
            return _databaseGateway.GetDataTableFor(_decimalTableQueryFactory.ConstrainedDecimalValues(id));
        }

        public DataTable DecimalTableReport(Guid decimalTableId)
        {
            return _databaseGateway.GetDataTableFor(_decimalTableQueryFactory.DecimalTableReport(decimalTableId));
        }
    }
}