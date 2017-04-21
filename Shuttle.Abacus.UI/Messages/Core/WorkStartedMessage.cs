using Abacus.Infrastructure;

namespace Abacus.UI
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
