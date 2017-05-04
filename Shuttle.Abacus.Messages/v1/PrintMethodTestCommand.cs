using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class PrintMethodTestCommand
    {
        public PrintMethodTestCommand(Guid workItemId)
        {
            WorkItemId = workItemId;
            MethodTestIds = new List<Guid>();
        }

        public Guid WorkItemId { get; private set; }
        public List<Guid> MethodTestIds { get; private set; }

    }
}
