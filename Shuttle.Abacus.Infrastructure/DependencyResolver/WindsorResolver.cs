using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;

namespace Shuttle.Abacus.Infrastructure
{
    public class WindsorResolver : IDependencyResolver
    {
        private WindsorContainer container;

        private bool disposed;

        public WindsorResolver(WindsorContainer configured)
        {
            container = configured;

            disposed = false;
        }

        public object Container
        {
            get { return container; }
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T Resolve<T>(string key)
        {
            return container.Resolve<T>(key);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return container.Kernel.ResolveAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return (IEnumerable<object>) container.Kernel.ResolveAll(type);
        }

        public IEnumerable<T> ResolveAssignable<T>()
        {
            var result = new List<T>();

            foreach (var implementation in ResolveAssignable(typeof(T)))
            {
                result.Add((T)implementation);
            }

            return result;
        }

        public IEnumerable<object> ResolveAssignable(Type type)
        {
            return container.ResolveAll(type).Cast<object>();
        }

        public object Resolve(string key)
        {
            throw new NotImplementedException();
            //return container.Resolve(key);
        }

        public void AddComponent<I, T>() where T : class
        {
            container.AddComponent<I, T>();
        }

        public void AddComponent<I, T>(string key) where T : class
        {
            container.AddComponent<I, T>(key);
        }

        public void AddComponent<I>(string key, Type type)
        {
            container.AddComponent(key, typeof (I), type);
        }

        public void AddComponent<T>(string key)
        {
            container.AddComponent<T>(key);
        }

        public void AddComponent<T>()
        {
            container.AddComponent<T>();
        }

        public void AddComponentInstance<I>(object instance)
        {
            container.Kernel.AddComponentInstance<I>(instance);
        }

        public void AddComponentInstance<I, T>(T instance)
        {
            container.Kernel.AddComponentInstance<I>(instance);
        }

        public void AddComponent(string key, Type service, Type type)
        {
            container.Kernel.AddComponent(key, service, type);
        }

        public bool HasComponent(Type service)
        {
            return container.Kernel.HasComponent(service);
        }

        public bool HasComponent(string key)
        {
            return container.Kernel.HasComponent(key);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (container != null)
                {
                    container.Dispose();
                }
            }

            container = null;
            disposed = true;
        }
    }
}
