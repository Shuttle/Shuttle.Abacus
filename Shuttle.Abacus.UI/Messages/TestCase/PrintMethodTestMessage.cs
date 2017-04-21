using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class PrintMethodTestMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }
    }
}