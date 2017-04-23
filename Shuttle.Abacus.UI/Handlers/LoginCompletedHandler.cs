using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Handlers
{
    public class LoginCompletedHandler : IMessageHandler<LoginCompletedEvent>
    {
        private readonly IMessageBus _messageBus;

        public LoginCompletedHandler(IMessageBus messageBus)
        {
            Guard.AgainstNull(messageBus, "messageBus");

            _messageBus = messageBus;
        }

        public void HandleMessage(LoginCompletedEvent message)
        {
            _messageBus.Publish(new ActivateShellMessage());
        }
    }
}