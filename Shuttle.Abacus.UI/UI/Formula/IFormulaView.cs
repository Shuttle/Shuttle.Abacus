using System.Collections.Generic;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Formula
{
    public interface IFormulaView : IView
    {
        string NameValue { get; set; }
        string MaximumFormulaNameValue { get; set; }
        string MinimumFormulaNameValue { get; set; }
        IRuleCollection<object> FormulaNameRules { set; }

        void PopulateFormulas(IEnumerable<string> formulaNames);
    }
}