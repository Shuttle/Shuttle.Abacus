using Abacus.Messages;

namespace Abacus.UI
{
    public class LimitController : WorkItemController, ILimitController
    {
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
