using System;
using Abacus.Infrastructure;

namespace Abacus.UI
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
        public IShell Shell { get; set; }

        public void Dispose()
        {
            if (MessageBus == null)
            {
                return;
            }

            MessageBus.RemoveSubscriber(this);
        }

        protected void HostInWorkspace<T>(IWorkItem workItem) where T : IWorkspacePresenter
        {
            Shell.Invoke(() =>
                {
                    Guard.AgainstNull(workItem, "workItem");

                    workItem.Presenters.ForEach(presenter => presenter.Initialize());

                    WorkspaceProvider.Get<T>().Add(workItem);

                    MessageBus.Publish(new WorkItemSitedMessage(workItem));
                });
        }
    }
}
