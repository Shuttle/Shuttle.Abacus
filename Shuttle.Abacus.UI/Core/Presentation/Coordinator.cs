using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Shell.Core.Clipboard;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.WorkItem;
using Shuttle.Abacus.Shell.Navigation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public abstract class Coordinator : ICoordinator, IDisposable
    {
        public IClipboard Clipboard { get; set; }
        public ISummaryViewManager SummaryViewManager { get; set; }
        public IWorkItemManager WorkItemManager { get; set; }
        public IMessageBus MessageBus { get; set; }
        public INavigationItemFactory NavigationItemFactory { get; set; }
        public ISession Session { get; set; }
        public IWorkspaceProvider WorkspaceProvider { get; set; }
        public IWorkItemControllerFactory WorkItemControllerFactory { get; set; }
        public IUIService UIService { get; set; }
        public IApplicationShell ApplicationShell { get; set; }

        public void Dispose()
        {
            MessageBus?.RemoveSubscriber(this);
        }

        protected void HostInWorkspace<T>(IWorkItem workItem) where T : IWorkspacePresenter
        {
            ApplicationShell.Invoke(() =>
                {
                    Guard.AgainstNull(workItem, "workItem");

                    workItem.Presenters.ForEach(presenter => presenter.Initialize());

                    WorkspaceProvider.Get<T>().Add(workItem);

                    MessageBus.Publish(new WorkItemSitedMessage(workItem));
                });
        }
    }
}
