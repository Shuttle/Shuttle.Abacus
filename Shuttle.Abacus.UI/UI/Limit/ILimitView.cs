using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.Limit
{
    public interface ILimitView : IView
    {
        string LimitNameValue { get; set; }
        string TypeValue { get; set; }

        IRuleCollection<object> LimitNameRules { set; }
        IRuleCollection<object> TypeRules { set; }
    }
}
