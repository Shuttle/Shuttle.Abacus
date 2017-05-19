using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Core
{
    public class WorkStartedMessage : Message
    {
        public static readonly WorkStartedMessage Instance = new WorkStartedMessage();
 
        public override IPermission RequiredPermission => Permissions.Null;
    }
}
