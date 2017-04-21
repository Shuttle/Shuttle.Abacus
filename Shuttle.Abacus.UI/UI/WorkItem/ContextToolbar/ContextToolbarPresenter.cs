using System.Collections.Generic;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class ContextToolbarPresenter :
        Presenter<IContextToolbarView>,
        IContextToolbarPresenter,
        IMessageHandler<WorkItemSitedMessage>
    {
        private readonly List<string> disabledMessageTypes = new List<string>();

        private readonly IUIService uiService;

        public ContextToolbarPresenter(IContextToolbarView view, IMessageBus messageBus, IUIService uiService)
            : base(view)
        {
            this.uiService = uiService;
            MessageBus = messageBus;

            messageBus.AddSubscriber(this);
        }

        public void Close()
        {
            if (!CanClose())
            {
                return;
            }

            MessageBus.Publish(new WorkItemCompletedMessage(WorkItem));
        }

        public void InvokeMessage(Message message)
        {
            MessageBus.Publish(message, WorkItem);
        }

        public void PresenterSelected(IPresenter presenter)
        {
            var defaultHolder = (IHaveDefaultMessage) presenter;
            var cancelHolder = (IHaveCancelMessage) presenter;

            if (defaultHolder.HasDefaultMessage)
            {
                WorkItem.AssignActiveDefaultMessage(defaultHolder.DefaultMessage);
            }
            else
            {
                WorkItem.ResetDefaultMessage();
            }

            if (cancelHolder.HasCancelMessage)
            {
                WorkItem.AssignActiveCancelMessage(cancelHolder.CancelMessage);
            }
            else
            {
                WorkItem.ResetCancelMessage();
            }

            ShowNavigationItems(presenter);
        }

        public void NoPresenterSelected()
        {
            WorkItem.ClearActiveCancelMessage();
        }

        public void AddPresenter(IPresenter presenter)
        {
            Guard.AgainstNull(presenter, "presenter");

            View.AddPresenter(presenter);
        }

        public void SelectPresenter(IPresenter presenter)
        {
            View.SelectPresenter(presenter);
        }

        public void DisableMessage<T>()
        {
            var key = typeof (T).FullName;

            if (disabledMessageTypes.Contains(key))
            {
                return;
            }

            disabledMessageTypes.Add(key);

            if (View.HasSelectedPresenter)
            {
                ShowNavigationItems(View.SelectedPresenter);
            }
        }

        public void EnableMessage<T>()
        {
            var key = typeof (T).FullName;

            if (!disabledMessageTypes.Contains(key))
            {
                return;
            }

            disabledMessageTypes.Remove(key);
        }

        public bool IsMessageEnabled(Message message)
        {
            Guard.AgainstNull(message, "message");

            return !disabledMessageTypes.Contains(message.GetType().FullName);
        }

        public void ResetChanges()
        {
            View.ResetChanges();
        }

        public void HandleMessage(ShowPresenterMessage message)
        {
            if (!WorkItem.Id.Equals(message.WorkItem.Id))
            {
                return;
            }

            View.SelectPresenter(message.Presenter);
        }

        public void HandleMessage(WorkItemSitedMessage message)
        {
            if (message.WorkItem.Equals(WorkItem))
            {
                View.ResetChanges();
            }
        }

        private bool CanClose()
        {
            return !View.HasChanges || uiService.Confirm("The current work item has changes.  Close anyway?");
        }

        private void ShowNavigationItems(IPresenter presenter)
        {
            View.ShowNavigationItems(presenter.MergedNavigationItems());
        }

        public override void OnViewCancelled()
        {
            base.OnViewCancelled();

            var cancelMessage = WorkItem.CancelMessage;

            if (cancelMessage == null)
            {
                Close();
            }
            else
            {
                if (CanClose())
                {
                    MessageBus.Publish(cancelMessage, WorkItem);
                }
            }
        }

        public override void OnViewAccepted()
        {
            base.OnViewAccepted();

            var defaultMessage = WorkItem.DefaultMessage;

            if (defaultMessage == null)
            {
                return;
            }

            MessageBus.Publish(defaultMessage, WorkItem);
        }
    }
}
