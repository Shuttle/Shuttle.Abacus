using Abacus.Infrastructure;
using NServiceBus;

namespace Abacus.Messages
{
    public class NotificationMessage : IMessage
    {
        public Result Result { get; set; }

        public NotificationMessage(IResult result)
        {
            Guard.AgainstNull(result, "result");

            Result = (Result)result;
        }
    }
}
