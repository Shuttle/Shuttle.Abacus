using System;
using Abacus.Infrastructure;

namespace Abacus.UI
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
