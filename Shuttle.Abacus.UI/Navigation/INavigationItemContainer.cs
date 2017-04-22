using System.Collections.Generic;

namespace Shuttle.Abacus.UI.Navigation
{
    public interface INavigationItemContainer<T> where T : class
    {
        T AddNavigationItem(INavigationItem navigationItem);
        IEnumerable<INavigationItem> NavigationItems { get; }
    }
}
