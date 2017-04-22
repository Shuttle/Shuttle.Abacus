using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public abstract class WorkItemController : IWorkItemController
    {
        protected WorkItemController(IServiceBus serviceBus, IMessageBus messageBus)
        {
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

        public void Dispose()
        {
            MessageBus.RemoveSubscriber(this);
        }

        protected void SetWorkItemWaiting()
        {
            if (WorkItem == null)
            {
                return;
            }

            WorkItem.Waiting();
        }

        protected void Send(object command)
        {
            ServiceBus.Send(command);
        }
    }
}