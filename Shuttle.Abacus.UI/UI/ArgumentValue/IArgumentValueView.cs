using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.ArgumentValue
{
    public interface IArgumentValueView : IView
    {
        string ValueValue { get; set; }
        IRuleCollection<object> ValueRules { set; }
    }
}
