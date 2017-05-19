using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Shell.Messages.Core
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
