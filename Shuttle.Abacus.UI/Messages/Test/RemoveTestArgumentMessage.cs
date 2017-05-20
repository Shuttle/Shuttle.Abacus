using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Test
{
    public class RemoveTestArgumentMessage : Message
    {
        public RemoveTestArgumentMessage(Guid testId, string argumentName)
        {
            TestId = testId;
            ArgumentName = argumentName;
        }

        public Guid TestId { get; }
        public string ArgumentName { get; }

        public override IPermission RequiredPermission => Permissions.Test;
    }
}