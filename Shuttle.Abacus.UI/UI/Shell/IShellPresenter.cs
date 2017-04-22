using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.UI.Shell
{
    public interface IShellPresenter :
        IPresenter,
        IMessageHandler<StartShellMessage>,
        IMessageHandler<ApplicationExitMessage>,
        IMessageHandler<WorkStartedMessage>,
        IMessageHandler<WorkCompletedMessage>,
        IMessageHandler<StatusMessage>,
        IMessageHandler<ReadyStatusMessage>
    {
        void PublishMessage(Message message);
        void Configure();
    }
}
