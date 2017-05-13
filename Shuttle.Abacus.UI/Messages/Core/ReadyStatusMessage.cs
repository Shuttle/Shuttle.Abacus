using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Core
{
    public class ReadyStatusMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Null;
    }
}
