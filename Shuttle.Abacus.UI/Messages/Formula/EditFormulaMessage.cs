using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class EditFormulaMessage : Message
    {
        public EditFormulaMessage(Guid formulaId)
        {
            FormulaId = formulaId;
        }

        public Guid FormulaId { get; private set; }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Formula; }
        }
    }
}
