using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Core
{
    public class NullPermissionMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Null;
    }
}
