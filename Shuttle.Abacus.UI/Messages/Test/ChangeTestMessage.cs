using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Test
{
    public class ChangeTestMessage : Message
    {
        public ChangeTestMessage(EditTestMessage message)
        {
            MethodTestId = message.TestId;
            MethodId = message.FormulaId;
            WorkItemId = message.WorkItemId;
        }

        public override IPermission RequiredPermission => Permissions.Test;

        public Guid MethodTestId { get; set; }

        public Guid MethodId { get; set; }

        public Guid WorkItemId { get; set; }

        public string Description { get; set; }

        public string ExpectedResult { get; set; }
    }
}
