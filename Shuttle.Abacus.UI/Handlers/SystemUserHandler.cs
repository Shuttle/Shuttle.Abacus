using Abacus.Infrastructure;
using Abacus.Messages;

namespace Abacus.UI
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
