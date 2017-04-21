using System.Collections.Generic;
using Abacus.Data;

namespace Abacus.UI
{
    public class SimpleListModel
    {
        public SimpleListModel()
        {
            VisibleColumns = new List<QueryColumn>();
            HiddenColumns = new List<QueryColumn>();
        }

        public SimpleListModel(IQueryResult result) : this()
        {
            ListItems = result;
        }

        public IQueryResult ListItems { get; set; }
        public IList<QueryColumn> VisibleColumns { get; set; }
        public List<QueryColumn> HiddenColumns { get; set; }
        public bool HasCheckBoxes { get; set; }
    }
}
