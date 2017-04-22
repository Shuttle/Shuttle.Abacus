using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Method
{
    public interface IMethodView : IView
    {
        string MethodNameValue { get; set; }
        IRuleCollection<object> MethodNameRules { set; }
    }
}
