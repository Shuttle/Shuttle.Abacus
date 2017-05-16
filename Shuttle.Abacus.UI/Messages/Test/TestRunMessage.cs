using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.Test
{
    public class TestRunMessage : NullPermissionMessage
    {
        public TestRunMessage(Guid workItemId, TestRunEvent message)
        {
            WorkItemId = workItemId;
            Event = message;
        }

        public Guid WorkItemId { get; set; }
        public TestRunEvent Event { get; set; }
    }
}
