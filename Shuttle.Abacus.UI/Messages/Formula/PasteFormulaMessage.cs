using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class PasteFormulaMessage : ResourceMessage
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
