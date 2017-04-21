using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ApplicationExitMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
