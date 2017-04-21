using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess.Query
{
    public interface IAnswerTypeQuery
    {
        IEnumerable<AnswerTypeDTO> All();
    }
}
