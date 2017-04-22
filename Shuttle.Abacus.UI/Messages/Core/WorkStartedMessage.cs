using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.Core
{
    public class WorkStartedMessage : Message
    {
        public static readonly WorkStartedMessage Instance = new WorkStartedMessage();
 
        public override IPermission RequiredPermission
        {
            get { return Permissions.Null; }
        }
    }
}
