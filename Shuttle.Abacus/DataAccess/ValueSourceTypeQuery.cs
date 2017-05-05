using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class ValueSourceTypeQuery : IValueSourceTypeQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IValueSourceTypeQueryFactory _valueSourceTypeQueryFactory;

        public ValueSourceTypeQuery(IDatabaseGateway databaseGateway,
            IValueSourceTypeQueryFactory valueSourceTypeQueryFactory)
        {
            _databaseGateway = databaseGateway;
            _valueSourceTypeQueryFactory = valueSourceTypeQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_valueSourceTypeQueryFactory.All());
        }
    }
}