using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Argument;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.FactorAnswer;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IArgumentCoordinator :
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<RegisterArgumentMessage>, 
        IMessageHandler<RenameArgumentMessage>,
        IMessageHandler<RemoveArgumentMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {

    }
}
