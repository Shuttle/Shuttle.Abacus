using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Resources;

namespace Shuttle.Abacus.Shell.Messages.Formula
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

        public override IPermission RequiredPermission => Permissions.Formula;
    }
}
