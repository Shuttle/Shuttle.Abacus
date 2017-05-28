using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Test
{
    public class TestExecutedMessage : Message
    {
        public TestExecutedMessage(Guid id, string formulaName, decimal result)
        {
            Id = id;
            FormulaName = formulaName;
            Result = result;
        }

        public string FormulaName { get; }
        public Guid Id { get; }
        public decimal Result { get; }

        public override IPermission RequiredPermission => Permissions.Test;
    }
}