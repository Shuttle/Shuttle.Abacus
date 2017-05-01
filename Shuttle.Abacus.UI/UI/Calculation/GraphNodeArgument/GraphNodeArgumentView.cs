using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.DataAccess;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Extensions;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Calculation.GraphNodeArgument
{
    public partial class GraphNodeArgumentView : GenericGraphNodeArgumentView, IGraphNodeArgumentView
    {
        private readonly ListViewExtender lvw;

        public GraphNodeArgumentView()
        {
            InitializeComponent();

            lvw = new ListViewExtender(ArgumentListView);
        }

        public void PopulateArguments(IEnumerable<DataRow> rows)
        {
            Argument.DisplayMember = "Name";
            rows.ForEach(item => Argument.Items.Add(item));
        }

        public DataRow ArgumentRow
        {
            get { return Argument.SelectedItem as DataRow; }
        }

        public string FormatValue
        {
            get { return Format.Text; }
        }

        public bool HasArgument
        {
            get { return Argument.Text.Length > 0; }
        }

        public bool HasFormat
        {
            get { return Format.Text.Length > 0; }
        }

        public IEnumerable<GraphNodeDataRow> GraphNodeArguments
        {
            get
            {
                var result = new List<GraphNodeDataRow>();

                foreach (ListViewItem item in ArgumentListView.Items)
                {
                    var tag = (ItemTag) item.Tag;

                    //TODO
                    //result.Add(new GraphNodeDataRow
                    //               {
                    //                   DataRow = tag.DataRow,
                    //                   Format = tag.Format
                    //               });
                }

                return result;
            }
            set
            {
                //TODO
                //value.ForEach(dto => AddArgument(dto.DataRow, dto.Format)); 
            }
        }


        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the argument to use.");
        }

        public void ShowFormatError()
        {
            SetError(Format, "Please enter a format to use.");
        }

        public void AddArgumentRow(DataRow row, string format)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);

            ArgumentListView.Items.Add(PopulateItem(item, row, format));
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            lvw.MoveSelectedUp();
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            lvw.MoveSelectedDown();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Presenter.ArgumentOK())
            {
                AddArgumentRow(ArgumentRow, FormatValue);
            }
        }

        private static ListViewItem PopulateItem(ListViewItem item, DataRow row, string format)
        {
            item.Text = ArgumentColumns.Name.MapFrom(row);
            item.SubItems[1].Text = format;

            item.Tag = new ItemTag(row, format);

            return item;
        }

        private void Argument_TextChanged(object sender, EventArgs e)
        {
            ClearError(Argument);
        }

        private void Format_TextChanged(object sender, EventArgs e)
        {
            ClearError(Format);
        }

        private void ArgumentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lvw.SelectedItem();

            var b = item != null;

            if (b)
            {
                Argument.Text = item.Text;
                Format.Text = item.SubItems[1].Text;
            }

            MoveDownButton.Enabled = b;
            MoveUpButton.Enabled = b;
            RemoveButton.Enabled = b;
            ApplyButton.Enabled = b;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            lvw.RemoveSelectedItem();
        }

        private void Argument_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError(Argument);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (!lvw.HasSelectedItem)
            {
                return;
            }

            if (!Presenter.ArgumentOK())
            {
                return;
            }

            PopulateItem(lvw.SelectedItem(), ArgumentRow, FormatValue);
        }

        private class ItemTag
        {
            public ItemTag(DataRow row, string format)
            {
                ArgumentRow = row;
                Format = format;
            }

            public DataRow ArgumentRow { get; private set; }
            public string Format { get; private set; }
        }
    }

    public class GenericGraphNodeArgumentView : View<IGraphNodeArgumentPresenter>
    {
    }
}