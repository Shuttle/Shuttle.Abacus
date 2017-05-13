using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Core
{
    public class NullPermissionMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Null;
    }
}
