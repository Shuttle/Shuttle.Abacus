using Abacus.Messages;

namespace Abacus.UI
{
    public class ArgumentController : WorkItemController, IArgumentController
    {
        public void HandleMessage(NewArgumentMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IArgumentView>();
            var mappingListView = WorkItem.GetView<IArgumentRestrictedAnswerView>();

            var command = new CreateArgumentCommand
                          {
                              Name = view.ArgumentNameValue,
                              AnswerType = view.AnswerTypeValue,
                              Answers = mappingListView.ArgumentAnswerCatalog
                          };

            Send(command);
        }

        public void HandleMessage(EditArgumentMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IArgumentView>();
            var mappingListView = WorkItem.GetView<IArgumentRestrictedAnswerView>();

            var command = new ChangeArgumentCommand
                          {
                              ArgumentId = message.ArgumentId,
                              Name = view.ArgumentNameValue,
                              AnswerType = view.AnswerTypeValue,
                              ArgumentAnswers = mappingListView.ArgumentAnswerCatalog
                          };

            Send(command, () =>
                          MessageBus.Publish(
                              new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }

        public void HandleMessage(DeleteArgumentMessage message)
        {
            Send(new DeleteArgumentCommand
                 {
                     ArgumentId = message.ArgumentId
                 },
                 () => MessageBus.Publish(new ResourceRefreshItemMessage(message.OwnerResource)));
        }
    }
}
