using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public interface IMethodQuery
    {
        IQueryResult MethodName(Guid id);
        IQueryResult All();
        IQueryResult Get(Guid id);
        IEnumerable<MethodDTO> AllDTOs();
    }
}
