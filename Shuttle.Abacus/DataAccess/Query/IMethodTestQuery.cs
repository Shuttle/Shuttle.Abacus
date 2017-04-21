﻿using System;

namespace Abacus.Data
{
    public interface IMethodTestQuery
    {
        IQueryResult All();
        IQueryResult Get(Guid id);
        IQueryResult FetchForMethodId(Guid methodId);
        IQueryResult GetArgumentAnswers(Guid id);
        IQueryResult AllUsingArgument(Guid argumentId);
    }
}
