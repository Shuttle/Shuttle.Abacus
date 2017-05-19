using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.Handlers
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
