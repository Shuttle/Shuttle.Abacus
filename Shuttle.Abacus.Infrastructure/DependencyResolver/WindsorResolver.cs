using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;

namespace Shuttle.Abacus.Infrastructure
{
    public class WindsorResolver : IDependencyResolver
    {
        private IWindsorContainer _container;

        private bool disposed;

        public WindsorResolver(IWindsorContainer configured)
        {
            _container = configured;

            disposed = false;
        }

        public object Container
        {
            get { return _container; }
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string key)
        {
            return _container.Resolve<T>(key);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.Kernel.ResolveAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return (IEnumerable<object>) _container.Kernel.ResolveAll(type);
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
            return _container.ResolveAll(type).Cast<object>();
        }

        public object Resolve(string key)
        {
            throw new NotImplementedException();
            //return _container.Resolve(key);
        }

        public void AddComponent<I, T>() where T : class
        {
            _container.AddComponent<I, T>();
        }

        public void AddComponent<I, T>(string key) where T : class
        {
            _container.AddComponent<I, T>(key);
        }

        public void AddComponent<I>(string key, Type type)
        {
            _container.AddComponent(key, typeof (I), type);
        }

        public void AddComponent<T>(string key)
        {
            _container.AddComponent<T>(key);
        }

        public void AddComponent<T>()
        {
            _container.AddComponent<T>();
        }

        public void AddComponentInstance<I>(object instance)
        {
            _container.Kernel.AddComponentInstance<I>(instance);
        }

        public void AddComponentInstance<I, T>(T instance)
        {
            _container.Kernel.AddComponentInstance<I>(instance);
        }

        public void AddComponent(string key, Type service, Type type)
        {
            _container.Kernel.AddComponent(key, service, type);
        }

        public bool HasComponent(Type service)
        {
            return _container.Kernel.HasComponent(service);
        }

        public bool HasComponent(string key)
        {
            return _container.Kernel.HasComponent(key);
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
                if (_container != null)
                {
                    _container.Dispose();
                }
            }

            _container = null;
            disposed = true;
        }
    }
}
