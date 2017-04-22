using Abacus.Messages;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Handlers
{
    public class ReplyMessageHandler : Esb.IMessageHandler<ReplyMessage>
    {
        private readonly IMessageBus _messageBus;

        public ReplyMessageHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void ProcessMessage(IHandlerContext<ReplyMessage> context)
        {
            var message = context.Message;
            var result = message.Result;

            if (result == null)
            {
                return;
            }

            foreach (var header in context.TransportMessage.Headers)
            {
                result.Headers.Add(new ResultHeader
                {
                    Key = header.Key,
                    Value = header.Value
                });
            }

            if (result.HasMessages)
            {
                _messageBus.Publish(result);
            }
        }
    }
}