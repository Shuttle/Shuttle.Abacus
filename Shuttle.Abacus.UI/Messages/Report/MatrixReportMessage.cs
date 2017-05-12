using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Report
{
    public class MatrixReportMessage : Message
    {
        public MatrixReportMessage(Guid decimalTableId, string decimalTableName)
        {
            MatrixId = decimalTableId;
            DecimalTableName = decimalTableName;
        }

        public Guid MatrixId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Matrix; }
        }

        public string DecimalTableName { get; private set; }
    }
}
