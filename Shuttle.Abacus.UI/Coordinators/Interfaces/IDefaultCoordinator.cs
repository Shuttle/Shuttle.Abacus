using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.UI.Shell;

namespace Shuttle.Abacus.UI.Coordinators.Interfaces
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
