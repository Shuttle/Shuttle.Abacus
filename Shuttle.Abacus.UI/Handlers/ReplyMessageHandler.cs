using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Handlers
{
    public class ReplyMessageHandler : Esb.IMessageHandler<Abacus.Messages.ReplyMessage>
    {
        private readonly IMessageBus _messageBus;

        public ReplyMessageHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void ProcessMessage(IHandlerContext<Abacus.Messages.ReplyMessage> context)
        {
            var message = context.Message;
            var result = new Result
            {
                SuccessMessages = message.SuccessMessages,
                FailureMessages = message.FailureMessages
            };

            foreach (var header in context.TransportMessage.Headers)
            {
                result.Headers.Add(new ResultHeader
                {
                    Key = header.Key,
                    Value = header.Value
                });
            }

            _messageBus.Publish(new ReplyMessage(result));
        }
    }
}