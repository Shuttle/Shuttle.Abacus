using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class DisplayHelpManualMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
