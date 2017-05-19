using System;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Argument
{
    public partial class ArgumentView : GenericArgumentView, IArgumentView
    {
        public ArgumentView()
        {
            InitializeComponent();
        }

        public string ArgumentNameValue
        {
            get { return ArgumentName.Text; }
            set { ArgumentName.Text = value; }
        }

        public string AnswerTypeValue
        {
            get { return ValueType.Text; }
            set { ValueType.Text = value; }
        }

        public IRuleCollection<object> ArgumentNameRules
        {
            set { ViewValidator.Control(ArgumentName).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> AnswerTypeRules
        {
            set { ViewValidator.Control(ValueType).ShouldSatisfy(value); }
        }

        private void AnswerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(ValueType, string.Empty);
        }

        private void ArgumentName_Leave(object sender, EventArgs e)
        {
            Presenter.ArgumentNameExited();
        }
    }

    public class GenericArgumentView : View<IArgumentPresenter>
    {
    }
}
