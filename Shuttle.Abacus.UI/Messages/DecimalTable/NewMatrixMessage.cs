using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.DecimalTable
{
    public class NewMatrixMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Matrix; }
        }
    }
}
