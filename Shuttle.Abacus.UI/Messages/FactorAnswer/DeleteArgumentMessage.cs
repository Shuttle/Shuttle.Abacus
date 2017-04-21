using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class DeleteArgumentMessage : Message
    {
        public DeleteArgumentMessage(Guid argumentId, string text, Resource ownerResource)
        {
            ArgumentId = argumentId;
            Text = text;
            OwnerResource = ownerResource;
        }

        public Guid ArgumentId { get; private set; }
        public string Text { get; private set; }
        public Resource OwnerResource { get; set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Argument; }
        }
    }
}
