using Abacus.Validation;

namespace Abacus.UI
{
    public interface IMethodView : IView
    {
        string MethodNameValue { get; set; }
        IRuleCollection<object> MethodNameRules { set; }
    }
}
