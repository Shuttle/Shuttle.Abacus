using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Limit;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.UI.Limit;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class LimitController : WorkItemController, ILimitController
    {
        public LimitController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository) 
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(NewLimitMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<ILimitView>();

            var command = new CreateLimitCommand
                          {
                              OwnerName = message.OwnerName,
                              OwnerId = message.OwnerId,
                              Type = view.TypeValue,
                              Name = view.LimitNameValue
                          };

            Send(command);
        }

        public void HandleMessage(EditLimitMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<ILimitView>();

            var command = new ChangeLimitCommand
                          {
                              LimitId = message.LimitId,
                              Type = view.TypeValue,
                              Name = view.LimitNameValue
                          };

            Send(command, () =>
                          MessageBus.Publish(
                              new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }

        public void HandleMessage(DeleteLimitMessage message)
        {
            Send(new DeleteLimitCommand
                 {
                     LimitId = message.LimitId
                 },
                 () => MessageBus.Publish(new ResourceRefreshItemMessage(message.OwnerResource)));
        }
    }
}
