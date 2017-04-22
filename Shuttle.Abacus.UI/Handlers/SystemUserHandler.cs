using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Handlers
{
    public class SystemUserHandler : NServiceBus.IMessageHandler<LoginCompletedEvent>
    {
        private readonly ISession session;
        private readonly IMessageBus messageBus;

        public SystemUserHandler(ISession session, IMessageBus messageBus)
        {
            this.session = session;
            this.messageBus = messageBus;
        }

        public void Handle(LoginCompletedEvent message)
        {
            var permissions = new PermissionCollection();

            message.Permissions.ForEach(permissions.Add);

            session.Permissions = permissions;

            messageBus.Publish(new ConfigureShellMessage());
        }
    }
}
