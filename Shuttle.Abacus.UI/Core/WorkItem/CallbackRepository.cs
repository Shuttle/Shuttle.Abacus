using System;
using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public class CallbackRepository : ICallbackRepository
    {
        private readonly Dictionary<string, Action> _callbacks = new Dictionary<string, Action>();
        private readonly IMessageBus _messageBus;
        private readonly Action _null = () => { };

        public CallbackRepository(IMessageBus messageBus)
        {
            Guard.AgainstNull(messageBus, "messageBus");

            _messageBus = messageBus;
        }

        public string Register(IWorkItem workItem, Action action, bool complete)
        {
            var id = Guid.NewGuid().ToString();

            _callbacks.Add(id, () =>
            {
                workItem?.Ready();

                action.Invoke();

                if (!complete || workItem == null)
                {
                    return;
                }

                _messageBus.Publish(new RefreshWorkItemDispatcherMessage(workItem));

                _messageBus.Publish(new WorkItemCompletedMessage(workItem));
            });

            return id;
        }

        public Action Find(string id)
        {
            return _callbacks.ContainsKey(id)
                ? _callbacks[id]
                : _null;
        }

        public void Remove(string id)
        {
            _callbacks.Remove(id);
        }
    }
}