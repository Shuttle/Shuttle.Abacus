using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Messaging
{
    public class ReplyMessage : NullPermissionMessage
    {
        public IResult Result { get; private set; }

        public ReplyMessage(IResult result)
        {
            Guard.AgainstNull(result, "result");

            Result = result;
        }
    }
}