using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Test
{
    public class EditTestMessage : Message
    {
        public EditTestMessage()
        {
        }

        public EditTestMessage(Guid testId, Guid formulaId, string description, string expectedResult)
        {
            TestId = testId;
            FormulaId = formulaId;
            Description = description;
            ExpectedResult = expectedResult;
        }

        public override IPermission RequiredPermission => Permissions.Test;

        public Guid WorkItemId { get; set; }

        public Guid TestId { get; set; }
        public Guid FormulaId { get; set; }
        public string Description { get; set; }
        public string ExpectedResult { get; set; }
    }
}
