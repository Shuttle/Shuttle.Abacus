using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewSystemUserMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.SystemUser; }
        }
    }
}
