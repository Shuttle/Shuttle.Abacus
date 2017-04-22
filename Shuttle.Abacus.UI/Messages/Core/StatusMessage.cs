using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Core
{
    public class StatusMessage : Message
    {
        public string Message { get; private set; }

        public StatusMessage(string message)
        {
            Message = message;
        }

        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
