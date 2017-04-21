using System.Collections.Generic;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public interface INavigationMap
    {
        IEnumerable<INavigationItem> Items { get; }

        IEnumerable<INavigationItem> SecuredItems(IPermissionCollection permissions);
    }
}
