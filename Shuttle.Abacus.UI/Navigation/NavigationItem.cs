using System.Collections.Generic;
using System.Drawing;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.UI.Navigation
{
    public class NavigationItem : INavigationItem
    {
        private ResourceItem resourceItem;

        private readonly List<INavigationItem> items = new List<INavigationItem>();

        public NavigationItem(ResourceItem resourceItem)
        {
            this.resourceItem = resourceItem;

            Message = null;
        }

        public string Text => resourceItem.Text;

        public Image Image => resourceItem.Image;

        public IEnumerable<INavigationItem> Items => items;

        public Message Message { get; private set; }

        public bool HasMessage => Message != null;

        public INavigationItem Add(INavigationItem navigationItem)
        {
            items.Add(navigationItem);

            return this;
        }

        public INavigationItem Add(ResourceItem item)
        {
            items.Add(new NavigationItem(resourceItem));

            return this;
        }

        public bool GraphHasPermittedItems(IPermissionCollection permissions)
        {
            var message = Message;

            if (!HasMessage || HasMessage && message.RequiredPermission == null)
            {
                if (items.Count == 0)
                {
                    return true;
                }
            }

            if (HasMessage && message.RequiredPermission != null)
            {
                return message.RequiredPermission.IsSatisfiedBy(permissions);
            }

            foreach (var item in items)
            {
                if (item.GraphHasPermittedItems(permissions))
                {
                    return true;
                }
            }

            return false;
        }

        public INavigationItem WithResourceItem(ResourceItem item)
        {
            Guard.AgainstNull(item, "item");

            return new NavigationItem(item).AssignMessage(Message);
        }

        public INavigationItem AssignMessage(Message message)
        {
            Message = message;

            return this;
        }

        public INavigationItem Copy()
        {
            return new NavigationItem(resourceItem) { Message = Message };
        }

        public INavigationItem AddRange(IEnumerable<INavigationItem> navigationItems)
        {
            navigationItems.ForEach(item => Add(item));

            return this;
        }
    }
}
