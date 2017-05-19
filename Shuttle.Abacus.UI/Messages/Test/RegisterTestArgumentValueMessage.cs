using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Test
{
    public class RegisterTestArgumentValueMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Test;
    }
}