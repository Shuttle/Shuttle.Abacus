using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class RunTestMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Test; }
        }
    }
}