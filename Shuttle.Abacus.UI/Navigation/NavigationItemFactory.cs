using System;
using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Messaging;
using Shuttle.Core.Infrastructure;
using ConventionException = Shuttle.Core.Infrastructure.ConventionException;

namespace Shuttle.Abacus.UI.Navigation
{
    public class NavigationItemFactory : INavigationItemFactory
    {
        private static readonly string[][] Operations =
            new[]
            {
                new[]
                {
                    "New", "New"
                },
                new[]
                {
                    "Create", "New"
                },
                new[]
                {
                    "Edit", "Edit"
                },
                new[]
                {
                    "Update", "Edit"
                },
                new[]
                {
                    "Delete", "Delete"
                },
                new[]
                {
                    "Remove", "Delete"
                },
                new[]
                {
                    "List", string.Empty
                }
            };


        private readonly Dictionary<Type, ResourceItem> items = new Dictionary<Type, ResourceItem>();

        internal NavigationItemFactory()
        {
        }

        public NavigationItemFactory(IMessageResourceItemStore store)
        {
            store.Fill(this);
        }

        public bool ContainsMessageType(Type type)
        {
            return items.ContainsKey(type);
        }

        public INavigationItemFactory RegisterResourceItem<T>() where T : Message
        {
            return RegisterResourceItem<T>(ResourceItemFor<T>());
        }

        public INavigationItemFactory RegisterResourceItem<T>(ResourceItem resourceItem) where T : Message
        {
            if (ContainsMessageType(typeof(T)))
            {
                throw new DuplicateEntryException(string.Format(Resources.DuplicateEntryException, typeof(T).Name, "ResourceItemStore"));
            }

            items.Add(typeof(T), resourceItem);

            return this;
        }

        public INavigationItem Create<T>() where T : Message, new()
        {
            return new NavigationItem(GetResourceItem<T>()).AssignMessage(new T());
        }

        private ResourceItem GetResourceItem<T>()
        {
            var type = typeof(T);

            if (!items.ContainsKey(typeof(T)))
            {
                throw new NavigationItemNotFoundForMessageException(string.Format(Resources.NavigationItemNotFoundForMessage, type.Name));
            }
            return items[typeof(T)];
        }

        public INavigationItem Create<T>(T message) where T : Message
        {
            return new NavigationItem(GetResourceItem<T>()).AssignMessage(message);
        }

        public INavigationItem Create(Type type)
        {
            var result = items[type];

            if (result == null)
            {
                throw new NavigationItemNotFoundForMessageException(
                    string.Format(Resources.NavigationItemNotFoundForMessage, type.Name));
            }

            return new NavigationItem(result);
        }

        public IEnumerable<INavigationItem> Create(IEnumerable<Message> messages)
        {
            var result = new List<INavigationItem>();

            foreach (var message in messages)
            {
                result.Add(Create(message.GetType()));
            }

            return result;
        }

        public bool ContainsMessageType<T>() where T : Message
        {
            return ContainsMessageType(typeof(T));
        }

        private static ResourceItem ResourceItemFor<T>()
        {
            var name = typeof(T).Name;

            var exception = string.Format(Resources.ResourceItemForMessageError, name);

            if (!name.EndsWith("Message"))
            {
                throw new ConventionException(exception);
            }

            name = name.Substring(0, name.Length - 7);

            var operation = string.Empty;
            var identifier = string.Empty;

            foreach (var arr in Operations)
            {
                if (!name.StartsWith(arr[0]))
                {
                    continue;
                }

                operation = arr[0];
                identifier = arr[1];

                break;
            }

            if (string.IsNullOrEmpty(operation))
            {
                throw new ConventionException(exception);
            }

            var entity = name.Substring(operation.Length);

            if (string.IsNullOrEmpty(entity))
            {
                throw new ConventionException(exception);
            }

            return new ResourceItem(operation, entity + identifier);
        }
    }
}
