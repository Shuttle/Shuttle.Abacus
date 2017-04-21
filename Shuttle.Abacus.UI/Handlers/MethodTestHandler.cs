using Abacus.Messages;

namespace Abacus.UI
{
    public class MethodTestHandler : UIHandler,
                                   NServiceBus.IMessageHandler<MethodTestRunEvent>,
                                   NServiceBus.IMessageHandler<MethodTestPrintEvent>
    {
        public void Handle(MethodTestRunEvent message)
        {
            MessageBus.Publish(new MethodTestRunMessage(message.WorkItemId, message));
        }

        public void Handle(MethodTestPrintEvent message)
        {
            MessageBus.Publish(new MethodTestPrintMessage(message));
        }
    }
}
