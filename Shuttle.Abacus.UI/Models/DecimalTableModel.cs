using System.Collections.Generic;
using System.Data;
using Abacus.DTO;

namespace Abacus.UI
{
    public class DecimalTableModel
    {
        public IEnumerable<ArgumentDTO> Factors { get; set; }
        public IEnumerable<ConstraintTypeDTO> ConstraintTypes { get; set; }
        public DataRow DecimalTableRow { get; set; }
        public DataTable ConstrainedDecimalValues { get; set; }
    }
}
