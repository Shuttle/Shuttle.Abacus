using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class ValueSourceFactoryProvider 
    {
        private readonly Dictionary<string, IValueSourceFactory> _factories =
            new Dictionary<string, IValueSourceFactory>();

        public ValueSourceFactoryProvider(IEnumerable<IValueSourceFactory> factories)
        {
            foreach (var factory in factories)
            {
                _factories.Add(factory.Name.ToLower(), factory);
            }
        }

        public IValueSourceFactory Get(string name)
        {
            if (!_factories.ContainsKey(name.ToLower()))
            {
                throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, name,
                                                             "ValueSourceFactoryProvider"));
            }

            return _factories[name.ToLower()];
        }
    }
}
