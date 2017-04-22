using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Handlers
{
    public class SystemUserHandler : Esb.IMessageHandler<LoginCompletedEvent>
    {
        private readonly ISession session;
        private readonly IMessageBus messageBus;

        public SystemUserHandler(ISession session, IMessageBus messageBus)
        {
            this.session = session;
            this.messageBus = messageBus;
        }

        public void ProcessMessage(IHandlerContext<LoginCompletedEvent> context)
        {
            var permissions = new PermissionCollection();

            context.Message.Permissions.ForEach(permissions.Add);

            session.Permissions = permissions;

            messageBus.Publish(new ConfigureShellMessage());
        }
    }
}
