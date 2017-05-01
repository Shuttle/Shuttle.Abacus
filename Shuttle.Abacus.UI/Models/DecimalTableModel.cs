using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.UI.Models
{
    public class DecimalTableModel
    {
        public IEnumerable<DataRow> ArgumentRows { get; set; }
        public IEnumerable<ConstraintTypeDTO> ConstraintTypes { get; set; }
        public DataRow DecimalTableRow { get; set; }
        public DataTable ConstrainedDecimalValues { get; set; }
    }
}
