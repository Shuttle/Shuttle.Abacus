using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Section
{
    public class EditMethodMessage : Message
    {
        public Guid MethodId;

        public EditMethodMessage(Guid methodId)
        {
            MethodId = methodId;
        }

        public override IPermission RequiredPermission => Permissions.Formula;
    }
}
