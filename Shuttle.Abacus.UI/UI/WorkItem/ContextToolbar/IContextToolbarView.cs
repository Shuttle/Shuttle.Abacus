using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Navigation;

namespace Shuttle.Abacus.UI.UI.WorkItem.ContextToolbar
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
