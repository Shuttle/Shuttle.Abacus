using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IFormulaCoordinator :
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<RegisterFormulaMessage>,
        IMessageHandler<RenameFormulaMessage>,
        IMessageHandler<RemoveFormulaMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
    }
}
