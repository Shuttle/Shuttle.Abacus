using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.DecimalTable
{
    public class NewDecimalTableFromExistingMessage : Message
    {
        public NewDecimalTableFromExistingMessage(Guid decimalTableId)
        {
            DecimalTableId = decimalTableId;
        }

        public Guid DecimalTableId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.DecimalTable; }
        }
    }
}
