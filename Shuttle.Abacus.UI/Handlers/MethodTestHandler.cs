using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Messages.TestCase;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Handlers
{
    public class MethodTestHandler : UIHandler,
        IMessageHandler<TestRunEvent>,
        IMessageHandler<MethodTestPrintEvent>
    {
        public void ProcessMessage(IHandlerContext<MethodTestPrintEvent> context)
        {
            MessageBus.Publish(new TestPrintMessage(context.Message));
        }

        public void ProcessMessage(IHandlerContext<TestRunEvent> context)
        {
            var message = context.Message;

            MessageBus.Publish(new TestRunMessage(message.WorkItemId, message));
        }
    }
}