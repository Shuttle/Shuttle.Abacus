using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.DecimalTable
{
    public class EditMatrixMessage : Message
    {
        public EditMatrixMessage(Guid decimalTableId, string decimalTableName)
        {
            MatrixId = decimalTableId;
            DecimalTableName = decimalTableName;
        }

        public Guid MatrixId { get; private set; }

        public override IPermission RequiredPermission => Permissions.Matrix;

        public string DecimalTableName { get; private set; }
    }
}
