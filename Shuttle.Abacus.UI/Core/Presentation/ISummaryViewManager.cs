using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Resources;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public interface ISummaryViewManager :
        IMessageHandler<ResourceSelectedMessage>
    {
        void ShowView();
        bool ViewOpen { get; }
        bool CanIgnore(SummaryViewRequestedMessage message, ResourceKey forKey);
    }
}
