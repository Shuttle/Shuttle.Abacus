using System;
using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Models;

namespace Shuttle.Abacus.Shell.UI.Test.TestArgument
{
    public partial class TestArgumentView : GenericTestArgumentView, ITestArgumentView
    {
        public TestArgumentView()
        {
            InitializeComponent();
        }

        public bool HasAnswer => ValueControl.Text.Length > 0;

        public string ValueValue
        {
            get { return ValueControl.Text; }
            set { ValueControl.Text = value; }
        }

        public bool HasArgument => Argument.Text.Length > 0;

        public void PopulateArgumentValues(IEnumerable<string> answers)
        {
            ValueControl.Items.Clear();

            answers.ForEach(answer => ValueControl.Items.Add(answer));
        }

        public void PopulateArguments(IEnumerable<ArgumentModel> arguments)
        {
            ValueControl.Items.Clear();

            Argument.DisplayMember = "Name";
            arguments.ForEach(item => Argument.Items.Add(item));
        }

        public ArgumentModel ArgumentModel => Argument.SelectedItem as ArgumentModel;

        public string ArgumentName
        {
            get { return ArgumentModel.Name; }
            set { ValueControl.SelectedIndex = FindArgumentNameIndex(value); }
        }

        private int FindArgumentNameIndex(string argumentName)
        {
            var index = 0;

            foreach (ArgumentModel model in Argument.Items)
            {
                if (model.Name.Equals(argumentName))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public bool HasArgumentValue => ValueControl.Text.Length > 0;

        public void ShowArgumentValueError(string message)
        {
            SetError(ValueControl, message);
        }

        public void ShowArgumentError()
        {
            SetError(Argument, "Please select the argument to use.");
        }

        public void ShowAnswerError(string message)
        {
            SetError(ValueControl, message);
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
            SetError(ValueControl, string.Empty);
        }

        public void OnReady()
        {
            ViewValidator.Control(Argument).ShouldSatisfy(RuleCollection.Required());
            ViewValidator.Control(ValueControl).ShouldSatisfy(RuleCollection.Required());
        }
    }

    public class GenericTestArgumentView : View<ITestArgumentPresenter>
    {
    }
}