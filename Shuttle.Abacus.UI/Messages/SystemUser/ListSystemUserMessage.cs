using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ListSystemUserMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.SystemUser; }
        }
    }
}
