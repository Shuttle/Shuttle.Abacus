using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewMethodMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}