using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class MethodTestRunMessage : NullPermissionMessage
    {
        public MethodTestRunMessage(Guid workItemId, MethodTestRunEvent message)
        {
            WorkItemId = workItemId;
            Event = message;
        }

        public Guid WorkItemId { get; set; }
        public MethodTestRunEvent Event { get; set; }
    }
}
