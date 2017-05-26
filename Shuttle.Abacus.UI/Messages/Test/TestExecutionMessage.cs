using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Test
{
    public class TestExecutionMessage : Message
    {
        public Guid TestId { get; private set; }

        public TestExecutionMessage(Guid testId)
        {
            TestId = testId;
        }

        public override IPermission RequiredPermission => Permissions.Test;
    }
}