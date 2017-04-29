using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Messaging
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