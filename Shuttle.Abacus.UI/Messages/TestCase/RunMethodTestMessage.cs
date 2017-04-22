using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Messages.TestCase
{
    public class RunMethodTestMessage : Message
    {
        public override IPermission RequiredPermission
        {
            get { return Permissions.MethodTest; }
        }
    }
}