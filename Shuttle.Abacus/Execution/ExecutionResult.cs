using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionResult
    {
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();

        public ExecutionResult()
        {
        }

        public ExecutionResult(IEnumerable<ArgumentValue> values)
        {
            Guard.AgainstNull(values, "values");

            foreach (var argumentValue in values)
            {
                _values.Add(argumentValue.Name, argumentValue.Value);
            }
        }

        public static ExecutionResult Empty()
        {
            return new ExecutionResult();
        }

        public string GetArgumentValue(string name)
        {
            if (!_values.ContainsKey(name))
            {
                throw new InvalidOperationException(string.Format("There is no argument value with name '{0}'.", name));
            }

            return _values[name];
        }

        public FormulaContext FormulaContext()
        {
            return new FormulaContext();
        }
    }
}