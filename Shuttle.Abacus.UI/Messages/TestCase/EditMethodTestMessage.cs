using System;
using Abacus.Infrastructure;

namespace Abacus.UI
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
            get { return Permissions.MethodTest; }
        }

        public Guid WorkItemId { get; set; }

        public Guid MethodTestId { get; set; }
        public Guid MethodId { get; set; }
        public string Description { get; set; }
        public decimal ExpectedResult { get; set; }
    }
}
