using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class StartShellMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
