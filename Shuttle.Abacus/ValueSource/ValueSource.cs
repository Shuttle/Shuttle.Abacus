using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
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
            throw new NotImplementedException();
        }

        public IValueSource Add(string name, T value)
        {
            throw new NotImplementedException();
        }
    }
}