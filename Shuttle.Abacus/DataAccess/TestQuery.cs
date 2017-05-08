using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class TestQuery : ITestQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ITestQueryFactory _queryFactory;

        public TestQuery(IDatabaseGateway databaseGateway, ITestQueryFactory queryFactory)
        {
            Guard.AgainstNull(databaseGateway, "databaseGateway");
            Guard.AgainstNull(queryFactory, "queryFactory");

            _databaseGateway = databaseGateway;
            _queryFactory = queryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_queryFactory.Get(id));
        }

        public IEnumerable<DataRow> GetArgumentAnswers(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.GetArgumentAnswers(id));
        }

        public IEnumerable<DataRow> AllUsingArgument(Guid argumentId)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.AllUsingArgument(argumentId));
        }

        public IEnumerable<DataRow> FetchForMethodId(Guid methodId)
        {
            return _databaseGateway.GetRowsUsing(_queryFactory.AllForMethod(methodId));
        }
    }
}