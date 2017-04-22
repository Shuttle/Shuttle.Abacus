using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Calculation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface ICalculationCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<ChangeCalculationOrderMessage>,
        IMessageHandler<NewCalculationMessage>,
        IMessageHandler<EditCalculationMessage>,
        IMessageHandler<DeleteCalculationMessage>,
        IMessageHandler<GrabCalculationsMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
        
    }
}
