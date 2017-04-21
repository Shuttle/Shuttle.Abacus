using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class RunMethodTestCommand : IMessage, IRunMethodTestCommand
    {
        public RunMethodTestCommand(Guid workItemId)
        {
            WorkItemId = workItemId;
            MethodTestIds = new List<Guid>();
        }

        public Guid WorkItemId { get; private set; }
        public List<Guid> MethodTestIds { get; private set; }
    }
}
