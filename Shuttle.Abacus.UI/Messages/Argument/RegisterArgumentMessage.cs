using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Argument
{
    public class RegisterArgumentMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Argument;
    }
}