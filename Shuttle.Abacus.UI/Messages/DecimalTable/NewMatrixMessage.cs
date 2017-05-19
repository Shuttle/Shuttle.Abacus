using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.DecimalTable
{
    public class NewMatrixMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Matrix;
    }
}
