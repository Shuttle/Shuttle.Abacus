using Abacus.Infrastructure;

namespace Abacus.UI
{
    public interface IPermissionsView : IView
    {
        IPermissionCollection AssignedPermissions { get; set; }
        IPermissionCollection AvailablePermissions { set; }
    }
}
