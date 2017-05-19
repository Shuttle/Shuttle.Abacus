using Shuttle.Abacus.Shell.Core.Messaging;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.UI.Shell;

namespace Shuttle.Abacus.Shell.Coordinators
{
    public interface IDefaultCoordinator :
        IMessageHandler<StartShellMessage>,
        IMessageHandler<ResultNotificationMessage>,
        IMessageHandler<ConfigureShellMessage>,
        IMessageHandler<ActivateShellMessage>,
        IMessageHandler<ShowSummaryViewMessage>
    {
        IShellPresenter ShellPresenter { get; set; }
    }
}
