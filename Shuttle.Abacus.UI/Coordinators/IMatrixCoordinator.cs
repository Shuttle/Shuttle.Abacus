using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.DecimalTable;
using Shuttle.Abacus.Shell.Messages.Explorer;
using Shuttle.Abacus.Shell.Messages.Resources;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public interface IMatrixCoordinator :
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<NewMatrixMessage>,
        IMessageHandler<EditMatrixMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
        
    }
}
