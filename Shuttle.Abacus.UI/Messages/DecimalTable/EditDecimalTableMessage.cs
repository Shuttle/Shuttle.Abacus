using System;
using Abacus.Infrastructure;

namespace Abacus.UI
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
            get { return Permissions.DecimalTable; }
        }

        public string DecimalTableName { get; private set; }
    }
}
