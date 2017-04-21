using System;
using System.Windows.Forms;
using Abacus.DTO;
using Abacus.Infrastructure;
using Abacus.Messages;

namespace Abacus.UI
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
                 MessageBus.Publish(new MethodTestChangedMessage(message.WorkItemId, message.MethodId)));
        }
    }
}
