using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Test
{
    public interface ITestView : IView
    {
        string NameValue { get; set; }

        string ExpectedResultValue { get; set; }

        IRuleCollection<object> NameRules { set; }
        IRuleCollection<object> ExpectedResultRules { set; }
        string ExpectedResultTypeValue { get; set; }
        string ComparisonValue { get; set; }
    }
}