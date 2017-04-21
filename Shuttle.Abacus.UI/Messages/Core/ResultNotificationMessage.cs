using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ResultNotificationMessage : NullPermissionMessage
    {
        public IAbstractResult Result { get; private set; }

        public ResultNotificationMessage(IAbstractResult result)
        {
            Result = result;
        }
    }
}
