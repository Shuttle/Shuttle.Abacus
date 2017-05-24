using System;
using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionContext
    {
        private int _depth = -1;

        private readonly Dictionary<string, Argument> _arguments = new Dictionary<string, Argument>();
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();

        public ExecutionContext(IEnumerable<Argument> arguments, IEnumerable<ArgumentValue> values)
        {
            Guard.AgainstNull(arguments, "arguments");
            Guard.AgainstNull(values, "values");

            foreach (var argument in arguments)
            {
                _arguments.Add(argument.Name, argument);
            }

            foreach (var argumentValue in values)
            {
                _values.Add(argumentValue.Name, argumentValue.Value);
            }
        }

        public string GetArgumentValue(string name)
        {
            if (!_values.ContainsKey(name))
            {
                throw new InvalidOperationException(string.Format("There is no argument value with name '{0}'.", name));
            }

            return _values[name];
        }

        public FormulaContext FormulaContext(string formulaName)
        {
            _depth += 1;

            return new FormulaContext(this, formulaName);
        }

        public void Exception(Exception exception)
        {
            throw new NotImplementedException();
        }

        public Argument GetArgument(string name)
        {
            return _arguments[name];
        }

        public void FormulaContextCompleted()
        {
            _depth -= 1;
        }
    }
}