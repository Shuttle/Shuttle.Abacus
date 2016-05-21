using System.Collections.Generic;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
{
    public class ValueSourceFactoryProvider 
    {
        private readonly Dictionary<string, IValueSourceFactory> factories =
            new Dictionary<string, IValueSourceFactory>();

        public ValueSourceFactoryProvider()
        {
            //TODO: DependencyResolver.Resolver.ResolveAll<IValueSourceFactory>().ForEach(factory => factories.Add(factory.Name.ToLower(), factory));
        }

        public IValueSourceFactory Get(string name)
        {
            if (!factories.ContainsKey(name.ToLower()))
            {
                throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, name,
                                                             "ValueSourceFactoryProvider"));
            }

            return factories[name.ToLower()];
        }
    }
}
