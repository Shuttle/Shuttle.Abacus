using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Formula
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
