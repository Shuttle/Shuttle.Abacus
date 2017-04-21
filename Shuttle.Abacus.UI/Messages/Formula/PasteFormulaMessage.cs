using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class PasteFormulaMessage : ResourceMessage
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
