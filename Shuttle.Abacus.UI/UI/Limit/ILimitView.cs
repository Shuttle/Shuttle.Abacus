using Abacus.Validation;

namespace Abacus.UI
{
    public interface ILimitView : IView
    {
        string LimitNameValue { get; set; }
        string TypeValue { get; set; }

        IRuleCollection<object> LimitNameRules { set; }
        IRuleCollection<object> TypeRules { set; }
    }
}
