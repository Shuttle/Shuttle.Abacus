using System;
using System.Collections;
using System.Collections.Generic;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public interface IMessageBus : ISubscriberProvider
    {
        void Publish(Message message);
        void Publish(Message message, ISubscriberProvider provider);
        IResult Publish(IResult result);
        IResult<TValue> Publish<TValue>(IResult<TValue> result);

        void AddSubscriber(object subscriber);
        void AddSubscribers(IEnumerable<object> enumerable);

        void RemoveSubscriber(object subscriber);

        IEnumerable<Type> MessageTypesHandledByObject(object o);
        IEnumerable<Type> MessageTypesHandledByEnumerable(IEnumerable list);
    }
}
