using System.Collections.Generic;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Navigation
{
    public interface INavigationItem : IResourceAccessor
    {
        IEnumerable<INavigationItem> Items { get; }

        Message Message { get; }

        INavigationItem Add(INavigationItem navigationItem);
        INavigationItem Add(ResourceItem resourceItem);
        INavigationItem AddRange(IEnumerable<INavigationItem> navigationItems);

        INavigationItem AssignMessage(Message message);

        INavigationItem Copy();

        bool GraphHasPermittedItems(IPermissionCollection permissions);
        INavigationItem AssignResourceItem(ResourceItem item);
    }
}
