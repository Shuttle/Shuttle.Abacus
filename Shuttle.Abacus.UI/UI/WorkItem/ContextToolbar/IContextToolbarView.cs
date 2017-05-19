using System.Collections.Generic;
using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Navigation;

namespace Shuttle.Abacus.Shell.UI.WorkItem.ContextToolbar
{
    public interface IContextToolbarView : IView
    {
        void AddPresenter(IPresenter presenter);
        void SelectPresenter(IPresenter presenter);
        void ShowNavigationItems(IEnumerable<INavigationItem> navigationItems);
        bool HasChanges { get; }
        IPresenter SelectedPresenter { get; }
        bool HasSelectedPresenter { get; }
        void ResetChanges();
    }
}
