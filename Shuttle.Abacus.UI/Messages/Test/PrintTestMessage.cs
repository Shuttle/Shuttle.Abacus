using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Test
{
    public class PrintTestMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Test;
    }
}