using System;
using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public interface IArgumentQuery
    {
        IQueryResult All();
        IQueryResult Get(Guid id);
        IQueryResult GetAnswerCatalog(Guid id);
        IQueryResult Definitions();
        IQueryResult Name(Guid id);
        IEnumerable<ArgumentDTO> AllDTOs();
        ArgumentDTO ArgumentDTO(Guid argumentId);
    }
}
