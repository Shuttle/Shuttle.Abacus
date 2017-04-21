namespace Abacus.UI
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
