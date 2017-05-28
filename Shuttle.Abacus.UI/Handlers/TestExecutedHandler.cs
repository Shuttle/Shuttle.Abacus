using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.Test;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.Handlers
{
    public class TestExecutedHandler : Esb.IMessageHandler<TestExecutedEvent>
    {
        private readonly IMessageBus _messageBus;

        public TestExecutedHandler(IMessageBus messageBus)
        {
            Guard.AgainstNull(messageBus, "_messageBus");

            this._messageBus = messageBus;
        }

        public void ProcessMessage(IHandlerContext<TestExecutedEvent> context)
        {
            var message = context.Message;

            _messageBus.Publish(new TestExecutedMessage(message.Id, message.FormulaName, message.Result));
        }
    }
}