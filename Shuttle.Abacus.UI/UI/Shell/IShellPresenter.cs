using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.UI.Shell
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
