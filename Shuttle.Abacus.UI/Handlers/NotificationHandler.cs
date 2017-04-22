using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Handlers
{
    public class NotificationHandler : NServiceBus.IMessageHandler<NotificationMessage>
    {
        private readonly IMessageBus messageBus;

        public NotificationHandler(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public void Handle(NotificationMessage message)
        {
            messageBus.Publish(new ResultNotificationMessage(message.Result));
        }
    }
}
