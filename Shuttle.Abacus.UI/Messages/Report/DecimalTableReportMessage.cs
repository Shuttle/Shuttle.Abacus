using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class DecimalTableReportMessage : Message
    {
        public DecimalTableReportMessage(Guid decimalTableId, string decimalTableName)
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
