using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Messaging;

namespace Shuttle.Abacus.Shell.Messages.Section
{
    public class NewMethodMessage : Message
    {
        public override IPermission RequiredPermission => Permissions.Formula;
    }
}