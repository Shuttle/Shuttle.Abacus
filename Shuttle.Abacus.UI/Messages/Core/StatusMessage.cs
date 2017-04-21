using Abacus.Infrastructure;

namespace Abacus.UI
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
