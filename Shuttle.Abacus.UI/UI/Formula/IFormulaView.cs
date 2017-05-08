using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Formula
{
    public interface IFormulaView : IView
    {
        string NameValue { get; set; }
        string MaximumFormulaNameValue { get; set; }
        string MinimumFormulaNameValue { get; set; }
        void PopulateFormulas(IEnumerable<string> formulaNames);
    }
}