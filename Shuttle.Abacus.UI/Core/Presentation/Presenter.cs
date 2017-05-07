using System;
using System.Collections.Generic;
using System.Drawing;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.UI.Core.EventArgs;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Validation;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Navigation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public abstract class Presenter<TView> :
        IPresenter,
        IHaveDefaultMessage,
        IHaveCancelMessage,
        INavigationItemConfiguration<IPresenter>
        where TView : class, IView
    {
        private readonly IList<INavigationItem> _navigationItems;
        private INavigationItem _buildNavigationItem;

        private Image _image;
        private string _text;
        private IWorkItem _workItem;

        protected Presenter(TView view)
        {
            Guard.AgainstNull(view, "view");

            IView = view;

            view.AttachPresenter(this);

            Text = GetType().Name;
            Image = Localisation.Resources.Image_Presenter;

            TrackChanges = true;

            _navigationItems = new List<INavigationItem>();
        }

        public TView View
        {
            get
            {
                var result = IView as TView;

                if (result == null)
                {
                    throw new InvalidCastException(string.Format(Localisation.Resources.NullSafeCasting,
                        IView.GetType().FullName,
                        typeof(TView).FullName));
                }

                return result;
            }
        }

        public IViewValidatorFactory ViewValidatorFactory
        {
            set { View.AttachViewValidator(value.Create(View.ViewValidationManager)); }
        }

        public Message CancelMessage { get; private set; }

        public bool HasCancelMessage
        {
            get { return CancelMessage != null; }
        }

        public Message DefaultMessage { get; private set; }

        public bool HasDefaultMessage
        {
            get { return DefaultMessage != null; }
        }

        public IPresenter AsDefault()
        {
            DefaultMessage = _buildNavigationItem.Message;

            return this;
        }

        public IPresenter AsCancel()
        {
            CancelMessage = _buildNavigationItem.Message;

            return this;
        }

        public virtual void Dispose()
        {
            MessageBus.RemoveSubscriber(this);
        }

        public event PresenterTextChanged TextChanged = delegate { };
        public event PresenterImageChanged ImageChanged = delegate { };

        public void Show()
        {
            MessageBus.Publish(new ShowPresenterMessage(WorkItem, this));
        }

        public IEnumerable<INavigationItem> MergedNavigationItems()
        {
            var result = new List<INavigationItem>();

            WorkItem.NavigationItems.ForEach(result.Add);

            NavigationItems.ForEach(result.Add);

            return result;
        }

        public IEnumerable<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
        }

        public IMessageBus MessageBus { get; set; }

        public INavigationItemFactory NavigationItemFactory { get; set; }

        public string Text
        {
            get { return _text; }
            set
            {
                var from = _text;

                _text = value;

                TextChanged(this, new PresenterTextChangedArgs(from, value));
            }
        }

        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;

                ImageChanged(this, new PresenterImageChangedArgs(value));
            }
        }

        public IWorkItem WorkItem
        {
            get { return _workItem; }
            set
            {
                Guard.AgainstReassignment(_workItem, "WorkItem");

                _workItem = value;
            }
        }

        public IView IView { get; protected set; }

        public bool IsValid
        {
            get
            {
                IView.ValidateView();

                return IView.IsValid;
            }
        }

        public bool TrackChanges { get; private set; }

        public void ViewReady()
        {
            OnViewReady();

            View.ValidateView();
        }

        public void ViewAccepted()
        {
            OnViewAccepted();
        }

        public void ViewCancelled()
        {
            OnViewCancelled();
        }

        public void Initialize()
        {
            OnInitialize();
        }

        public void PublishReadyStatus()
        {
            MessageBus.Publish(new ReadyStatusMessage());
        }

        public void PublishStatus(string message)
        {
            MessageBus.Publish(new StatusMessage(message));
        }

        public IPresenter AddNavigationItem(INavigationItem navigationItem)
        {
            Guard.AgainstNull(navigationItem, "navigationItem");

            _navigationItems.Add(navigationItem);

            _buildNavigationItem = navigationItem;

            return this;
        }

        public virtual void OnViewReady()
        {
        }

        public virtual void OnViewAccepted()
        {
        }

        public virtual void OnViewCancelled()
        {
        }

        public virtual void OnInitialize()
        {
        }

        public IResult PublishResult(IResult result)
        {
            MessageBus.Publish(new ResultNotificationMessage(result));

            return result;
        }

        public IResult<TValue> PublishResult<TValue>(IResult<TValue> result)
        {
            MessageBus.Publish(new ResultNotificationMessage(result));

            return result;
        }

        protected void DontTrackChanges()
        {
            TrackChanges = false;
        }

        protected void ResetChangeTracking()
        {
            WorkItem.WorkItemPresenter.ResetChanges();
        }
    }

    public abstract class Presenter<TView, TModel> : Presenter<TView>
        where TView : class, IView where TModel : class
    {
        protected Presenter(TView view) : base(view)
        {
        }

        public TModel Model { get; private set; }

        public void AssignModel<T>(T dto)
        {
            Model = dto as TModel;
        }

    }


    public delegate void PresenterImageChanged(object sender, PresenterImageChangedArgs args);

    public delegate void PresenterTextChanged(object sender, PresenterTextChangedArgs args);
}