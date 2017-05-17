using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Test
{
    public interface ITestView : IView, IViewReady
    {
        string NameValue { get; set; }

        string ExpectedResultValue { get; set; }

        string ExpectedResultTypeValue { get; set; }
        string ComparisonValue { get; set; }
        void PopulateFormulas(IEnumerable<string> formulas);
    }
}