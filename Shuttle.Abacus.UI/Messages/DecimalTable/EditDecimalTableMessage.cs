using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.DecimalTable
{
    public class EditDecimalTableMessage : Message
    {
        public EditDecimalTableMessage(Guid decimalTableId, string decimalTableName)
        {
            DecimalTableId = decimalTableId;
            DecimalTableName = decimalTableName;
        }

        public Guid DecimalTableId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Matrix; }
        }

        public string DecimalTableName { get; private set; }
    }
}
