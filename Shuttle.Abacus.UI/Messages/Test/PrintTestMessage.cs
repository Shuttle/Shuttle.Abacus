using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class PrintTestMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Test;
    }
}