using System;
using System.Collections.Generic;
using Abacus.Messages;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public abstract class WorkItemController : IWorkItemController, Messaging.IMessageHandler<ReplyMessage>
    {
        private readonly Dictionary<string, Action> _callbacks = new Dictionary<string, Action>();

        protected WorkItemController(IServiceBus serviceBus, IMessageBus messageBus)
        {
            ServiceBus = serviceBus;
            MessageBus = messageBus;

            MessageBus.AddSubscriber(this);
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

        protected void Send(object command, Action action)
        {
            SetWorkItemWaiting();

            var callback = RegisterCallback(action);

            ServiceBus.Send(command, c => c.Headers.Add(new TransportHeader { Key = "__callback", Value = callback }));
        }

        private string RegisterCallback(Action action)
        {
            var id = Guid.NewGuid().ToString();

            _callbacks.Add(id, action);

            return id;
        }

        public void HandleMessage(ReplyMessage message)
        {
            var header = message.Result.Headers.Find(item=> item.Key.Equals("__callback", StringComparison.OrdinalIgnoreCase));

            if (header == null)
            {
                return;
            }

            if (!_callbacks.ContainsKey(header.Value))
            {
                return;
            }

            _callbacks[header.Value]();

            _callbacks.Remove(header.Value);
        }
    }
}