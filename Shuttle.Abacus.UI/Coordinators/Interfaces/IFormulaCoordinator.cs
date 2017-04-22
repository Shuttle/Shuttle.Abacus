using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Formula;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IFormulaCoordinator :
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<NewFormulaMessage>,
        IMessageHandler<NewFormulaFromExistingMessage>,
        IMessageHandler<EditFormulaMessage>,
        IMessageHandler<DeleteFormulaMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<ChangeFormulaOrderMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
    }
}
