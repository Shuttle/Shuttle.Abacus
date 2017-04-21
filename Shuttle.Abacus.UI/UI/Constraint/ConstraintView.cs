using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Abacus.DTO;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public partial class ConstraintView : GenericConstraintView, IConstraintView
    {
        private readonly ListViewExtender lvw ;
        private MoneyFormatter valueFormatter;
        private IEnumerable<ConstraintTypeDTO> constraintTypes;

        public ConstraintView()
        {
            InitializeComponent();

            lvw = new ListViewExtender(ConstraintsListView);
        }

        public void PopulateFactors(IEnumerable<ArgumentDTO> items)
        {
            Answer.Items.Clear();

            Argument.DisplayMember = "Name";
            items.ForEach(item => Argument.Items.Add(item));
        }

        public void SetContraintTypes(IEnumerable<ConstraintTypeDTO> items)
        {
            constraintTypes = items;
        }

        public void PopulateConstraintTypes(bool restrictToAnswerCatalog)
        {
            Constraint.Items.Clear();
            Constraint.DisplayMember = "Text";

            constraintTypes.ForEach(item =>
                {
                    if (!restrictToAnswerCatalog || item.EnabledForAnswerCatalog)
                    {
                        Constraint.Items.Add(item);
                    }
                });
        }

        public ArgumentDTO ArgumentDto
        {
            get { return Argument.SelectedItem as ArgumentDTO; }
        }

        public ConstraintTypeDTO ConstraintTypeDTO
        {
            get { return Constraint.SelectedItem as ConstraintTypeDTO; }
        }

        public void EnableAnswerSelection()
        {
            Answer.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void EnableAnswerEntry()
        {
            Answer.DropDownStyle = ComboBoxStyle.DropDown;
            Answer.Text = string.Empty;
            Answer.Items.Clear();
        }

        public void PopulateAnswerCatalogValues(IEnumerable<ArgumentRestrictedAnswerDTO> list)
        {
            Answer.Items.Clear();

            list.ForEach(dto =>
                {
                    if (!ContainsAnswerName(dto.Answer))
                    {
                        Answer.Items.Add(dto.Answer);
                    }
                });
        }

        private bool ContainsAnswerName(string name)
        {
            foreach (string item in Answer.Items)
            {
                if (name.Equals(item, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public string AnswerValue
        {
            get { return Answer.Text; }
            set { Answer.Text = value; }
        }

        public bool HasAnswer
        {
            get { return Answer.Text.Length > 0; }
        }

        public bool HasArgument
        {
            get { return Argument.Text.Length > 0; }
        }

        public bool HasConstraint
        {
            get { return Constraint.Text.Length > 0; }
        }

        public List<ConstraintDTO> Constraints
        {
            get
            {
                var result = new List<ConstraintDTO>();

                foreach (ListViewItem item in ConstraintsListView.Items)
                {
                    var tag = (ItemTag) item.Tag;

                    result.Add(new ConstraintDTO
                               {
                                   ArgumentDto = tag.ArgumentDto,
                                   ConstraintTypeDTO = tag.ConstraintTypeDTO,
                                   Value = tag.ValueSelection
                               });
                }

                return result;
            }
            set { value.ForEach(dto => AddConstraint(dto.ArgumentDto, dto.ConstraintTypeDTO, dto.Value)); }
        }

        public ComboBox ValueSelectionControl
        {
            get { return Answer; }
        }

        public TextBox FormattedControl
        {
            get { return FormattedTextBox; }
        }

        public void ShowAnswerError(string message)
        {
            SetError(Answer, message);
        }

        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the argument to use.");
        }

        public void ShowConstraintError()
        {
            SetError(Constraint, "Please select the constraint to use.");
        }

        public void AddConstraint(ArgumentDTO argumentDto, ConstraintTypeDTO constraintTypeDTO,
                                  string valueSelection)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);
            item.SubItems.Add(string.Empty);

            ConstraintsListView.Items.Add(PopulateItem(item, argumentDto, constraintTypeDTO, valueSelection));
        }

        public void ShowAllConstraints()
        {
            PopulateConstraintTypes(false);
        }

        public void ShowAnswerCatalogConstraints()
        {
            PopulateConstraintTypes(true);
        }

        public void AttachValueFormatter(MoneyFormatter formatter)
        {
            valueFormatter = formatter;
        }

        public void DetachValueFormatter()
        {
            FormattedTextBox.Text = string.Empty;

            if (valueFormatter == null)
            {
                return;
            }

            valueFormatter.Dispose();

            valueFormatter = null;
        }

        private void ArgumentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError(Argument);

            Presenter.ArgumentChanged();
        }

        private void Constraint_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(Constraint, string.Empty);
        }

        private void ValueSelection_TextChanged(object sender, EventArgs e)
        {
            SetError(Answer, string.Empty);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (Presenter.ConstraintOK())
            {
                AddConstraint(ArgumentDto, ConstraintTypeDTO, AnswerValue);
            }
        }

        private static ListViewItem PopulateItem(ListViewItem item, ArgumentDTO argumentDto,
                                                 ConstraintTypeDTO constraintTypeDTO, string valueSelection)
        {
            item.Text = argumentDto.Name;
            item.SubItems[1].Text = constraintTypeDTO.Text;
            item.SubItems[2].Text = valueSelection;

            item.Tag = new ItemTag(argumentDto, constraintTypeDTO, valueSelection);

            return item;
        }

        private void ConstraintsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lvw.SelectedItem();

            var b = item != null;

            if (b)
            {
                Argument.Text = item.Text;
                Constraint.Text = item.SubItems[1].Text;
                AnswerValue = item.SubItems[2].Text;
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

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (!lvw.HasSelectedItem)
            {
                return;
            }

            if (!Presenter.ConstraintOK())
            {
                return;
            }

            PopulateItem(lvw.SelectedItem(), ArgumentDto, ConstraintTypeDTO, AnswerValue);
        }

        private class ItemTag
        {
            public ItemTag(ArgumentDTO argumentDto, ConstraintTypeDTO constraintTypeDTO,
                           string valueSelection)
            {
                ArgumentDto = argumentDto;
                ConstraintTypeDTO = constraintTypeDTO;
                ValueSelection = valueSelection;
            }

            public ArgumentDTO ArgumentDto { get; private set; }
            public ConstraintTypeDTO ConstraintTypeDTO { get; private set; }
            public string ValueSelection { get; private set; }
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            lvw.MoveSelectedUp();
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            lvw.MoveSelectedDown();
        }

        private void Answer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }

    public class GenericConstraintView : View<IConstraintPresenter>
    {
    }
}
