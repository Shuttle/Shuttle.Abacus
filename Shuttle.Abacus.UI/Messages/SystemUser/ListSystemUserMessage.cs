using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.SystemUser
{
    public class ListSystemUserMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.SystemUser;
    }
}
