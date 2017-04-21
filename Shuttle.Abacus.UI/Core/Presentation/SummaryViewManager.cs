using System;

namespace Abacus.UI
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

        public bool ViewOpen
        {
            get { return workItemManager.Contains(DefaultCoordinator.SummaryViewWorkItemId); }
        }

        public bool CanIgnore(SummaryViewRequestedMessage message, ResourceKey forKey)
        {
            return !ViewOpen || message.Item.ResourceKey != forKey;
        }

        public void HandleMessage(ResourceSelectedMessage message)
        {
            var populateSummaryViewMessage = new SummaryViewRequestedMessage(message.Item, message.RelatedItems);

            messageBus.Publish(populateSummaryViewMessage);

            var workItem = workItemManager.Get(DefaultCoordinator.SummaryViewWorkItemId);

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
