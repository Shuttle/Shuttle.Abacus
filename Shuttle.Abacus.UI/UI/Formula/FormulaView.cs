using System;
using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Formula
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

        public string NameValue { get; set; }
        public string MaximumFormulaNameValue { get; set; }
        public string MinimumFormulaNameValue { get; set; }
    }

    public class GenericFormulaView : View<IFormulaPresenter>
    {
    }
}