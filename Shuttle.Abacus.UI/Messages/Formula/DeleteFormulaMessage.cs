using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class DeleteFormulaMessage : Message
    {
        public DeleteFormulaMessage(Resource ownerResource, string text, string ownerName, Guid ownerId, Guid formulaId)
        {
            OwnerResource = ownerResource;
            Text = text;
            OwnerName = ownerName;
            OwnerId = ownerId;
            FormulaId = formulaId;
        }

        public Resource OwnerResource { get; private set; }
        public string Text { get; private set; }
        public string OwnerName { get; private set; }
        public Guid OwnerId { get; private set; }
        public Guid FormulaId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
