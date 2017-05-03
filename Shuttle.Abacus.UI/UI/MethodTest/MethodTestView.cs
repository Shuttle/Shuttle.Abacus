using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public partial class MethodTestView : GenericMethodTestView, IMethodTestView
    {
        private MoneyFormatter valueFormatter;

        public MethodTestView()
        {
            InitializeComponent();
        }

        public string DescriptionValue
        {
            get { return Description.Text; }
            set { Description.Text = value; }
        }

        public decimal ExpectedResultValue
        {
            get
            {
                decimal dec;

                return decimal.TryParse(ExpectedResult.Text, out dec)
                           ? dec
                           : 0;
            }
            set { ExpectedResult.Text = Convert.ToString(value); }
        }

        public IList ArgumentAnswers
        {
            get { return ArgumentAnswersListView.Items; }
        }

        public IRuleCollection<object> DescriptionRules
        {
            set { ViewValidator.Control(Description).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> ExpectedResultRules
        {
            set { ViewValidator.Control(ExpectedResult).ShouldSatisfy(value); }
        }

        public bool HasInvalidArgumentAnswers()
        {
            throw new NotImplementedException();

            foreach (ListViewItem item in ArgumentAnswersListView.Items)
            {
                //if (Guid.Empty.Equals(((DataRow)item.Tag).Id))
                //{
                //    return true;
                //}
            }

            return false;
        }

        public DataRow ArgumentRow
        {
            get { return Argument.SelectedItem as DataRow; }
        }

        public bool HasArgument
        {
            get { return Argument.Text.Length > 0; }
        }

        public bool HasAnswer
        {
            get { return Answer.Text.Length > 0; }
        }

        public void EnableAnswerSelection()
        {
            ValueSelectionLabel.Text = "Selection";
            Answer.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void EnableAnswerEntry()
        {
            ValueSelectionLabel.Text = "Value";
            Answer.DropDownStyle = ComboBoxStyle.DropDown;
            Answer.Text = string.Empty;
            Answer.Items.Clear();
        }

        public void PopulateAnswerCatalog(IEnumerable<string> answers)
        {
            Answer.Items.Clear();

            answers.ForEach(answer => Answer.Items.Add(answer));
        }

        public void PopulateFactors(IEnumerable<DataRow> items)
        {
            Answer.Items.Clear();

            Argument.DisplayMember = "Name";
            items.ForEach(item => Argument.Items.Add(item));
        }

        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the input to use.");
        }

        public void ShowAnswerError(string message)
        {
            SetError(Answer, message);
        }

        public void AddArgumentAnswer(Guid argumentId, string answer)
        {
            var dto = FindDataRow(argumentId);

            if (dto == null)
            {
                return;
            }

            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);

            ArgumentAnswersListView.Items.Add(PopulateItem(item, dto, answer));
        }

        public void AddArgument(DataRow dto)
        {
            Argument.Items.Add(dto);
        }

        public string AnswerValue
        {
            get { return Answer.Text; }
            set { Answer.Text = value; }
        }

        private ListViewItem PopulateItem(ListViewItem item, DataRow dto, string answer)
        {
            item.SubItems[1].Text = answer;

            throw new NotImplementedException();

            //TODO
            //item.Tag = dto;
            //item.Text = dto.Name;

            return item;
        }

        private DataRow FindDataRow(Guid argumentId)
        {
            throw new NotImplementedException();

            foreach (DataRow dto in Argument.Items)
            {
                //TODO
                //if (dto.Id.Equals(argumentId))
                //{
                //    return dto;
                //}
            }

            return null;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (ArgumentAnswersListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.");
                return;
            }

            ArgumentAnswersListView.Items.Remove(ArgumentAnswersListView.SelectedItems[0]);
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (!Presenter.ArgumentAnswerOK())
            {
                return;
            }

            if (Argument.SelectedItem == null)
            {
                return;
            }

            var argumentDTO = (DataRow)Argument.SelectedItem;

            throw new NotImplementedException();

            //TODO
            //foreach (ListViewItem item in ArgumentAnswersListView.Items)
            //{
            //    if (argumentDTO.Name != item.Text)
            //    {
            //        continue;
            //    }

            //    PopulateItem(item, argumentDTO, Answer.Text);

            //    return;
            //}

            //AddArgumentAnswer(argumentDTO.Id, Answer.Text);
        }

        private void ArgumentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError(Argument);

            Presenter.ArgumentChanged();
        }

        private void ArgumentAnswerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ArgumentAnswersListView.SelectedItems.Count <= 0)
            {
                return;
            }

            throw new NotImplementedException();

            foreach (DataRow item in Argument.Items)
            {
                //TODO
                //if (item.Name !=
                //    ArgumentAnswersListView.SelectedItems[0].SubItems[0].Text)
                //{
                //    continue;
                //}

                Argument.SelectedItem = item;

                Answer.Text = ArgumentAnswersListView.SelectedItems[0].SubItems[1].Text;

                return;
            }
        }

        private void ValueSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(Answer, string.Empty);
        }

        public ComboBox ValueSelectionControl
        {
            get { return Answer; }
        }

        public TextBox FormattedControl
        {
            get { return FormattedTextBox; }
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

    }

    public class GenericMethodTestView : View<IMethodTestPresenter>
    {
    }
}
