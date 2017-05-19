using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Test
{
    public class RegisterTestMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Test;
    }
}