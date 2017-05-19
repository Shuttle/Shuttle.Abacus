using System;
using System.Collections.Generic;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Formula
{
    public partial class FormulaView : GenericFormulaView, IFormulaView
    {
        public FormulaView()
        {
            InitializeComponent();
        }

        public void PopulateFormulas(IEnumerable<string> formulaNames)
        {
            throw new NotImplementedException();
        }

        public string NameValue { get { return FormulaName.Text; }
            set { FormulaName.Text = value; }
        }
        public string MaximumFormulaNameValue {
            get { return MaximumFormulaName.Text; }
            set { MaximumFormulaName.Text = value; } }
        public string MinimumFormulaNameValue
        {
            get { return MinimumFormulaName.Text; }
            set { MinimumFormulaName.Text = value; }
        }
        public IRuleCollection<object> FormulaNameRules
        {
            set { ViewValidator.Control(FormulaName).ShouldSatisfy(value); }
        }
    }

    public class GenericFormulaView : View<IFormulaPresenter>
    {
    }
}