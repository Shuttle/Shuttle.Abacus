using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class ManageFormulaConstraintsMessage : Message
    {
        public string FormulaName { get; private set; }
        public Guid FormulaId { get; private set; }

        public ManageFormulaConstraintsMessage(string formulaName, Guid formulaId)
        {
            FormulaName = formulaName;
            FormulaId = formulaId;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Formula; }
        }
    }
}
