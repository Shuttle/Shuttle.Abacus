using System.Windows.Forms;
using Abacus.Messages;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Handlers
{
    public class LoginTimeoutHandler : Esb.IMessageHandler<LoginTimeoutCommand>
    {
        private readonly IMessageBus _messageBus;

        public LoginTimeoutHandler(IMessageBus messageBus)
        {
            Guard.AgainstNull(messageBus, "messageBus");

            _messageBus = messageBus;
        }

        public void ProcessMessage(IHandlerContext<LoginTimeoutCommand> context)
        {
            Program.CloseSplash();

            MessageBox.Show("It took too long to try to log you on.  Please try again.", "Login Failure",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            Application.Exit();
            //_messageBus.Publish(new ShutdownApplication());
        }
    }
}