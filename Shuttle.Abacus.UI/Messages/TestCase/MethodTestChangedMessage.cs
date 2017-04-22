using System;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class MethodTestChangedMessage : NullPermissionMessage
    {
        public MethodTestChangedMessage(Guid workItemId, Guid methodId)
        {
            WorkItemId = workItemId;
            MethodId = methodId;
        }

        public Guid WorkItemId { get; private set; }
        public Guid MethodId { get; private set; }
    }
}
