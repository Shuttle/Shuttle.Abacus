using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Argument;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.UI.Argument
{
    public class ArgumentController : WorkItemController, IArgumentController
    {
        public ArgumentController(IServiceBus serviceBus, IMessageBus messageBus) 
            : base(serviceBus, messageBus)
        {
        }

        public void HandleMessage(RegisterArgumentMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IArgumentView>();

            var command = new RegisterArgumentCommand
                          {
                              Name = view.ArgumentNameValue,
                              AnswerType = view.AnswerTypeValue
                          };

            Send(command);
        }

        public void HandleMessage(RenameArgumentMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IArgumentView>();

            var command = new RenameArgumentCommand
                          {
                              ArgumentId = message.ArgumentId,
                              Name = view.ArgumentNameValue
                          };

            Send(command);
        }

        public void HandleMessage(RemoveArgumentMessage message)
        {
            Send(new RemoveArgumentCommand
                 {
                     ArgumentId = message.ArgumentId
                 });
        }
    }
}
