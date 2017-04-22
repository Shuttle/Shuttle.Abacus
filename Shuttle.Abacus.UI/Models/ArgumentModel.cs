using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class ArgumentModel
    {
        public DataRow ArgumentRow { get; set; }
        public IEnumerable<AnswerTypeDTO> AnswerTypes { get; set; }
    }
}
