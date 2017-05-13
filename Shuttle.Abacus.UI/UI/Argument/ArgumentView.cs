﻿using System;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Invariants.Core;
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
