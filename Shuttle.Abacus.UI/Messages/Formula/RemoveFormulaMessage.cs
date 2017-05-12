using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class RemoveFormulaMessage : Message
    {
        public RemoveFormulaMessage(string text, Guid formulaId, Resource ownerResource)
        {
            Text = text;
            FormulaId = formulaId;
            OwnerResource = ownerResource;
        }

        public string Text { get; private set; }
        public Guid FormulaId { get; private set; }

        public Resource OwnerResource { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Formula; }
        }
    }
}
