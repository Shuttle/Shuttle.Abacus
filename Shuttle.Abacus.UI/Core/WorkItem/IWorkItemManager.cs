using System;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.Navigation;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface IWorkItemManager : IMessageHandler<WorkItemCompletedMessage>
    {
        void Add(IWorkItem workItem);
        IWorkItem Get(Guid id);

        IWorkItemBuilderController Create(string text);
        IWorkItemBuilderController Create(Guid workItemId, string text);

        void TextChanged(WorkItem workItem);

        INavigationItemContainer<IPresenter> BuildPresenter<T>() where T : IPresenter;
        void Waiting(WorkItem workItem);
        void Ready(WorkItem workItem);
        bool Contains(Guid id);
        IPresenter CreatePresenter<T>() where T : IPresenter;
        void Invoke(Action action);
    }
}
