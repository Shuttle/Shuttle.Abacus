using System;
using System.Collections.Generic;
using System.Data;
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
    public abstract class Presenter<TView, TModel> :
        IPresenter,
        IHaveDefaultMessage,
        IHaveCancelMessage,
        INavigationItemConfiguration<IPresenter>
        where TView : class, IView
        where TModel : class
    {
        private readonly IList<INavigationItem> navigationItems;

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

        private Image image;
        private string text;
        private IWorkItem workItem;
        private INavigationItem buildNavigationItem;

        protected Presenter(TView view)
        {
            IView = view;

            view.AttachPresenter(this);

            Text = GetType().Name;
            Image = Localisation.Resources.Image_Presenter;

            TrackChanges = true;

            navigationItems = new List<INavigationItem>();
        }

        public IEnumerable<INavigationItem> NavigationItems
        {
            get { return navigationItems; }
        }

        public TView View
        {
            get
            {
                var result = IView as TView;

                if (result == null)
                {
                    throw new InvalidCastException(string.Format(Localisation.Resources.NullSafeCasting, IView.GetType().FullName,
                                                                 typeof (TView).FullName));
                }

                return result;
            }
        }

        public Message DefaultMessage { get; private set; }

        public bool HasDefaultMessage
        {
            get { return DefaultMessage != null; }
        }

        public Message CancelMessage { get; private set; }
        
        public bool HasCancelMessage
        {
            get { return CancelMessage != null; }
        }

        public TModel Model { get; private set; }
        public IMessageBus MessageBus { get; set; }

        public IViewValidatorFactory ViewValidatorFactory
        {
            set
            {
                View.AttachViewValidator(value.Create(View.ViewValidationManager));
            }
        }

        public INavigationItemFactory NavigationItemFactory { get; set; }

        public string Text
        {
            get { return text; }
            set
            {
                var from = text;

                text = value;

                TextChanged(this, new PresenterTextChangedArgs(from, value));
            }
        }

        public Image Image
        {
            get { return image; }
            set
            {
                image = value;

                ImageChanged(this, new PresenterImageChangedArgs(value));
            }
        }

        public IWorkItem WorkItem
        {
            get { return workItem; }
            set
            {
                Guard.AgainstReassignment(workItem, "WorkItem");

                workItem = value;
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

        public void AssignModel<T>(T dto)
        {
            Model = dto as TModel;
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

        public IPresenter AddNavigationItem(INavigationItem navigationItem)
        {
            Guard.AgainstNull(navigationItem, "navigationItem");

            navigationItems.Add(navigationItem);

            buildNavigationItem = navigationItem;

            return this;
        }

        public IPresenter AsDefault()
        {
            DefaultMessage = buildNavigationItem.Message;

            return this;
        }

        public IPresenter AsCancel()
        {
            CancelMessage = buildNavigationItem.Message;

            return this;
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

    public delegate void PresenterImageChanged(object sender, PresenterImageChangedArgs args);

    public delegate void PresenterTextChanged(object sender, PresenterTextChangedArgs args);

    public abstract class Presenter<TView> : Presenter<TView, IEnumerable<DataRow>> where TView : class, IView
    {
        protected Presenter(TView view)
            : base(view)
        {
        }
    }
}
