using Shuttle.Abacus.Messages;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Server
{
    public static class MessageSenderExtensions
    {
        public static void ReplyOK(this IMessageSender context)
        {
            Guard.AgainstNull(context, "context");

            context.Send(new ReplyMessage(), c => c.Reply());
        }
    }
}