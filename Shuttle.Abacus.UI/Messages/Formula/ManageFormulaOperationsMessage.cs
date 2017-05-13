using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Formula
{
    public class ManageFormulaOperationsMessage : Message
    {
        public string FormulaName { get; private set; }
        public Guid FormulaId { get; private set; }

        public ManageFormulaOperationsMessage(string formulaName, Guid formulaId)
        {
            FormulaName = formulaName;
            FormulaId = formulaId;
        }

        public override IPermission RequiredPermission => Permissions.Formula;
    }
}
