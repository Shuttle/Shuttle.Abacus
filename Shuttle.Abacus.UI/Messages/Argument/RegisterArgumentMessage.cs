using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Argument
{
    public class RegisterArgumentMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Argument;
    }
}