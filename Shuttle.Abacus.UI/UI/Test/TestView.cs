using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Formatters;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Models;

namespace Shuttle.Abacus.UI.UI.MethodTest
{
    public partial class TestView : GenericMethodTestView, ITestView
    {
        private MoneyFormatter valueFormatter;

        public TestView()
        {
            InitializeComponent();
        }

        public string DescriptionValue
        {
            get { return Description.Text; }
            set { Description.Text = value; }
        }

        public string ExpectedResultValue
        {
            get
            {
                return ExpectedResult.Text;
            }
            set { ExpectedResult.Text = value; }
        }

        public IEnumerable<ArgumentAnswerModel> ArgumentAnswers
        {
            get
            {
                var result = new List<ArgumentAnswerModel>();

                foreach (ListViewItem item in ArgumentAnswersListView.Items)
                {
                    result.Add((ArgumentAnswerModel)item.Tag);
                }

                return result;
            }
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

        public ArgumentModel ArgumentModel
        {
            get { return Argument.SelectedItem as ArgumentModel; }
        }

        public bool HasArgument
        {
            get { return Argument.Text.Length > 0; }
        }

        public bool HasAnswer
        {
            get { return ValueSelectionControl.Text.Length > 0; }
        }

        public void EnableAnswerSelection()
        {
            ValueSelectionLabel.Text = "Selection";
            ValueSelectionControl.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void EnableAnswerEntry()
        {
            ValueSelectionLabel.Text = "Value";
            ValueSelectionControl.DropDownStyle = ComboBoxStyle.DropDown;
            ValueSelectionControl.Text = string.Empty;
            ValueSelectionControl.Items.Clear();
        }

        public void PopulateAnswers(IEnumerable<string> answers)
        {
            ValueSelectionControl.Items.Clear();

            answers.ForEach(answer => ValueSelectionControl.Items.Add(answer));
        }

        public void PopulateArguments(IEnumerable<ArgumentModel> arguments)
        {
            ValueSelectionControl.Items.Clear();

            Argument.DisplayMember = "Name";
            arguments.ForEach(item => Argument.Items.Add(item));
        }

        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the input to use.");
        }

        public void ShowAnswerError(string message)
        {
            SetError(ValueSelectionControl, message);
        }

        public void AddArgumentAnswer(ArgumentModel argument, string answer)
        {
            var item = new ListViewItem();

            item.SubItems.Add(string.Empty);

            ArgumentAnswersListView.Items.Add(PopulateItem(item, argument, answer));
        }

        public void AddArgument(ArgumentModel model)
        {
            Argument.Items.Add(model);
        }

        public string AnswerValue
        {
            get { return ValueSelectionControl.Text; }
            set { ValueSelectionControl.Text = value; }
        }

        public ComboBox ValueSelectionControl { get; private set; }

        public TextBox FormattedControl { get; private set; }

        public void AttachValueFormatter(MoneyFormatter formatter)
        {
            valueFormatter = formatter;
        }

        public void DetachValueFormatter()
        {
            FormattedControl.Text = string.Empty;

            if (valueFormatter == null)
            {
                return;
            }

            valueFormatter.Dispose();

            valueFormatter = null;
        }

        private ListViewItem PopulateItem(ListViewItem item, ArgumentModel argument, string answer)
        {
            item.SubItems[1].Text = answer;
            item.Text = argument.Name;

            item.Tag = new ArgumentAnswerModel
            {
                ArgumentId = argument.Id,
                Argument = argument.Name,
                Answer = answer
            };

            return item;
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

            //AddValue(argumentDTO.Id, Answer.Text);
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

                ValueSelectionControl.Text = ArgumentAnswersListView.SelectedItems[0].SubItems[1].Text;

                return;
            }
        }

        private void ValueSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(ValueSelectionControl, string.Empty);
        }
    }

    public class GenericMethodTestView : View<ITestPresenter>
    {
    }
}