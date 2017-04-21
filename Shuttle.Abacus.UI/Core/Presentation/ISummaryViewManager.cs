namespace Abacus.UI
{
    public interface ISummaryViewManager :
        IMessageHandler<ResourceSelectedMessage>
    {
        void ShowView();
        bool ViewOpen { get; }
        bool CanIgnore(SummaryViewRequestedMessage message, ResourceKey forKey);
    }
}
