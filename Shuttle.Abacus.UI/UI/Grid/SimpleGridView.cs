
using System.Data;
using System.Windows.Forms;
using Abacus.Localisation;

namespace Abacus.UI
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

            foreach(var col in model.HiddenColumns)
            {
                GridView.Columns[col.ColumnName].Visible = false;
            }

            foreach (var col in model.VisibleColumns)
            {
                GridView.Columns[col.ColumnName].Visible = true;
            }

            foreach (var col in model.EditableColumns)
            {
                GridView.Columns[col.ColumnName].ReadOnly = false;
            }

        }

        public DataTable Items
        {
            get
            {
                return (DataTable)GridView.DataSource;
            }
        }
    }

    public class GenericSimpleGridView : View<ISimpleGridPresenter>
    {
    }
}
