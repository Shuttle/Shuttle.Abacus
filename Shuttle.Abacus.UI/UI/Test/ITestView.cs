using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.Test
{
    public interface ITestView : IView, IViewReady
    {
        string NameValue { get; set; }

        string ExpectedResultValue { get; set; }

        string ExpectedResultTypeValue { get; set; }
        string ComparisonValue { get; set; }
        string FormulaNameValue { get; set; }
        void PopulateFormulas(IEnumerable<string> formulas);
    }
}