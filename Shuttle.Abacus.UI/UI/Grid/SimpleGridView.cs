using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.Grid
{
    public partial class SimpleGridView : GenericSimpleGridView, ISimpleGridView
    {
        public SimpleGridView()
        {
            InitializeComponent();
        }

        public void PopulateGridView(SimpleGridModel model)
        {
            GridView.DataSource = model.GridItems;

            foreach (DataGridViewColumn column in GridView.Columns)
            {
                column.ReadOnly = true;
                column.HeaderText = ResourceItem.GetText(column.Name);
            }

            foreach(var name in model.HiddenColumns)
            {
                var column = GridView.Columns[name];

                if (column == null)
                {
                    continue;
                }

                column.Visible = false;
            }

            foreach (var name in model.VisibleColumns)
            {
                var column = GridView.Columns[name];

                if (column == null)
                {
                    continue;
                }

                column.Visible = true;
            }

            foreach (var name in model.EditableColumns)
            {
                var column = GridView.Columns[name];

                if (column == null)
                {
                    continue;
                }

                column.ReadOnly = false;
            }

        }

        public DataTable Items => (DataTable)GridView.DataSource;
    }

    public class GenericSimpleGridView : View<ISimpleGridPresenter>
    {
    }
}
