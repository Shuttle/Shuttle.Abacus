using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class SimpleListModel
    {
        public SimpleListModel()
        {
            VisibleColumns = new List<string>();
            HiddenColumns = new List<string>();
        }

        public SimpleListModel(IEnumerable<DataRow> rows) : this()
        {
            Rows = rows;
        }

        public IEnumerable<DataRow> Rows { get; set; }
        public IList<string> VisibleColumns { get; set; }
        public List<string> HiddenColumns { get; set; }
        public bool HasCheckBoxes { get; set; }
    }
}
