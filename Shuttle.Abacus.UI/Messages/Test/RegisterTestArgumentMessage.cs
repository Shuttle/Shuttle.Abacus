using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Test
{
    public class RegisterTestArgumentMessage : Message
    {
        public Guid TestId { get; }

        public RegisterTestArgumentMessage(Guid testId)
        {
            TestId = testId;
        }

        public override IPermission RequiredPermission => Permissions.Test;
    }
}