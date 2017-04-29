using System;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public abstract class WorkItemController : IWorkItemController
    {
        private readonly ICallbackRepository _callbackRepository;

        protected WorkItemController(IServiceBus serviceBus, IMessageBus messageBus, ICallbackRepository callbackRepository)
        {
            Guard.AgainstNull(serviceBus, "serviceBus");
            Guard.AgainstNull(messageBus, "messageBus");
            Guard.AgainstNull(callbackRepository, "callbackRepository");

            _callbackRepository = callbackRepository;

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
            Send(command, () => { });
        }

        protected void Send(object command, Action action)
        {
            Send(command, action, true);
        }

        protected void Send(object command, Action action, bool complete)
        {
            SetWorkItemWaiting();

            var callback = _callbackRepository.Register(WorkItem, action, complete);

            ServiceBus.Send(command, c => c.Headers.Add(new TransportHeader {Key = "__callback", Value = callback}));
        }
    }
}