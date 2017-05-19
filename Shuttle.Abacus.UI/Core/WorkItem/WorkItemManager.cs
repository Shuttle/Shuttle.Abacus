using System;
using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.WorkItem;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.WorkItem
{
    public class WorkItemManager : IWorkItemManager
    {
        private readonly IMessageBus _messageBus;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IApplicationShell _applicationShell;

        private readonly IWorkItemControllerFactory _workItemControllerFactory;
        private readonly IWorkItemPresenterFactory _workItemPresenterFactory;
        private readonly Dictionary<Guid, IWorkItem> store = new Dictionary<Guid, IWorkItem>();

        public WorkItemManager(IWorkItemControllerFactory workItemControllerFactory,
            IWorkItemPresenterFactory workItemPresenterFactory, IPresenterFactory presenterFactory,
            IMessageBus messageBus, IApplicationShell applicationShell)
        {
            Guard.AgainstNull(workItemControllerFactory, "workItemControllerFactory");
            Guard.AgainstNull(workItemPresenterFactory, "workItemPresenterFactory");
            Guard.AgainstNull(presenterFactory, "presenterFactory");
            Guard.AgainstNull(messageBus, "messageBus");

            _workItemControllerFactory = workItemControllerFactory;
            _workItemPresenterFactory = workItemPresenterFactory;
            _presenterFactory = presenterFactory;
            _messageBus = messageBus;
            _applicationShell = applicationShell;

            messageBus.AddSubscriber(this);
        }

        public void Add(IWorkItem workItem)
        {
            store.Add(workItem.Id, workItem);
        }

        public IWorkItem Get(Guid id)
        {
            return store.ContainsKey(id) ? store[id] : null;
        }

        public IWorkItemBuilderController Create(string text)
        {
            return new WorkItemBuilder(text, this, _workItemControllerFactory, _workItemPresenterFactory);
        }

        public IWorkItemBuilderController Create(Guid workItemId, string text)
        {
            return new WorkItemBuilder(workItemId, text, this, _workItemControllerFactory, _workItemPresenterFactory);
        }

        public void TextChanged(WorkItem workItem)
        {
            _messageBus.Publish(new WorkItemTextChangedMessage(workItem));
        }

        public INavigationItemContainer<IPresenter> BuildPresenter<T>() where T : IPresenter
        {
            return _presenterFactory.Create<T>();
        }

        public void Waiting(WorkItem workItem)
        {
            _messageBus.Publish(new WorkItemBusyMessage(workItem));
        }

        public void Ready(WorkItem workItem)
        {
            _messageBus.Publish(new WorkItemReadyMessage(workItem));
        }

        public bool Contains(Guid id)
        {
            return store.ContainsKey(id);
        }

        public IPresenter CreatePresenter<T>() where T : IPresenter
        {
            return _presenterFactory.Create<T>();
        }

        public void Invoke(Action action)
        {
            _applicationShell.Invoke(action);
        }

        public void HandleMessage(WorkItemCompletedMessage message)
        {
            if (message.WorkItem == null)
            {
                return;
            }

            store.Remove(message.WorkItem.Id);
        }
    }
}