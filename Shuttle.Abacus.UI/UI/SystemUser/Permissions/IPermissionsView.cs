using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Presentation;

namespace Shuttle.Abacus.UI.UI.SystemUser
{
    public interface IPermissionsView : IView
    {
        IPermissionCollection AssignedPermissions { get; set; }
        IPermissionCollection AvailablePermissions { set; }
    }
}
