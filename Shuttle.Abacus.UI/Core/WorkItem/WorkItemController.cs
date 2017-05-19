using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.WorkItem;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.Shell.Core.WorkItem
{
    public abstract class WorkItemController : IWorkItemController
    {
        protected WorkItemController(IServiceBus serviceBus, IMessageBus messageBus)
        {
            Guard.AgainstNull(serviceBus, "serviceBus");
            Guard.AgainstNull(messageBus, "messageBus");

            ServiceBus = serviceBus;
            MessageBus = messageBus;
        }

        protected IServiceBus ServiceBus { get; }
        protected IMessageBus MessageBus { get; }
        protected IWorkItem WorkItem { get; private set; }


        public void AssignWorkItem(IWorkItem workItem)
        {
            Guard.AgainstReassignment(WorkItem, "WorkItem");

            WorkItem = workItem;
        }

        protected void Send(object command)
        {
            ServiceBus.Send(command);

            if (WorkItem == null)
            {
                return;
            }

            MessageBus.Publish(new RefreshWorkItemDispatcherMessage(WorkItem));
            MessageBus.Publish(new WorkItemCompletedMessage(WorkItem));
        }
    }
}