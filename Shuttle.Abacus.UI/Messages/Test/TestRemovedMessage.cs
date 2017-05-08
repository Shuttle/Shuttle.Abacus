using System;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class TestRemovedMessage : NullPermissionMessage
    {
        public TestRemovedMessage(Guid workItemId, Guid methodId)
        {
            WorkItemId = workItemId;
            MethodId = methodId;
        }

        public Guid WorkItemId { get; private set; }
        public Guid MethodId { get; private set; }
    }
}
