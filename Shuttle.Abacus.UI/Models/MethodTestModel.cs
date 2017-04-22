using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class MethodTestModel
    {
        public IEnumerable<ArgumentDTO> Arguments { get; set; }

        public DataRow MethodTestRow { get; set; }
        public DataTable ArgumentAnswers { get; set; }
    }
}
