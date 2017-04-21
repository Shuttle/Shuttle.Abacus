using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class RunMethodTestMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }
    }
}