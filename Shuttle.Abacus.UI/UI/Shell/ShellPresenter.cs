using System.Windows.Forms;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ShellPresenter :
        Presenter<IShellView>,
        IShellPresenter
    {
        private readonly ISession session;
        private readonly IClipboard clipboard;
        private ExplorerPartialPresenter explorerPartialPresenter;

        private readonly IExplorerRootItemOrderProvider explorerRootItemOrderProvider;
        private readonly INavigationMap navigationMap;

        public ShellPresenter(IShellView view, IExplorerRootItemOrderProvider explorerRootItemOrderProvider,
                              INavigationMap navigationMap, ISession session, IClipboard clipboard)
            : base(view)
        {
            this.explorerRootItemOrderProvider = explorerRootItemOrderProvider;
            this.navigationMap = navigationMap;
            this.session = session;
            this.clipboard = clipboard;
        }

        public IWorkspaceProvider WorkspaceProvider { get; set; }

        public void PublishMessage(Message message)
        {
            MessageBus.Publish(message);
        }

        public void Configure()
        {
            View.PopulateMenu(navigationMap.SecuredItems(session.Permissions));

            explorerPartialPresenter.Initialize();
        }

        public void HandleMessage(StartShellMessage message)
        {
            MessageBus.AddSubscriber(this);

            ShowContainers();
        }

        public void HandleMessage(WorkStartedMessage message)
        {
            View.Busy();
        }

        public void HandleMessage(WorkCompletedMessage message)
        {
            View.Ready();
        }

        public void HandleMessage(StatusMessage message)
        {
            View.ShowStatus(message.Message);
        }

        public void HandleMessage(ReadyStatusMessage message)
        {
            View.ShowStatus("Ready");
        }

        public override void OnViewReady()
        {
            base.OnViewReady();

            explorerPartialPresenter = new ExplorerPartialPresenter(View.ExplorerView, MessageBus,
                                                                    explorerRootItemOrderProvider, session, clipboard);
        }

        private void ShowContainers()
        {
            WorkspaceProvider.RegisterSingleton<ITabbedWorkspacePresenter>();

            View.ShowContainer(WorkspaceProvider.Get<ITabbedWorkspacePresenter>().IView as UserControl);
        }

        public void HandleMessage(ApplicationExitMessage message)
        {
            Application.Exit();
        }
    }
}
