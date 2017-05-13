using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.State;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public class WorkItem : IWorkItemBuilderPresenter
    {
        private readonly IList<INavigationItem> _navigationItems;
        private readonly IList<IPresenter> _presenters;
        private readonly IWorkItemManager _workItemManager;

        private INavigationItem _navigationItem;
        private IPresenter _presenter;

        private string _text;
        private string _textBeforeWaiting;

        public WorkItem(string text, IWorkItemManager workItemManager, IWorkItemController workItemController,
            IWorkItemPresenter workItemPresenter)
            : this(Guid.NewGuid(), text, workItemManager, workItemController, workItemPresenter)
        {
        }

        public WorkItem(Guid workItemId, string text, IWorkItemManager workItemManager,
            IWorkItemController workItemController,
            IWorkItemPresenter workItemPresenter)
        {
            _workItemManager = workItemManager;

            Id = workItemId;

            _presenters = new List<IPresenter>();
            _navigationItems = new List<INavigationItem>();

            _text = text;

            DefaultMessage = null;
            CancelMessage = null;

            WorkItemPresenter = workItemPresenter;
            WorkItemPresenter.WorkItem = this;

            WorkItemController = workItemController;
            WorkItemController.AssignWorkItem(this);

            State = new State<IWorkItem>(this);

            Image = null;
        }

        public IWorkItemController WorkItemController { get; }
        public IWorkItemPresenter WorkItemPresenter { get; }

        public Message ActiveDefaultMessage { get; private set; }
        public Message DefaultMessage { get; private set; }

        public bool HasDefaultMessage => ActiveDefaultMessage != null;

        public Message ActiveCancelMessage { get; private set; }

        public bool IsWaiting { get; private set; }

        public string ToolTipText => Initiator != null
            ? Initiator.ToolTipText
            : string.Empty;

        public Message CancelMessage { get; private set; }

        public bool HasCancelMessage => ActiveCancelMessage != null;

        public State<IWorkItem> State { get; }

        public Image Image { get; set; }

        public IEnumerable<INavigationItem> NavigationItems => _navigationItems;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value + (IsWaiting
                            ? " (waiting)"
                            : string.Empty);

                _workItemManager.TextChanged(this);
            }
        }

        public Guid Id { get; }

        public IEnumerable<IPresenter> Presenters => new ReadOnlyCollection<IPresenter>(_presenters);

        public IWorkItem AssignWorkItemImage(Image image)
        {
            Image = image;

            return this;
        }

        public IWorkItemBuilderPresenter AddPresenter<T>() where T : IPresenter
        {
            return AddPresenter(_workItemManager.CreatePresenter<T>());
        }

        public IWorkItemBuilderPresenter AddPresenter(IPresenter presenter)
        {
            _workItemManager.Invoke(() =>
            {
                presenter.WorkItem = this;

                _presenters.Add(presenter);

                WorkItemPresenter.AddPresenter(presenter);

                _presenter = presenter;
            });

            return this;
        }

        public IWorkItem AddNavigationItem(INavigationItem navigationItem)
        {
            Guard.AgainstNull(navigationItem, "navigationItem");

            _navigationItems.Add(navigationItem);

            _navigationItem = navigationItem;

            return this;
        }

        public T GetPresenter<T>() where T : IPresenter
        {
            var type = typeof(T);

            foreach (var presenter in _presenters)
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
            var type = typeof(T);

            foreach (var presenter in _presenters)
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
            _textBeforeWaiting = _text;

            IsWaiting = true;

            _text += " (waiting)";

            _workItemManager.TextChanged(this);
            _workItemManager.Waiting(this);
        }

        public void Ready()
        {
            IsWaiting = false;

            Text = _textBeforeWaiting;

            _workItemManager.Ready(this);
        }

        public IEnumerable<object> Subscribers => new List<object>(_presenters)
        {
            WorkItemController
        };

        public IWorkItem AsDefault()
        {
            ActiveDefaultMessage = _navigationItem.Message;

            DefaultMessage = _navigationItem.Message;

            return this;
        }

        public IWorkItem AsCancel()
        {
            CancelMessage = _navigationItem.Message;

            ActiveCancelMessage = _navigationItem.Message;

            return this;
        }

        public IWorkItemBuilderPresenter WithModel<T>(T presenterModel) where T : class
        {
            var presenter = _presenter as IPresenter<T>;

            if (presenter == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Cannot assign model of type '{0}' to presenter of type '{1}' as the presenter does not accept a model of that type.",
                        typeof(T).FullName, _presenter.GetType().FullName));
            }

            presenter.AssignModel(presenterModel);

            return this;
        }

        public IWorkItemBuilderPresenter AssignText(string presenterText)
        {
            _presenter.Text = presenterText;

            return this;
        }

        public IWorkItemBuilderPresenter AssignImage(Image presenterImage)
        {
            _presenter.Image = presenterImage;

            return this;
        }

        public bool Equals(WorkItem other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || other.Id.Equals(Id));
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || obj.GetType() == typeof(WorkItem) && Equals((WorkItem) obj));
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