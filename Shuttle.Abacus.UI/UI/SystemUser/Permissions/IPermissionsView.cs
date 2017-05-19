using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Presentation;

namespace Shuttle.Abacus.Shell.UI.SystemUser
{
    public interface IPermissionsView : IView
    {
        IPermissionCollection AssignedPermissions { get; set; }
        IPermissionCollection AvailablePermissions { set; }
    }
}
