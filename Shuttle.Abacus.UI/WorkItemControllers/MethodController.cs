using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.Section;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.UI.Method;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class MethodController : WorkItemController, IMethodController
    {
        public void HandleMessage(NewMethodMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IMethodView>();

            var command = new CreateMethodCommand
                          {
                              MethodName= view.MethodNameValue
                          };

            Send(command);
        }

        public void HandleMessage(EditMethodMessage message)
        {
            var view = WorkItem.GetView<IMethodView>();

            var command = new ChangeMethodCommand
                          {
                              MethodId = message.MethodId,
                              MethodName = view.MethodNameValue
                          };


            Send(command,
                 () =>
                 MessageBus.Publish(new RefreshWorkItemDispatcherTextMessage(WorkItem.Initiator.WorkItemInitiatorId)));
        }

        public void HandleMessage(NewMethodFromExistingMessage message)
        {
            if (!WorkItem.PresentationValid())
            {
                return;
            }

            var view = WorkItem.GetView<IMethodView>();

            var command = new CopyMethodCommand
                          {
                              MethodId = message.MethodId,
                              MethodName = view.MethodNameValue
                          };

            Send(command);
        }

        public void HandleMessage(DeleteMethodMessage message)
        {
            Send(new DeleteMethodCommand
                 {
                     MethodId = message.MethodId
                 },
                 () => MessageBus.Publish(new ResourceRefreshItemMessage(message.OwnerResource)));
        }
    }
}
