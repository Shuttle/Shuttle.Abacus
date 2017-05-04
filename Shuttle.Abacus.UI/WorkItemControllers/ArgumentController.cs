using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.FactorAnswer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.UI.Argument;
using Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class ArgumentController : WorkItemController, IArgumentController
    {
        public ArgumentController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository) 
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

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
                              Answers = mappingListView.Answers
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
                              Answers = mappingListView.Answers
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
