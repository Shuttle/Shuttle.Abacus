using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.ArgumentValue
{
    public interface IArgumentValueView : IView
    {
        string ValueValue { get; set; }
        IRuleCollection<object> ValueRules { set; }
    }
}
