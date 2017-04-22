using System;
using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Argument
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
            get { return AnswerType.Text; }
            set { AnswerType.Text = value; }
        }

        public IRuleCollection<object> ArgumentNameRules
        {
            set { ViewValidator.Control(ArgumentName).ShouldSatisfy(value); }
        }

        public IRuleCollection<object> AnswerTypeRules
        {
            set { ViewValidator.Control(AnswerType).ShouldSatisfy(value); }
        }

        public bool HasValueType
        {
            get { return AnswerType.Text.Length > 0; }
        }

        public AnswerTypeDTO AnswerTypeDTO
        {
            get { return AnswerType.SelectedItem as AnswerTypeDTO; }
        }

        public void PopulateAnswerTypes(IEnumerable<AnswerTypeDTO> items)
        {
            AnswerType.Items.Clear();
            AnswerType.DisplayMember = "Text";

            items.ForEach(item => AnswerType.Items.Add(item));
        }

        private void AnswerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetError(AnswerType, string.Empty);
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
