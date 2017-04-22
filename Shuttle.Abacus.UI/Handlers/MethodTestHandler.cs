using Shuttle.Abacus.Domain;
using Shuttle.Abacus.UI.Messages.TestCase;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Handlers
{
    public class MethodTestHandler : UIHandler,
        IMessageHandler<MethodTestRunEvent>,
        IMessageHandler<MethodTestPrintEvent>
    {
        public void ProcessMessage(IHandlerContext<MethodTestPrintEvent> context)
        {
            MessageBus.Publish(new MethodTestPrintMessage(context.Message));
        }

        public void ProcessMessage(IHandlerContext<MethodTestRunEvent> context)
        {
            var message = context.Message;

            MessageBus.Publish(new MethodTestRunMessage(message.WorkItemId, message));
        }
    }
}