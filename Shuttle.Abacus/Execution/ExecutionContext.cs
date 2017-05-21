using System.Collections.Generic;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionContext : IContext
    {
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();

        public string FormulaName { get; }

        public ExecutionContext(string formulaName, IEnumerable<ArgumentValue> values)
        {
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");

            FormulaName = formulaName;

            if (values != null)
            {
                foreach (var argumentValue in values)
                {
                    _values.Add(argumentValue.Name, argumentValue.Value);
                }
            }
        }

        public ExecutionContext SetValue(string name, string value)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            _values[name] = value;

            return this;
        }

        public string GetValue(string name)
        {
            Guard.AgainstNull(name, "name");

            return !_values.ContainsKey(name) ? string.Empty : _values[name];
        }
    }
}