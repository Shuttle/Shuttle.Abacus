using System;
using System.Collections.Generic;
using System.Drawing;
using Abacus.Infrastructure;
using Abacus.Localisation;

namespace Abacus.UI
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

        public string Text
        {
            get { return resourceItem.Text; }
        }

        public Image Image
        {
            get { return resourceItem.Image; }
        }

        public IEnumerable<INavigationItem> Items
        {
            get { return items; }
        }

        public Message Message { get; private set; }

        public bool HasMessage
        {
            get { return Message != null; }
        }

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

        public INavigationItem AssignResourceItem(ResourceItem item)
        {
            Guard.AgainstNull(item, "item");

            resourceItem = item;

            return this;
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
