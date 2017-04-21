using System.Collections.Generic;

namespace Abacus.UI
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
