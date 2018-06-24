using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ValueProvider<T> : IValueProvider
    {
        private readonly Dictionary<string, T> _values = new Dictionary<string, T>();

        public ValueProvider(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            Name = name;
        }

        public string Name { get; }

        public string Value(string inputParameter)
        {
            throw new NotImplementedException();
        }

        public IValueProvider Add(string name, T value)
        {
            throw new NotImplementedException();
        }
    }
}