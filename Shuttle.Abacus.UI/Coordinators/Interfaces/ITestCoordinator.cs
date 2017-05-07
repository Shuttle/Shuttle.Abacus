using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.Explorer;
using Shuttle.Abacus.UI.Messages.Resources;
using Shuttle.Abacus.UI.Messages.TestCase;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
{
    public interface ITestCoordinator :
        ICoordinator,
        IMessageHandler<ExplorerInitializeMessage>,
        IMessageHandler<ResourceMenuRequestMessage>,
        IMessageHandler<ManageMethodTestsMessage>,
        IMessageHandler<NewMethodTestMessage>,
        IMessageHandler<NewMethodTestFromExistingMessage>,
        IMessageHandler<EditMethodTestMessage>,
        IMessageHandler<MethodTestCreatedMessage>,
        IMessageHandler<MethodTestChangedMessage>,
        IMessageHandler<MethodTestRemovedMessage>,
        IMessageHandler<MethodTestRunMessage>,
        IMessageHandler<SummaryViewRequestedMessage>
    {

    }
}
