using System;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.Clipboard;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.WorkItem;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public abstract class Coordinator : ICoordinator, IDisposable, IMessageHandler<ReplyMessage>
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
            MessageBus?.RemoveSubscriber(this);
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

        public void HandleMessage(ReplyMessage message)
        {
            var header =
                message.Result.Headers.Find(item => item.Key.Equals("__callback", StringComparison.OrdinalIgnoreCase));

            if (header == null)
            {
                return;
            }

            //var action = CallbackRepository.Find(header.Value);

            //if (action == null)
            //{
            //    return;
            //}

            //action();

            //CallbackRepository.Remove(header.Value);
        }

    }
}
