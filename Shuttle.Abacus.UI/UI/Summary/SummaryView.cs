using System;
using System.Drawing;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Summary
{
    public partial class SummaryView : GenericSummaryView, ISummaryView
    {
        private static readonly DataGridViewCellStyle groupCellStyle =
            new DataGridViewCellStyle
            {
                Font = new Font(SystemFonts.CaptionFont, FontStyle.Bold),
                BackColor = Color.White
            };

        private static readonly DataGridViewCellStyle headerCellStyle =
            new DataGridViewCellStyle
            {
                ForeColor = Color.White,
                BackColor = Color.SteelBlue
            };

        public SummaryView()
        {
            InitializeComponent();

            CurrentHeadingColumn = 0;

            GridView.SelectionChanged += SelectionChanged;
        }

        protected int CurrentHeadingColumn { get; set; }

        private int CurrentRow
        {
            get { return GridView.RowCount - 1; }
        }

        public void Clear()
        {
            GridView.Rows.Clear();
            GridView.Columns.Clear();
        }

        public void EnsureColumnCount(int count)
        {
            while (GridView.ColumnCount < count)
            {
                GridView.Columns.Add(string.Empty, string.Empty);
            }
        }

        public void AddGroup(string name)
        {
            AddRow();

            GridView[0, CurrentRow].Value = name;
            GridView[0, CurrentRow].Style = groupCellStyle;
        }

        public void AddRow()
        {
            GridView.Rows.Add(1);

            ResetCurrentHeadingColumn();
        }

        public void AddHeading(string text)
        {
            GridView[CurrentHeadingColumn, CurrentRow].Value = text;
            GridView[CurrentHeadingColumn, CurrentRow].Style = headerCellStyle;

            CurrentHeadingColumn++;
        }

        public void AddRow(object[] values)
        {
            GridView.Rows.Add(values);
        }

        public void AddAttribute(string text, object value)
        {
            AddRow();

            AddHeading(text);
            AddAttribute(value);
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            if (GridView.SelectedCells.Count > 0)
            {
                GridView.SelectedCells[0].Selected = false;
            }
        }

        private void AddAttribute(object value)
        {
            GridView[1, CurrentRow].Value = Convert.ToString(value);
        }

        private void ResetCurrentHeadingColumn()
        {
            CurrentHeadingColumn = 0;
        }
    }

    public class GenericSummaryView : View<ISummaryPresenter>
    {
    }
}
