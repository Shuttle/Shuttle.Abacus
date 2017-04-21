using Abacus.Validation;

namespace Abacus.UI
{
    public interface ISystemUserView : IView
    {
        string LoginNameValue { get; set; }
        IRuleCollection<object> LoginNameValueRules { set; }
    }
}
