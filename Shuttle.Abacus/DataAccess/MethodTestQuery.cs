using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodTestQuery :IMethodTestQuery
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IMethodTestQueryFactory _methodTestQueryFactory;

        public MethodTestQuery(IDatabaseGateway databaseGateway, IMethodTestQueryFactory methodTestQueryFactory)
        {
            _databaseGateway = databaseGateway;
            _methodTestQueryFactory = methodTestQueryFactory;
        }

        public IEnumerable<DataRow> All()
        {
            return _databaseGateway.GetRowsUsing(_methodTestQueryFactory.All());
        }

        public DataRow Get(Guid id)
        {
            return _databaseGateway.GetSingleRowUsing(_methodTestQueryFactory.Get(id));
        }

        public IEnumerable<DataRow> GetArgumentAnswers(Guid id)
        {
            return _databaseGateway.GetRowsUsing(_methodTestQueryFactory.GetArgumentAnswers(id));
        }

        public IEnumerable<DataRow> AllUsingArgument(Guid argumentId)
        {
            return _databaseGateway.GetRowsUsing(_methodTestQueryFactory.AllUsingArgument(argumentId));
        }

        public IEnumerable<DataRow> FetchForMethodId(Guid methodId)
        {
            return _databaseGateway.GetRowsUsing(_methodTestQueryFactory.AllForMethod(methodId));
        }
    }
}
