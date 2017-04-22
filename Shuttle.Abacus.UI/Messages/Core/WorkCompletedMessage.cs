using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Core
{
    public class WorkCompletedMessage : Message
    {
        public static readonly WorkCompletedMessage Instance = new WorkCompletedMessage();

        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
