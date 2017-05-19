using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.ArgumentValue;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Resources;

namespace Shuttle.Abacus.Shell.Coordinators.Interfaces
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