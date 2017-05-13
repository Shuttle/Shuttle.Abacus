using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.ArgumentValue
{
    public class RegisterArgumentValueMessage : Message
    {
        public Guid ArgumentId { get; }

        public RegisterArgumentValueMessage(Guid argumentId)
        {
            ArgumentId = argumentId;
        }

        public override IPermission RequiredPermission => Permissions.Argument;
    }
}