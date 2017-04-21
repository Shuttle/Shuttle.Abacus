using System;
using System.Collections.Generic;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class WorkItemManager : IWorkItemManager
    {
        private readonly Dictionary<Guid, IWorkItem> store = new Dictionary<Guid, IWorkItem>();

        private readonly IWorkItemControllerFactory workItemControllerFactory;
        private readonly IWorkItemPresenterFactory workItemPresenterFactory;
        private readonly IPresenterFactory presenterFactory;
        private readonly IMessageBus messageBus;
        private readonly IShell shell;

        public WorkItemManager(IWorkItemControllerFactory workItemControllerFactory, IWorkItemPresenterFactory workItemPresenterFactory, IPresenterFactory presenterFactory, IMessageBus messageBus, IShell shell)
        {
            Guard.AgainstNull(workItemControllerFactory, "workItemControllerFactory");
            Guard.AgainstNull(workItemPresenterFactory, "workItemPresenterFactory");
            Guard.AgainstNull(presenterFactory, "presenterFactory");
            Guard.AgainstNull(messageBus, "messageBus");

            this.workItemControllerFactory = workItemControllerFactory;
            this.workItemPresenterFactory = workItemPresenterFactory;
            this.presenterFactory = presenterFactory;
            this.messageBus = messageBus;
            this.shell = shell;

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
            return new WorkItemBuilder(text, this, workItemControllerFactory, workItemPresenterFactory);
        }

        public IWorkItemBuilderController Create(Guid workItemId, string text)
        {
            return new WorkItemBuilder(workItemId, text, this, workItemControllerFactory, workItemPresenterFactory);
        }

        public void TextChanged(WorkItem workItem)
        {
            messageBus.Publish(new WorkItemTextChangedMessage(workItem));
        }

        public INavigationItemContainer<IPresenter> BuildPresenter<T>() where T : IPresenter
        {
            return presenterFactory.Create<T>();
        }

        public void Waiting(WorkItem workItem)
        {
            messageBus.Publish(new WorkItemBusyMessage(workItem));
        }

        public void Ready(WorkItem workItem)
        {
            messageBus.Publish(new WorkItemReadyMessage(workItem));
        }

        public bool Contains(Guid id)
        {
            return store.ContainsKey(id);
        }

        public IPresenter CreatePresenter<T>() where T : IPresenter
        {
            return presenterFactory.Create<T>();
        }

        public void Invoke(Action action)
        {
            shell.Invoke(action);
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
