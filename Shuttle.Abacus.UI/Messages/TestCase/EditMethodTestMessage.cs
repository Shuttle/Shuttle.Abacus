using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class EditMethodTestMessage : Message
    {
        public EditMethodTestMessage()
        {
        }

        public EditMethodTestMessage(Guid testId, Guid methodId, string description, decimal expectedResult)
        {
            MethodTestId = testId;
            MethodId = methodId;
            Description = description;
            ExpectedResult = expectedResult;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Test; }
        }

        public Guid WorkItemId { get; set; }

        public Guid MethodTestId { get; set; }
        public Guid MethodId { get; set; }
        public string Description { get; set; }
        public decimal ExpectedResult { get; set; }
    }
}
