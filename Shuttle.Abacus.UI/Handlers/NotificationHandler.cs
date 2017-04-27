using Shuttle.Abacus.Messages;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Handlers
{
    public class NotificationHandler : Esb.IMessageHandler<NotificationMessage>
    {
        private readonly IMessageBus messageBus;

        public NotificationHandler(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public void ProcessMessage(IHandlerContext<NotificationMessage> context)
        {
            messageBus.Publish(new ResultNotificationMessage(context.Message.Result));
        }
    }
}
