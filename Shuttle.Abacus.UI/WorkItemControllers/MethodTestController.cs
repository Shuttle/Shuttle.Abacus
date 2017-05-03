using System;
using System.Data;
using System.Windows.Forms;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.TestCase;
using Shuttle.Abacus.UI.UI.MethodTest;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class MethodTestController : WorkItemController, IMethodTestController
    {
        public MethodTestController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository) 
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(NewMethodTestMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IMethodTestView>();

            if (view.HasInvalidArgumentAnswers())
            {
                WorkItem.GetPresenter<IMethodTestPresenter>().ShowInvalidArgumentAnswersMessage();

                return;
            } 
            
            var command = new CreateMethodTestCommand
                          {
                              MethodTestId = Guid.NewGuid(),
                              MethodId = message.MethodId,
                              Description = view.DescriptionValue,
                              ExpectedResult = view.ExpectedResultValue
                          };

            foreach (ListViewItem item in view.ArgumentAnswers)
            {
                var dto = ((DataRow) item.Tag);

                command.ArgumentAnswers.Add(new ArgumentAnswerDTO
                                          {
                                              ArgumentId = dto.Id,
                                              ArgumentName = item.Text, 
                                              AnswerType = dto.AnswerType,
                                              Answer = item.SubItems[1].Text
                                          });
            }

            if (command.ArgumentAnswers.Count == 0)
            {
                MessageBus.Publish(
                    new ResultNotificationMessage(Result.Create().AddFailureMessage("Please add at least one input.")));

                return;
            }

            Send(command,
                 () =>
                 MessageBus.Publish(new MethodTestCreatedMessage(message.WorkItemId, message.MethodId)));
        }

        public void HandleMessage(ChangeMethodTestMessage message)
        {
            var view = WorkItem.GetView<IMethodTestView>();

            if (view.HasInvalidArgumentAnswers())
            {
                WorkItem.GetPresenter<IMethodTestPresenter>().ShowInvalidArgumentAnswersMessage();

                return;
            }
            
            var command = new ChangeMethodTestCommand
                          {
                              MethodTestId = message.MethodTestId,
                              MethodId = message.MethodId,
                              Description = view.DescriptionValue,
                              ExpectedResult = view.ExpectedResultValue
                          };

            foreach (ListViewItem item in view.ArgumentAnswers)
            {
                var dto = ((DataRow)item.Tag);

                command.ArgumentAnswers.Add(new ArgumentAnswer
                {
                    ArgumentId = dto.Id,
                    Answer = item.SubItems[1].Text
                });
            }

            Send(command,
                 () =>
                 MessageBus.Publish(new MethodTestChangedMessage(message.WorkItemId, message.MethodId)));
        }
    }
}
