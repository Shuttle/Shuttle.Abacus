using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.DecimalTable
{
    public class NewDecimalTableFromExistingMessage : Message
    {
        public NewDecimalTableFromExistingMessage(Guid decimalTableId)
        {
            MatrixId = decimalTableId;
        }

        public Guid MatrixId { get; private set; }

        public override IPermission RequiredPermission => Permissions.Matrix;
    }
}
