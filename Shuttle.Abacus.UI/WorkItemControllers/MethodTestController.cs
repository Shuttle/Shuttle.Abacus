using System;
using System.Windows.Forms;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.TestCase;
using Shuttle.Abacus.UI.UI.MethodTest;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class MethodTestController : WorkItemController, IMethodTestController
    {
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
                var dto = ((ArgumentDTO) item.Tag);

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
                _messageBus.Publish(
                    new ResultNotificationMessage(Result.Create().AddFailureMessage("Please add at least one input.")));

                return;
            }

            Send(command,
                 () =>
                 _messageBus.Publish(new MethodTestCreatedMessage(message.WorkItemId, message.MethodId)));
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
                var dto = ((ArgumentDTO)item.Tag);

                command.ArgumentAnswers.Add(new ArgumentAnswerDTO
                {
                    ArgumentId = dto.Id,
                    ArgumentName = item.Text,
                    AnswerType = dto.AnswerType,
                    Answer = item.SubItems[1].Text
                });
            }

            Send(command,
                 () =>
                 _messageBus.Publish(new MethodTestChangedMessage(message.WorkItemId, message.MethodId)));
        }
    }
}
