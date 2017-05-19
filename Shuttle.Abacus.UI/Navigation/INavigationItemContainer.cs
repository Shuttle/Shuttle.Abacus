using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Navigation
{
    public interface INavigationItemContainer<T> where T : class
    {
        T AddNavigationItem(INavigationItem navigationItem);
        IEnumerable<INavigationItem> NavigationItems { get; }
    }
}
