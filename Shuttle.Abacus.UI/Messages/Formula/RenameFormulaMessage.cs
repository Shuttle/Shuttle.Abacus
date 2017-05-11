using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class RenameFormulaMessage : Message
    {
        public Guid FormulaId { get; }

        public RenameFormulaMessage(Guid formulaId)
        {
            FormulaId = formulaId;
        }

        public override IPermission RequiredPermission => Permissions.Formula;
    }
}
