using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Infrastructure
{
    public class FactoryProvider<T> : IFactoryProvider<T> where T : IFactory
    {
        private readonly Dictionary<string, T> factories = new Dictionary<string, T>();

        public FactoryProvider()
        {
            DependencyResolver.Resolver.ResolveAssignable<T>().ForEach(factory => factories.Add(factory.Name.ToLower(), factory));
        }

        public T Get(string name)
        {
            if (!factories.ContainsKey(name.ToLower()))
            {
                throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, name, typeof(FactoryProvider<T>).Name));
            }

            return factories[name.ToLower()];
        }

        public IEnumerable<T> Factories
        {
            get
            {
                var result = new List<T>();

                factories.ForEach(entry => result.Add(entry.Value));
                
                return new ReadOnlyCollection<T>(result);
            }
        }
    }
}
