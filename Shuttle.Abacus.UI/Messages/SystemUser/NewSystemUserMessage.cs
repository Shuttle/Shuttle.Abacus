using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.SystemUser
{
    public class NewSystemUserMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.SystemUser;
    }
}
