using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface IMethodQuery
    {
        IQueryResult MethodName(Guid id);
        IQueryResult All();
        IQueryResult Get(Guid id);
        IEnumerable<MethodDTO> AllDTOs();
    }
}
