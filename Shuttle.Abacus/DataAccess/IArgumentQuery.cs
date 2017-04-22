using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
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
