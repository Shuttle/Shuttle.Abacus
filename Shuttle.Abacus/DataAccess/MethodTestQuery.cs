using System;

namespace Abacus.Data
{
    public class MethodTestQuery : DataQuery, IMethodTestQuery
    {
        public IQueryResult All()
        {
            return QueryProcessor.Execute(MethodTestQueries.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(MethodTestQueries.Get(id));
        }

        public IQueryResult GetArgumentAnswers(Guid id)
        {
            return QueryProcessor.Execute(MethodTestQueries.GetArgumentAnswers(id));
        }

        public IQueryResult AllUsingArgument(Guid argumentId)
        {
            return QueryProcessor.Execute(MethodTestQueries.AllUsingArgument(argumentId));
        }

        public IQueryResult FetchForMethodId(Guid methodId)
        {
            return QueryProcessor.Execute(MethodTestQueries.AllForMethod(methodId));
        }
    }
}
