using System.Windows.Forms;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Permission = Shuttle.Abacus.Infrastructure.Permission;

namespace Shuttle.Abacus.Shell.Handlers
{
    public class SystemUserHandler : 
        Esb.IMessageHandler<LoginCompletedEvent>,
        Esb.IMessageHandler<LoginTimeoutCommand>
    {
        private readonly IMessageBus _messageBus;
        private readonly ISession _session;
        private readonly object _lock = new object();
        private static bool _loginTimeout;
        private static bool _loginCompleted;

        public SystemUserHandler(ISession session, IMessageBus messageBus)
        {
            Guard.AgainstNull(session, "session");
            Guard.AgainstNull(messageBus, "messageBus");

            _session = session;
            _messageBus = messageBus;
        }

        public void ProcessMessage(IHandlerContext<LoginCompletedEvent> context)
        {
            lock (_lock)
            {
                if (_loginTimeout)
                {
                    return;
                }

                _loginCompleted = true;
            }

            Program.CloseSplash();

            var permissions = new PermissionCollection();

            context.Message.Permissions.ForEach(
                permission => permissions.Add(new Permission(permission.Identifier, permission.Description)));

            _session.Permissions = permissions;

            _messageBus.Publish(new ConfigureShellMessage());
            _messageBus.Publish(new ActivateShellMessage());
        }

        public void ProcessMessage(IHandlerContext<LoginTimeoutCommand> context)
        {
            lock (_lock)
            {
                if (_loginCompleted)
                {
                    return;
                }

                _loginTimeout = true;
            }

            Program.CloseSplash();

            MessageBox.Show("It took too long to try to log you on.  Please try again.", "Login Failure",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            Application.Exit();
        }
    }
}