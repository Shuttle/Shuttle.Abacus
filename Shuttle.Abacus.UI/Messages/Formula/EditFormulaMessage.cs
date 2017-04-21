using System;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class EditFormulaMessage : Message
    {
        public EditFormulaMessage(Guid methodId, string ownerName, Guid ownerId, Guid formulaId)
        {
            MethodId = methodId;
            OwnerName = ownerName;
            OwnerId = ownerId;
            FormulaId = formulaId;
        }

        public Guid OwnerId { get; private set; }
        public Guid MethodId { get; private set; }
        public string OwnerName { get; private set; }
        public Guid FormulaId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}
