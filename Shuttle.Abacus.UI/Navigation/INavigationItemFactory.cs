using System;
using System.Collections.Generic;
using Shuttle.Abacus.Localisation;
using Shuttle.Abacus.UI.Core.Messaging;

namespace Shuttle.Abacus.UI.Navigation
{
    public interface INavigationItemFactory
    {
        INavigationItemFactory RegisterResourceItem<T>() where T : Message;
        INavigationItemFactory RegisterResourceItem<T>(ResourceItem resourceItem) where T : Message;

        INavigationItem Create<T>() where T : Message, new();
        INavigationItem Create<T>(T message) where T : Message;
        IEnumerable<INavigationItem> Create(IEnumerable<Message> messages);

        bool ContainsMessageType<T>() where T : Message;
        bool ContainsMessageType(Type type);
    }
}
