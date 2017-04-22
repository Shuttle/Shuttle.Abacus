using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Report
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
