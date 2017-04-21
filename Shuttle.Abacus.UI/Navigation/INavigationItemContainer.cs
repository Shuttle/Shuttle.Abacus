using System.Collections.Generic;

namespace Abacus.UI
{
    public interface INavigationItemContainer<T> where T : class
    {
        T AddNavigationItem(INavigationItem navigationItem);
        IEnumerable<INavigationItem> NavigationItems { get; }
    }
}
