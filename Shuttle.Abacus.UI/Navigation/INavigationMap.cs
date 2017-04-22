using System.Collections.Generic;

namespace Shuttle.Abacus.UI.Navigation
{
    public interface INavigationMap
    {
        IEnumerable<INavigationItem> Items { get; }

        IEnumerable<INavigationItem> SecuredItems(IPermissionCollection permissions);
    }
}
