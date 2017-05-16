using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.Test;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface ITestCoordinator :
        ICoordinator,
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
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
