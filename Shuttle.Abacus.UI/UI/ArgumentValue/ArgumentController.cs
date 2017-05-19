using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.ArgumentValue;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.UI.ArgumentValue
{
    public class ArgumentValueController : WorkItemController, IArgumentValueController
    {
        public ArgumentValueController(IServiceBus serviceBus, IMessageBus messageBus)
            : base(serviceBus, messageBus)
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
                ArgumentId = message.ArgumentId,
                Value = message.Value
            });
        }
    }
}
