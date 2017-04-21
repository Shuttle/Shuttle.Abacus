using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NullPermissionMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
