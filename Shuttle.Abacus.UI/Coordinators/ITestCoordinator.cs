using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Explorer;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Messages.Test;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public interface ITestCoordinator :
        ICoordinator,
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<RegisterTestMessage>,
        IMessageHandler<RunTestMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
    }
}
