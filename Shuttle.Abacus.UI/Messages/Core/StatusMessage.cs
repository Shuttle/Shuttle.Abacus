using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Core
{
    public class StatusMessage : Message
    {
        public string Message { get; private set; }

        public StatusMessage(string message)
        {
            Message = message;
        }

        public override IPermission RequiredPermission => Permissions.Null;
    }
}
