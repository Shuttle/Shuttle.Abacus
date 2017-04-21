using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.UI
{
    public class ArgumentModel
    {
        public DataRow ArgumentRow { get; set; }
        public IEnumerable<AnswerTypeDTO> AnswerTypes { get; set; }
    }
}
