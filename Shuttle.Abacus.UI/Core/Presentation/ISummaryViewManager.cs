using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface ISummaryViewManager :
        IMessageHandler<ResourceSelectedMessage>
    {
        void ShowView();
        bool ViewOpen { get; }
        bool CanIgnore(SummaryViewRequestedMessage message, ResourceKey forKey);
    }
}
