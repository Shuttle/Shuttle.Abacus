using System;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class DeleteMethodTestMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }

        public Guid MethodTestId { get; set; }
        public Guid MethodId { get; set; }
        public string Description { get; set; }
        public string ExpectedResult { get; set; }
    }
}
