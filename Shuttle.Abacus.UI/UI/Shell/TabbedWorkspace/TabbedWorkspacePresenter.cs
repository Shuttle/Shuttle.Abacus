using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.WorkItem;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Messages.WorkItem;

namespace Shuttle.Abacus.Shell.UI.Shell.TabbedWorkspace
{
    public class TabbedWorkspacePresenter :
        Presenter<ITabbedWorkspaceView>,
        ITabbedWorkspacePresenter
    {
        public TabbedWorkspacePresenter(ITabbedWorkspaceView view)
            : base(view)
        {
        }

        public IWorkspacePresenter Add(IWorkItem workItem)
        {
            View.Add(workItem);

            return this;
        }

        public void HandleMessage(WorkItemCompletedMessage message)
        {
            View.RemoveTab(message.WorkItem);
        }

        public void HandleMessage(WorkItemTextChangedMessage message)
        {
            View.SetTabText(message.WorkItem);
        }

        public void HandleMessage(WorkItemBusyMessage message)
        {
            View.SetTabWaiting(message.WorkItem);
        }

        public void HandleMessage(WorkItemReadyMessage message)
        {
            View.SetTabReady(message.WorkItem);
        }

        public void HandleMessage(ShowPresenterMessage message)
        {
            View.Show(message.WorkItem);
        }

        public override void OnViewReady()
        {
            base.OnViewReady();

            MessageBus.AddSubscriber(this);
        }

        public override void OnViewCancelled()
        {
            base.OnViewCancelled();

            var currentWorkItem = View.SelectedWorkItem;

            if (currentWorkItem == null)
            {
                return;
            }

            currentWorkItem.WorkItemPresenter.ViewCancelled();
        }

        public override void OnViewAccepted()
        {
            base.OnViewAccepted();

            var currentWorkItem = View.SelectedWorkItem;

            if (currentWorkItem == null || currentWorkItem.IsWaiting)
            {
                return;
            }

            currentWorkItem.WorkItemPresenter.ViewAccepted();
        }
    }
}
