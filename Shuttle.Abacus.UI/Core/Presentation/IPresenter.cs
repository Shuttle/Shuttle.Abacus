using System;
using System.Collections.Generic;
using System.Drawing;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Abacus.UI.Core.WorkItem;
using Shuttle.Abacus.UI.Navigation;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public interface IPresenter : INavigationItemContainer<IPresenter>, IDisposable
    {
        IWorkItem WorkItem { get; set; }

        IView IView { get; }

        IMessageBus MessageBus { get; set; }
        INavigationItemFactory NavigationItemFactory { get; set; }

        string Text { get; set; }
        Image Image { get; set; }
        bool IsValid { get; }
        bool TrackChanges { get; }

        void ViewReady();
        void ViewAccepted();
        void ViewCancelled();

        void Initialize();

        void PublishReadyStatus();
        void PublishStatus(string message);
        void AssignModel<T>(T dto);

        event PresenterTextChanged TextChanged;
        event PresenterImageChanged ImageChanged;

        void Show();
        
        IEnumerable<INavigationItem> MergedNavigationItems();
    }
}
