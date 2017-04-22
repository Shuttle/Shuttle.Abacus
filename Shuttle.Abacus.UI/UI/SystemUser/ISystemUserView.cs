using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.SystemUser
{
    public interface ISystemUserView : IView
    {
        string LoginNameValue { get; set; }
        IRuleCollection<object> LoginNameValueRules { set; }
    }
}
