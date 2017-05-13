using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.ArgumentValue;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Resources;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface IArgumentValueCoordinator :
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<RegisterArgumentValueMessage>,
        IMessageHandler<RemoveArgumentValueMessage>,
        IMessageHandler<ResourceRefreshItemTextMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
    }
}