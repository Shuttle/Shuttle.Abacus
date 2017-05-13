using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class DeleteTestMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Test;

        public Guid MethodTestId { get; set; }
        public Guid MethodId { get; set; }
        public string Description { get; set; }
        public string ExpectedResult { get; set; }
    }
}
