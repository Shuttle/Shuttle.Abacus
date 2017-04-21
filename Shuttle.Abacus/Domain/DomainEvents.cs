using System;
using System.Collections.Generic;
using Abacus.Infrastructure;

namespace Abacus.Domain
{
    public static class DomainEvents
    {
        [ThreadStatic] private static List<Delegate> actions;

        public static IDependencyResolver Container { get; set; }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }

            actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (Container != null)
            {
                foreach (var handler in Container.ResolveAssignable<IHandleEvent<T>>())
                {
                    handler.Handle(args);
                }
            }

            if (actions == null)
            {
                return;
            }

            foreach (var action in actions)
            {
                if (action is Action<T>)
                {
                    ((Action<T>) action)(args);
                }
            }
        }
    }
}
