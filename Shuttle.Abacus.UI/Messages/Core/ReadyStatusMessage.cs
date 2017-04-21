using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ReadyStatusMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
