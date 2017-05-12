using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.FactorAnswer
{
    public class RenameArgumentMessage : Message
    {
        public RenameArgumentMessage(Guid argumentId)
        {
            ArgumentId = argumentId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Argument; }
        }

        public Guid ArgumentId { get; private set; }
    }
}
