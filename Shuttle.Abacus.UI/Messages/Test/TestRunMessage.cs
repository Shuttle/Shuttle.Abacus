using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.Test
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
