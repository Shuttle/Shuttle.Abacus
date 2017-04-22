using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class NewFormulaFromExistingMessage : Message
    {
        public NewFormulaFromExistingMessage(Guid methodId, string ownerName, Guid ownerId, Guid formulaId)
        {
            MethodId = methodId;
            OwnerName = ownerName;
            OwnerId = ownerId;
            FormulaId = formulaId;
        }

        public Guid MethodId { get; private set; }
        public string OwnerName { get; private set; }
        public Guid OwnerId { get; private set; }
        public Guid FormulaId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
