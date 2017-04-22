using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Section
{
    public class NewMethodMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Method; }
        }
    }
}