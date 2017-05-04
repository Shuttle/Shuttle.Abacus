using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Messages.v1;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.TestCase;
using Shuttle.Abacus.UI.UI.List;
using Shuttle.Abacus.UI.WorkItemControllers.Interfaces;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.WorkItemControllers
{
    public class MethodTestManagerController : WorkItemController, IMethodTestManagerController
    {
        public MethodTestManagerController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository) 
            : base(serviceBus, messageBus, callbackRepository)
        {
        }

        public void HandleMessage(ListReadyMessage message)
        {
            WorkItem.GetView<ISimpleListView>().ShowCheckboxes();
        }

        public void HandleMessage(NewMethodTestMessage message)
        {
            message.WorkItemId = WorkItem.Id;

            MessageBus.Publish(message);
        }

        public void HandleMessage(EditMethodTestMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasSelectedItem)
            {
                return;
            }

            var item = view.SelectedItem();

            message.MethodTestId = new Guid(item.Name);
            message.WorkItemId = WorkItem.Id;

            MessageBus.Publish(message);
        }

        public void HandleMessage(RemoveMethodTestMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasElectedItems)
            {
                return;
            }

            if (!view.Confirmed(string.Format("Are you sure that you want to remove the selected test case(s)?")))
            {
                return;
            }

            message.WorkItemId = WorkItem.Id;

            var command = new DeleteMethodTestCommand();

            foreach (var item in view.ElectedItems)
            {
                command.MethodTestIds.Add(new Guid(item.Name));
            }

            Send(command,
                           () =>
                           MessageBus.Publish(new MethodTestRemovedMessage(WorkItem.Id, message.MethodId)));
        }

        public void HandleMessage(MarkAllMessage message)
        {
            WorkItem.GetView<ISimpleListView>().MarkAll();
        }

        public void HandleMessage(InvertMarksMessage message)
        {
            WorkItem.GetView<ISimpleListView>().InvertMarks();
        }

        public void HandleMessage(RunMethodTestMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasElectedItems)
            {
                return;
            }

            var command = new RunMethodTestCommand(WorkItem.Id);

            foreach (var item in view.ElectedItems)
            {
                command.MethodTestIds.Add(new Guid(item.Name));
            }

            Send(command);
        }

        public void HandleMessage(NewMethodTestFromExistingMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasSelectedItem)
            {
                return;
            }

            var item = view.SelectedItem();

            message.MethodTestId = new Guid(item.Name);
            message.WorkItemId = WorkItem.Id;

            MessageBus.Publish(message);
        }

        public void HandleMessage(PrintMethodTestMessage message)
        {
            var view = WorkItem.GetView<ISimpleListView>();

            if (!view.HasElectedItems)
            {
                return;
            }

            var command = new PrintMethodTestCommand(WorkItem.Id);

            foreach (var item in view.ElectedItems)
            {
                command.MethodTestIds.Add(new Guid(item.Name));
            }

            Send(command);
        }
    }
}
