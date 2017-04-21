using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.UI
{
    public class MethodTestModel
    {
        public IEnumerable<ArgumentDTO> Arguments { get; set; }

        public DataRow MethodTestRow { get; set; }
        public DataTable ArgumentAnswers { get; set; }
    }
}
