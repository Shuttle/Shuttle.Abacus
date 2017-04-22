using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.Section;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IMethodCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<NewMethodMessage>,
        IMessageHandler<NewMethodFromExistingMessage>,
        IMessageHandler<EditMethodMessage>,
        IMessageHandler<DeleteMethodMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>,
        IMessageHandler<ExplorerInitializeMessage>
    {
        
    }
}
