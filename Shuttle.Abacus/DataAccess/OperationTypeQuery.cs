using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class OperationTypeQuery : IOperationTypeQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IOperationTypeQueryFactory _operationTypeQueryFactory;

        public OperationTypeQuery(IDatabaseGateway databaseGateway,
            IOperationTypeQueryFactory operationTypeQueryFactory)
        {
            _databaseGateway = databaseGateway;
            _operationTypeQueryFactory = operationTypeQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_operationTypeQueryFactory.All());
        }
    }
}