using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Argument
{
    public class RenameArgumentMessage : Message
    {
        public RenameArgumentMessage(Guid argumentId)
        {
            ArgumentId = argumentId;
        }

        public override IPermission RequiredPermission => Permissions.Argument;

        public Guid ArgumentId { get; private set; }
    }
}
