using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.DecimalTable;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IDecimalTableCoordinator :
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<NewDecimalTableMessage>,
        IMessageHandler<EditDecimalTableMessage>,
        IMessageHandler<NewDecimalTableFromExistingMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
        
    }
}
