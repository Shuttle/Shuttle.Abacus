﻿using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.FactorAnswer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.UI.Argument;
using Shuttle.Abacus.UI.UI.Argument.RestrictedAnswer;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;

namespace Shuttle.Abacus.UI.WorkItemControllers
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
                          _messageBus.Publish(
                              new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }

        public void HandleMessage(DeleteArgumentMessage message)
        {
            Send(new DeleteArgumentCommand
                 {
                     ArgumentId = message.ArgumentId
                 },
                 () => _messageBus.Publish(new ResourceRefreshItemMessage(message.OwnerResource)));
        }
    }
}
