using System;
using System.Collections.Generic;
using System.Drawing;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.State;
using Shuttle.Abacus.UI.Navigation;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface IWorkItem : 
        IHaveDefaultMessage,
        IHaveCancelMessage,
        INavigationItemContainer<IWorkItem>, 
        INavigationItemConfiguration<IWorkItem>,
        ISubscriberProvider
    {
        Guid Id { get; }
        string Text { get; set; }
        IWorkItemController WorkItemController { get; }
        IWorkItemPresenter WorkItemPresenter { get; }
        IEnumerable<IPresenter> Presenters { get; }
        State<IWorkItem> State { get; }
        Image Image { get; set; }
        IWorkItem AssignWorkItemImage(Image image);
        IWorkItemBuilderPresenter AddPresenter<T>() where T : IPresenter;
        IWorkItemBuilderPresenter AddPresenter(IPresenter presenter);
        T GetPresenter<T>() where T : IPresenter;
        T GetView<T>() where T : IView;
        bool PresentationValid();
        IWorkItem AssignInitiator(IWorkItemInitiator initiator);
        IWorkItemInitiator Initiator { get; }
        Message ActiveDefaultMessage { get; }
        Message ActiveCancelMessage { get; }
        bool IsWaiting { get; }
        string ToolTipText { get; }

        void AssignActiveDefaultMessage(Message message);
        void ResetDefaultMessage();

        void AssignActiveCancelMessage(Message message);
        void ResetCancelMessage();
        void ClearActiveDefaultMessage();
        void ClearActiveCancelMessage();
        
        void Waiting();
        void Ready();
    }
}
