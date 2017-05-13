using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.SystemUser
{
    public class NewSystemUserMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.SystemUser;
    }
}
