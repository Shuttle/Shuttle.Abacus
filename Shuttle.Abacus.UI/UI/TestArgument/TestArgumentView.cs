using System;
using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.TestArgument
{
    public partial class TestArgumentView : GenericTestArgumentView, ITestArgumentView
    {
        public TestArgumentView()
        {
            InitializeComponent();
        }

        public IRuleCollection<object> DescriptionRules
        {
            set { ViewValidator.Control(Description).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> ExpectedResultRules
        {
            set { ViewValidator.Control(ExpectedResult).ShouldSatisfy(value); }
        }

        public bool HasAnswer => ValueSelectionControl.Text.Length > 0;

        public string AnswerValue
        {
            get { return ValueSelectionControl.Text; }
            set { ValueSelectionControl.Text = value; }
        }

        public bool HasArgument => Argument.Text.Length > 0;

        public void PopulateArgumentValues(IEnumerable<string> answers)
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

        public ArgumentModel ArgumentModel => Argument.SelectedItem as ArgumentModel;

        public string ArgumentValue
        {
            get { return ValueSelectionControl.Text; }
            set { ValueSelectionControl.Text = value; }
        }

        public bool HasArgumentValue => ValueSelectionControl.Text.Length > 0;

        public void ShowArgumentValueError(string message)
        {
            SetError(ValueSelectionControl, message);
        }

        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the argument to use.");
        }

        public void ShowAnswerError(string message)
        {
            SetError(ValueSelectionControl, message);
        }

        public void AddArgument(ArgumentModel model)
        {
            Argument.Items.Add(model);
        }

        private void ArgumentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError(Argument);

            Presenter.ArgumentChanged();
        }

        private void ValueSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(ValueSelectionControl, string.Empty);
        }
    }

    public class GenericTestArgumentView : View<ITestArgumentPresenter>
    {
    }
}