using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public interface IDependencyResolver : IDisposable
    {
        object Container { get; }

        T Resolve<T>();
        T Resolve<T>(string key);
        object Resolve(string key);
        IEnumerable<T> ResolveAll<T>();
        IEnumerable<object> ResolveAll(Type type);
        IEnumerable<T> ResolveAssignable<T>();
        IEnumerable<object> ResolveAssignable(Type type);
    }
}
