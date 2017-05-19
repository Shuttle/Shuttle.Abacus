using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Shell.Navigation
{
    public interface INavigationMap
    {
        IEnumerable<INavigationItem> Items { get; }

        IEnumerable<INavigationItem> SecuredItems(IPermissionCollection permissions);
    }
}
