using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.SystemUser
{
    public class ListSystemUserMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.SystemUser; }
        }
    }
}
