using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Core
{
    public class WorkCompletedMessage : Message
    {
        public static readonly WorkCompletedMessage Instance = new WorkCompletedMessage();

        public override IPermission RequiredPermission => Permissions.Null;
    }
}