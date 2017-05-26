using System;
using Shuttle.Abacus.Shell.Coordinators;
using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.UI.Summary;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public class SummaryViewManager : ISummaryViewManager, IDisposable
    {
        private readonly IMessageBus messageBus;
        private readonly IWorkItemManager workItemManager;

        public SummaryViewManager(IMessageBus messageBus, IWorkItemManager workItemManager)
        {
            this.messageBus = messageBus;
            this.workItemManager = workItemManager;

            messageBus.AddSubscriber(this);
        }

        public void ShowView()
        {
            messageBus.Publish(new ShowSummaryViewMessage());
        }

        public bool ViewOpen => workItemManager.Contains(DefaultCoordinator.SummaryViewWorkItemId);

        public bool CanIgnore(SummaryViewRequestedMessage message, ResourceKey forKey)
        {
            return !ViewOpen || !message.Item.ResourceKey.Equals(forKey);
        }

        public void HandleMessage(ResourceSelectedMessage message)
        {
            var populateSummaryViewMessage = new SummaryViewRequestedMessage(message.Item, message.RelatedItems);

            messageBus.Publish(populateSummaryViewMessage);

            var workItem = workItemManager.Find(DefaultCoordinator.SummaryViewWorkItemId);

            if (workItem == null)
            {
                return;
            }

            workItem.GetPresenter<ISummaryPresenter>().Populate(populateSummaryViewMessage.NamedQueryResults);
        }

        public void Dispose()
        {
            if (messageBus != null)
            {
                messageBus.RemoveSubscriber(this);
            }
        }
    }
}
