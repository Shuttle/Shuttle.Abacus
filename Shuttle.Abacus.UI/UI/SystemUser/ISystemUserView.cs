using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.SystemUser
{
    public interface ISystemUserView : IView
    {
        string LoginNameValue { get; set; }
        IRuleCollection<object> LoginNameValueRules { set; }
    }
}
