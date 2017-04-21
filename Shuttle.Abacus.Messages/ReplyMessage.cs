using Abacus.Infrastructure;
using NServiceBus;

namespace Abacus.Messages
{
    public class ReplyMessage : IMessage
    {
        public Result Result { get; set; }

        public ReplyMessage(IResult result)
        {
            Guard.AgainstNull(result, "result");

            Result = (Result)result;
        }
    }
}
