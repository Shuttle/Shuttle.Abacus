using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.UI.Models
{
    public class SimpleGridModel
    {
        public SimpleGridModel()
        {
            VisibleColumns = new List<QueryColumn>();
            HiddenColumns = new List<QueryColumn>();
            EditableColumns = new List<QueryColumn>();
        }

        public SimpleGridModel(DataTable result)
            : this()
        {
            GridItems = result;
        }

        public DataTable GridItems { get; set; }
        public IList<QueryColumn> VisibleColumns { get; set; }
        public IList<QueryColumn> HiddenColumns { get; set; }
        public IList<QueryColumn> EditableColumns { get; set; }
    }
}
