using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class ChangeMethodTestMessage : Message
    {
        public ChangeMethodTestMessage(EditMethodTestMessage message)
        {
            MethodTestId = message.MethodTestId;
            MethodId = message.MethodId;
            WorkItemId = message.WorkItemId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }

        public Guid MethodTestId { get; set; }

        public Guid MethodId { get; set; }

        public Guid WorkItemId { get; set; }

        public string Description { get; set; }

        public string ExpectedResult { get; set; }
    }
}
