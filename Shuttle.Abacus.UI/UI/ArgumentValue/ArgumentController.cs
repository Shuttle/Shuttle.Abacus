using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.ArgumentValue;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.UI.ArgumentValue
{
    public class ArgumentValueController : WorkItemController, IArgumentValueController
    {
        public ArgumentValueController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository)
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(RegisterArgumentValueMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IArgumentValueView>();

            var command = new RegisterArgumentValueCommand
            {
                ArgumentId = message.ArgumentId,
                Value = view.ValueValue
            };

            Send(command);
        }

        public void HandleMessage(RemoveArgumentValueMessage message)
        {
            Send(new RemoveArgumentValueCommand
            {
                ArgumentId = message.OwnerResource.Key,
                Value = message.Value
            },
                 () => MessageBus.Publish(new ResourceRefreshItemMessage(message.OwnerResource)));
        }
    }
}
