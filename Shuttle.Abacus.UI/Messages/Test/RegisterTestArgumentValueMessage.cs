using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Test
{
    public class RegisterTestArgumentValueMessage : Message
    {
        public Guid TestId { get; }

        public RegisterTestArgumentValueMessage(Guid testId)
        {
            TestId = testId;
        }

        public override IPermission RequiredPermission => Permissions.Test;
    }
}