using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class PrintMethodTestMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.Test; }
        }
    }
}