using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.State;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public class WorkItem : IWorkItemBuilderPresenter
    {
        private readonly IList<INavigationItem> navigationItems;
        private readonly IList<IPresenter> presenters;
        private readonly State<IWorkItem> state;
        private readonly IWorkItemManager workItemManager;

        private string text;
        private string textBeforeWaiting;

        private INavigationItem buildNavigationItem;
        private IPresenter buildPresenter;

        public WorkItem(string text, IWorkItemManager workItemManager, IWorkItemController workItemController,
                        IWorkItemPresenter workItemPresenter)
            : this(Guid.NewGuid(), text, workItemManager, workItemController, workItemPresenter)
        {
        }

        public WorkItem(Guid workItemId, string text, IWorkItemManager workItemManager,
                        IWorkItemController workItemController,
                        IWorkItemPresenter workItemPresenter)
        {
            this.workItemManager = workItemManager;

            Id = workItemId;

            presenters = new List<IPresenter>();
            navigationItems = new List<INavigationItem>();

            this.text = text;

            DefaultMessage = null;
            CancelMessage = null;

            WorkItemPresenter = workItemPresenter;
            WorkItemPresenter.WorkItem = this;

            WorkItemController = workItemController;
            WorkItemController.AssignWorkItem(this);

            state = new State<IWorkItem>(this);

            Image = null;
        }

        public IWorkItemController WorkItemController { get; private set; }
        public IWorkItemPresenter WorkItemPresenter { get; private set; }

        public Message ActiveDefaultMessage { get; private set; }
        public Message DefaultMessage { get; private set; }

        public bool HasDefaultMessage
        {
            get { return ActiveDefaultMessage != null; }
        }

        public Message ActiveCancelMessage { get; private set; }

        public bool IsWaiting { get; private set; }

        public string ToolTipText
        {
            get
            {
                return Initiator != null
                           ? Initiator.ToolTipText
                           : string.Empty;
            }
        }

        public Message CancelMessage { get; private set; }

        public bool HasCancelMessage
        {
            get { return ActiveCancelMessage != null; }
        }

        public State<IWorkItem> State
        {
            get { return state; }
        }

        public Image Image { get; set; }

        public IEnumerable<INavigationItem> NavigationItems
        {
            get { return navigationItems; }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value + (IsWaiting
                                    ? " (waiting)"
                                    : string.Empty);

                workItemManager.TextChanged(this);
            }
        }

        public Guid Id { get; private set; }

        public IEnumerable<IPresenter> Presenters
        {
            get { return new ReadOnlyCollection<IPresenter>(presenters); }
        }

        public IWorkItem AssignWorkItemImage(Image image)
        {
            Image = image;

            return this;
        }

        public IWorkItemBuilderPresenter AddPresenter<T>() where T : IPresenter
        {
            return AddPresenter(workItemManager.CreatePresenter<T>());
        }

        public IWorkItemBuilderPresenter AddPresenter(IPresenter presenter)
        {
            workItemManager.Invoke(() =>
                {
                    presenter.WorkItem = this;

                    presenters.Add(presenter);

                    WorkItemPresenter.AddPresenter(presenter);

                    buildPresenter = presenter;
                });

            return this;
        }

        public IWorkItem AddNavigationItem(INavigationItem navigationItem)
        {
            Guard.AgainstNull(navigationItem, "navigationItem");

            navigationItems.Add(navigationItem);

            buildNavigationItem = navigationItem;

            return this;
        }

        public T GetPresenter<T>() where T : IPresenter
        {
            var type = typeof (T);

            foreach (var presenter in presenters)
            {
                if (type.IsAssignableFrom(presenter.GetType()))
                {
                    return (T) presenter;
                }
            }

            return default(T);
        }

        public T GetView<T>() where T : IView
        {
            var type = typeof (T);

            foreach (var presenter in presenters)
            {
                if (type.IsAssignableFrom(presenter.IView.GetType()))
                {
                    return (T) presenter.IView;
                }
            }

            return default(T);
        }

        public bool PresentationValid()
        {
            foreach (var presenter in Presenters)
            {
                if (presenter.IsValid)
                {
                    continue;
                }

                WorkItemPresenter.SelectPresenter(presenter);

                return false;
            }

            return true;
        }

        public IWorkItem AssignInitiator(IWorkItemInitiator initiator)
        {
            Initiator = initiator;

            return this;
        }

        public IWorkItemInitiator Initiator { get; private set; }

        public void AssignActiveDefaultMessage(Message message)
        {
            DefaultMessage = message;
        }

        public void ResetDefaultMessage()
        {
            ActiveDefaultMessage = DefaultMessage;
        }

        public void AssignActiveCancelMessage(Message message)
        {
            CancelMessage = message;
        }

        public void ResetCancelMessage()
        {
            ActiveCancelMessage = CancelMessage;
        }

        public void ClearActiveDefaultMessage()
        {
            ActiveDefaultMessage = null;
        }

        public void ClearActiveCancelMessage()
        {
            ActiveCancelMessage = null;
        }

        public void Waiting()
        {
            textBeforeWaiting = text;

            IsWaiting = true;

            text += " (waiting)";

            workItemManager.TextChanged(this);
            workItemManager.Waiting(this);
        }

        public void Ready()
        {
            IsWaiting = false;

            Text = textBeforeWaiting;

            workItemManager.Ready(this);
        }

        public IEnumerable<object> Subscribers
        {
            get
            {
                return new List<object>(presenters)
                       {
                           WorkItemController
                       };
            }
        }

        public IWorkItem AsDefault()
        {
            ActiveDefaultMessage = buildNavigationItem.Message;

            DefaultMessage = buildNavigationItem.Message;

            return this;
        }

        public IWorkItem AsCancel()
        {
            CancelMessage = buildNavigationItem.Message;

            ActiveCancelMessage = buildNavigationItem.Message;

            return this;
        }

        public IWorkItemBuilderPresenter WithModel<T>(T presenterModel) where T : class
        {
            buildPresenter.AssignModel(presenterModel);

            return this;
        }

        public IWorkItemBuilderPresenter AssignText(string presenterText)
        {
            buildPresenter.Text = presenterText;

            return this;
        }

        public IWorkItemBuilderPresenter AssignImage(Image presenterImage)
        {
            buildPresenter.Image = presenterImage;

            return this;
        }

        public bool Equals(WorkItem other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || other.Id.Equals(Id));
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || obj.GetType() == typeof (WorkItem) && Equals((WorkItem) obj));
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(WorkItem left, WorkItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WorkItem left, WorkItem right)
        {
            return !Equals(left, right);
        }
    }
}
