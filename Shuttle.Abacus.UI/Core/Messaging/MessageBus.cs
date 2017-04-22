using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Core.Messaging
{
    public class MessageBus : IMessageBus
    {
        private readonly IList<object> subscribers;

        public IEnumerable<object> Subscribers
        {
            get { return new ReadOnlyCollection<object>(CopySubscribers()); }
        }

        public IEnumerable<Type> MessageTypesHandledByObject(object o)
        {
            return MessageTypesHandledByEnumerable(new List<object> {o});
        }

        public IEnumerable<Type> MessageTypesHandledByEnumerable(IEnumerable list)
        {
            var result = new List<Type>();

            var generic = typeof (IMessageHandler<>);
            var message = typeof (Message);

            foreach (var item in list)
            {
                foreach (var type in item.GetType().GetInterfaces())
                {
                    if (!type.IsGenericType || type.GetGenericTypeDefinition() != generic)
                    {
                        continue;
                    }

                    var parameter = type.GetGenericArguments()[0];

                    if (message.IsAssignableFrom(parameter))
                    {
                        result.Add(parameter);
                    }
                }
            }

            return result;
        }

        public MessageBus()
        {
            subscribers = new List<object>();
        }

        public void Publish(Message message)
        {
            Publish(message, this);
        }

        public void Publish(Message message, ISubscriberProvider provider)
        {
            Guard.AgainstNull(message, "message");

            PublishMessage(WorkStartedMessage.Instance, provider);
            PublishMessage(message, provider);
            PublishMessage(WorkCompletedMessage.Instance, provider);
        }

        public void AddSubscriber(object subscriber)
        {
            if (!subscribers.Contains(subscriber))
            {
                subscribers.Add(subscriber);
            }
        }

        public void AddSubscribers(IEnumerable<object> enumerable)
        {
            enumerable.ForEach(AddSubscriber);
        }

        public void RemoveSubscriber(object subscriber)
        {
            if (subscribers.Contains(subscriber))
            {
                subscribers.Remove(subscriber);
            }
        }

        private void PublishMessage(Message message, ISubscriberProvider provider)
        {
            PublishMessage(message, provider, false);
        }

        private void PublishMessage(Message message, ISubscriberProvider provider, bool throwOnException)
        {
            try
            {
                var messageType = message.GetType();

                var handlerType = typeof (IMessageHandler<>).MakeGenericType(messageType);

                foreach (var subscriber in provider.Subscribers)
                {
                    var subscriberType = subscriber.GetType();

                    if (!handlerType.IsAssignableFrom(subscriberType))
                    {
                        continue;
                    }

                    var mi = subscriberType.GetMethod("HandleMessage", new[]
                                                                           {
                                                                               messageType
                                                                           });

                    if (mi == null)
                    {
                        continue;
                    }

                    mi.Invoke(subscriber, new[]
                                              {
                                                  message
                                              });
                }
            }
            catch (Exception ex)
            {
                if (throwOnException)
                {
                    throw;
                }

                PublishMessage(new ResultNotificationMessage(new Result().AddException(ex)), this, true);
            }
        }

        private IList<object> CopySubscribers()
        {
            var result = new List<object>();

            foreach (var subscriber in subscribers)
            {
                result.Add(subscriber);
            }

            return result;
        }

        public IResult Publish(IResult result)
        {
            Publish(new ResultNotificationMessage(result));

            return result;
        }

        public IResult<TValue> Publish<TValue>(IResult<TValue> result)
        {
            Publish(new ResultNotificationMessage(result));

            return result;
        }
    }
}
