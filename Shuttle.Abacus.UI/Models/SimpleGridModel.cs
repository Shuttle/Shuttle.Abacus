using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.Shell.Models
{
    public class SimpleGridModel
    {
        public SimpleGridModel()
        {
            VisibleColumns = new List<string>();
            HiddenColumns = new List<string>();
            EditableColumns = new List<string>();
        }

        public SimpleGridModel(DataTable result)
            : this()
        {
            GridItems = result;
        }

        public DataTable GridItems { get; set; }
        public IList<string> VisibleColumns { get; set; }
        public IList<string> HiddenColumns { get; set; }
        public IList<string> EditableColumns { get; set; }
    }
}
