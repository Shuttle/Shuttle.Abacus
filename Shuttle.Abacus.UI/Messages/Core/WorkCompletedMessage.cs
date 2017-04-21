using Abacus.Infrastructure;

namespace Abacus.UI
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
