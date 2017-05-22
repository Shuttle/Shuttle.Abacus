using System.Collections.Generic;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class ValueSource<T> : IValueSource
    {
        private readonly Dictionary<string, T> _values = new Dictionary<string, T>();

        public ValueSource(string name)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            Name = name;
        }

        public string Name { get; }

        public string Value(string name)
        {
            throw new System.NotImplementedException();
        }

        public IValueSource Add(string name, T value)
        {
            throw new System.NotImplementedException();
        }
    }
}