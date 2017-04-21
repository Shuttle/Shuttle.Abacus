using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class NewArgumentMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Argument; }
        }
    }
}