using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.Data
{
    public interface IAnswerTypeQuery
    {
        IQueryResult All();
        IEnumerable<AnswerTypeDTO> AllDTOs();
    }
}
