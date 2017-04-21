using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Abacus.DTO;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public partial class GraphNodeArgumentView : GenericGraphNodeArgumentView, IGraphNodeArgumentView
    {
        private readonly ListViewExtender lvw;

        public GraphNodeArgumentView()
        {
            InitializeComponent();

            lvw = new ListViewExtender(ArgumentListView);
        }

        public void PopulateFactors(IEnumerable<ArgumentDTO> list)
        {
            Argument.DisplayMember = "Name";
            list.ForEach(item => Argument.Items.Add(item));
        }

        public ArgumentDTO ArgumentDto
        {
            get { return Argument.SelectedItem as ArgumentDTO; }
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

        public IEnumerable<GraphNodeArgumentDTO> GraphNodeArguments
        {
            get
            {
                var result = new List<GraphNodeArgumentDTO>();

                foreach (ListViewItem item in ArgumentListView.Items)
                {
                    var tag = (ItemTag) item.Tag;

                    result.Add(new GraphNodeArgumentDTO
                                   {
                                       ArgumentDTO = tag.ArgumentDTO,
                                       Format = tag.Format
                                   });
                }

                return result;
            }
            set { value.ForEach(dto => AddArgument(dto.ArgumentDTO, dto.Format)); }
        }


        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the argument to use.");
        }

        public void ShowFormatError()
        {
            SetError(Format, "Please enter a format to use.");
        }

        public void AddArgument(ArgumentDTO argumentDto, string format)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);

            ArgumentListView.Items.Add(PopulateItem(item, argumentDto, format));
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
                AddArgument(ArgumentDto, FormatValue);
            }
        }

        private static ListViewItem PopulateItem(ListViewItem item, ArgumentDTO argumentDto, string format)
        {
            item.Text = argumentDto.Name;
            item.SubItems[1].Text = format;

            item.Tag = new ItemTag(argumentDto, format);

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

            PopulateItem(lvw.SelectedItem(), ArgumentDto, FormatValue);
        }

        private class ItemTag
        {
            public ItemTag(ArgumentDTO argumentDto, string format)
            {
                ArgumentDTO = argumentDto;
                Format = format;
            }

            public ArgumentDTO ArgumentDTO { get; private set; }
            public string Format { get; private set; }
        }
    }

    public class GenericGraphNodeArgumentView : View<IGraphNodeArgumentPresenter>
    {
    }
}