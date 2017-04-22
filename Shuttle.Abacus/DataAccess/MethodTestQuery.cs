using System;

namespace Shuttle.Abacus.DataAccess
{
    public class MethodTestQuery :IMethodTestQuery
    {
        public IQueryResult All()
        {
            return QueryProcessor.Execute(MethodTestQueryFactory.All());
        }

        public IQueryResult Get(Guid id)
        {
            return QueryProcessor.Execute(MethodTestQueryFactory.Get(id));
        }

        public IQueryResult GetArgumentAnswers(Guid id)
        {
            return QueryProcessor.Execute(MethodTestQueryFactory.GetArgumentAnswers(id));
        }

        public IQueryResult AllUsingArgument(Guid argumentId)
        {
            return QueryProcessor.Execute(MethodTestQueryFactory.AllUsingArgument(argumentId));
        }

        public IQueryResult FetchForMethodId(Guid methodId)
        {
            return QueryProcessor.Execute(MethodTestQueryFactory.AllForMethod(methodId));
        }
    }
}
