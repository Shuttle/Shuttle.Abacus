using System;
using System.Windows.Forms;

namespace Abacus.UI
{
    public class DefaultCoordinator : Coordinator, IDefaultCoordinator
    {
        public static readonly Guid SummaryViewWorkItemId = new Guid("03F030CA-7618-4845-B671-4724042129D2");

        private Form shellForm;

        public IShellPresenter ShellPresenter { get; set; }

        public void HandleMessage(StartShellMessage message)
        {
            ShellPresenter.HandleMessage(message);

            shellForm = ((Form) ShellPresenter.IView);

            shellForm.Show();
        }

        public void HandleMessage(ResultNotificationMessage message)
        {
            if (!message.Result.HasMessages)
            {
                return;
            }

            var view = new NotificationView();

            view.HandleMessage(message);

            if (shellForm.InvokeRequired)
            {
                shellForm.Invoke(new MethodInvoker(() => view.ShowDialog()));
            }
            else
            {
                view.ShowDialog();
            }
        }

        public void HandleMessage(ConfigureShellMessage message)
        {
            ShellPresenter.Configure();
        }

        public void HandleMessage(ActivateShellMessage message)
        {
            shellForm.BringToFront();

            SummaryViewManager.ShowView();
        }

        public void HandleMessage(ShowSummaryViewMessage message)
        {
            if (WorkItemManager.Contains(SummaryViewWorkItemId))
            {
                WorkItemManager.Get(SummaryViewWorkItemId).GetPresenter<ISummaryPresenter>().Show();

                return;
            }

            var item = WorkItemManager.
                Create(SummaryViewWorkItemId, "Summary View")
                .ControlledBy<ISummaryController>()
                .ShowIn<IContextToolbarPresenter>()
                .AddPresenter<ISummaryPresenter>();

            HostInWorkspace<ITabbedWorkspacePresenter>(item);

            MessageBus.Publish(new SummaryViewActivatedMessage());
        }
    }
}
