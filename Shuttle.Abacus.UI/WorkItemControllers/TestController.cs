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
    public class TestController : WorkItemController, ITestController
    {
        public TestController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository) 
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(NewTestMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<ITestView>();

            if (view.HasInvalidArgumentAnswers())
            {
                WorkItem.GetPresenter<ITestPresenter>().ShowInvalidArgumentAnswersMessage();

                return;
            } 
            
            var command = new CreateTestCommand
                          {
                              MethodTestId = Guid.NewGuid(),
                              MethodId = message.MethodId,
                              Description = view.DescriptionValue,
                              ExpectedResult = view.ExpectedResultValue
                          };

            foreach (var model in view.ArgumentAnswers)
            {
                command.ArgumentAnswers.Add(new Abacus.Messages.v1.TransferObjects.ArgumentAnswer
                                          {
                                              ArgumentId = model.ArgumentId,
                                              Answer = model.Answer
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
                 MessageBus.Publish(new TestCreatedMessage(message.WorkItemId, message.MethodId)));
        }

        public void HandleMessage(ChangeTestMessage message)
        {
            var view = WorkItem.GetView<ITestView>();

            if (view.HasInvalidArgumentAnswers())
            {
                WorkItem.GetPresenter<ITestPresenter>().ShowInvalidArgumentAnswersMessage();

                return;
            }
            
            var command = new ChangeTestCommand
                          {
                              MethodTestId = message.MethodTestId,
                              MethodId = message.MethodId,
                              Description = view.DescriptionValue,
                              ExpectedResult = view.ExpectedResultValue
                          };

            foreach (var model in view.ArgumentAnswers)
            {
                command.ArgumentAnswers.Add(new Shuttle.Abacus.Messages.v1.TransferObjects.ArgumentAnswer
                {
                    ArgumentId = model.ArgumentId,
                    Answer = model.Answer
                });
            }

            Send(command,
                 () =>
                 MessageBus.Publish(new TestChangedMessage(message.WorkItemId, message.MethodId)));
        }
    }
}
