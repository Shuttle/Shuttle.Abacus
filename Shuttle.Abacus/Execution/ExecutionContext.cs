using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class ExecutionContext
    {
        private readonly Stack<FormulaContext> _stack = new Stack<FormulaContext>();

        private readonly Dictionary<string, Argument> _arguments = new Dictionary<string, Argument>();
        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();
        private readonly List<ExecutionResult> _results = new List<ExecutionResult>();

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

        public int Depth()
        {
            return _stack.Count;
        }

        public FormulaContext FormulaContext(string formulaName)
        {
            var result = new FormulaContext(this, formulaName);

            _stack.Push(result);

            return result;
        }

        public Exception Exception { get; private set; }

        public Argument GetArgument(string name)
        {
            return _arguments[name];
        }

        public void FormulaContextCompleted(FormulaContext formulaContext)
        {
            Guard.AgainstNull(formulaContext, "formulaContext");

            AddResult(formulaContext);

            _stack.Pop();
        }

        private void AddResult(FormulaContext formulaContext)
        {
            Guard.AgainstNull(formulaContext, "formulaContext");

            AddResult(formulaContext.FormulaName, formulaContext.Result);
        }

        public void AddResult(string formulaName, decimal result)
        {
            _results.Add(new ExecutionResult(formulaName, result, Depth()));
        }

        public ExecutionResult RootResult()
        {
            return _results.FirstOrDefault();
        }

        public decimal Result()
        {
            return RootResult()?.Value ?? 0;
        }

        public void CyclicInvariant(string formulaName)
        {
            var cyclic = false;

            foreach (var context in _stack)
            {
                if (context.FormulaName.Equals(formulaName, StringComparison.InvariantCultureIgnoreCase))
                {
                    cyclic = true;
                    break;
                }
            }

            if (!cyclic)
            {
                return;
            }

            var stack = new StringBuilder($"{formulaName}");

            foreach (var context in _stack)
            {
                stack.Append($" <- {context.FormulaName}");
            }

            throw new InvalidOperationException($"Cyclic formula usage: {stack}");
        }

        public ExecutionContext WithException(Exception exception)
        {
            Exception = exception;

            return this;
        }

        public bool HasException => Exception != null;
    }
}