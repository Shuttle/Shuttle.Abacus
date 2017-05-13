using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.ArgumentValue
{
    public class RemoveArgumentValueMessage : Message
    {
        public RemoveArgumentValueMessage(string value, Guid argumentId)
        {
            ArgumentId = argumentId;
            Value = value;
        }

        public Guid ArgumentId { get; private set; }
        public string Value { get; private set; }

        public override IPermission RequiredPermission => Permissions.Argument;
    }
}
