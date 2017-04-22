using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IAnswerTypeQuery
    {
        IEnumerable<AnswerTypeDTO> All();
    }
}
