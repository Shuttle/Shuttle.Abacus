using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Limit;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface ILimitCoordinator :
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<NewLimitMessage>,
        IMessageHandler<EditLimitMessage>,
        IMessageHandler<DeleteLimitMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
        
    }
}
