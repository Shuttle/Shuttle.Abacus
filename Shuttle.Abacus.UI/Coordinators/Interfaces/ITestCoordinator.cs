using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.Explorer;
using Shuttle.Abacus.Shell.Messages.Resources;
using Shuttle.Abacus.Shell.Messages.Test;

namespace Shuttle.Abacus.Shell.Coordinators.Interfaces
{
    public interface ITestCoordinator :
        ICoordinator,
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<PopulateResourceMessage>,
        IMessageHandler<ManageTestsMessage>,
        IMessageHandler<RegisterTestMessage>,
        IMessageHandler<EditTestMessage>,
        IMessageHandler<TestCreatedMessage>,
        IMessageHandler<TestChangedMessage>,
        IMessageHandler<TestRemovedMessage>,
        IMessageHandler<TestRunMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {
    }
}
