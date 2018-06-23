using System;
using System.Collections.Generic;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ArgumentValueProvider
    {
        private readonly Dictionary<string, string> _arguments = new Dictionary<string, string>();

        public ArgumentValueProvider Add(string name, string value)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            _arguments.Add(name, value);

            return this;
        }

        public string GetValue(string name)
        {
            if (!_arguments.ContainsKey(name))
            {
                throw new InvalidOperationException($"Could not find an argument with name '{name}'.");
            }

            return _arguments[name];
        }
    }
}