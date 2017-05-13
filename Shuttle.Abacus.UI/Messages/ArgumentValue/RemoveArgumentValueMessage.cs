using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.ArgumentValue
{
    public class RemoveArgumentValueMessage : Message
    {
        public RemoveArgumentValueMessage(string value, Guid argumentId, Resource ownerResource)
        {
            ArgumentId = argumentId;
            Value = value;
            OwnerResource = ownerResource;
        }

        public Guid ArgumentId { get; private set; }
        public string Value { get; private set; }
        public Resource OwnerResource { get; set; }

        public override IPermission RequiredPermission => Permissions.Argument;
    }
}
