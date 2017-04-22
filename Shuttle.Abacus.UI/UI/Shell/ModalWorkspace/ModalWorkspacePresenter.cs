using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Messages.WorkItem;

namespace Shuttle.Abacus.UI.UI.Shell.ModalWorkspace
{
    public class ModalWorkspacePresenter :
        Presenter<IModalWorkspaceView>,
        IModalWorkspacePresenter
    {
        public ModalWorkspacePresenter(IModalWorkspaceView view) : base(view)
        {
        }

        public IWorkspacePresenter Add(IWorkItem workItem)
        {
            WorkItem = workItem;

            View.Add(workItem);

            return this;
        }

        public void HandleMessage(WorkItemCompletedMessage message)
        {
            if (message.WorkItem == WorkItem)
            {
                View.Close();
            }
        }

        public void HandleMessage(WorkItemTextChangedMessage message)
        {
            if (message.WorkItem == WorkItem)
            {
                View.SetText(message.WorkItem.Text);
            }
        }

        public void HandleMessage(WorkItemBusyMessage message)
        {
            //todo
        }

        public void HandleMessage(WorkItemReadyMessage message)
        {
            //todo
        }

        public void HandleMessage(ShowPresenterMessage message)
        {
            // can't do anything
        }

        public override void OnViewReady()
        {
            base.OnViewReady();

            MessageBus.AddSubscriber(this);
        }

        public override void OnViewCancelled()
        {
            base.OnViewCancelled();

            if (WorkItem == null)
            {
                return;
            }

            var cancelMessage = WorkItem.CancelMessage;

            if (cancelMessage == null)
            {
                MessageBus.Publish(new WorkItemCompletedMessage(WorkItem));
            }
            else
            {
                MessageBus.Publish(cancelMessage, WorkItem);
            }
        }

        public override void OnViewAccepted()
        {
            base.OnViewAccepted();

            if (WorkItem == null)
            {
                return;
            }

            var defaultMessage = WorkItem.DefaultMessage;

            if (defaultMessage == null)
            {
                return;
            }

            MessageBus.Publish(defaultMessage, WorkItem);
        }
    }
}
