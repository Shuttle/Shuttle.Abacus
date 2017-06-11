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

        private readonly Dictionary<string, string> _values = new Dictionary<string, string>();
        private readonly List<ExecutionResult> _results = new List<ExecutionResult>();

        public ExecutionContext(IEnumerable<ArgumentValue> values, IContextLogger logger)
        {
            Guard.AgainstNull(values, "values");
            Guard.AgainstNull(logger, "logger");

            Logger = logger;

            foreach (var argumentValue in values)
            {
                _values.Add(argumentValue.Name, argumentValue.Value);

                if (logger.LogLevel == ContextLogLevel.Verbose)
                {
                    logger.LogVerbose($"[input argument] {argumentValue.Name} = '{argumentValue.Value}'");
                }
            }
        }

        public string GetArgumentValue(string name)
        {
            if (!_values.ContainsKey(name))
            {
                throw new InvalidOperationException(string.Format("There is no argument value with name '{0}'.", name));
            }

            var result = _values[name];

            if (HasActiveFormulaContext)
            {
                ActiveFormulaContext().UsedArgumentValue(name, result);
            }

            return result;
        }

        public bool HasActiveFormulaContext => _stack.Count > 0;

        public int Depth()
        {
            return _stack.Count;
        }

        public FormulaContext FormulaContext(string formulaName)
        {
            var result = new FormulaContext(this, formulaName);

            if (_stack.Count > 0)
            {
                _stack.Peek().Add(result);
            }
            else
            {
                RootFormulaContext = result;
            }

            _stack.Push(result);

            Logger.LogNormal($"[starting] : {formulaName}");

            return result;
        }

        public FormulaContext RootFormulaContext { get; private set; }

        public Exception Exception { get; private set; }

        public void FormulaContextCompleted(FormulaContext formulaContext)
        {
            Guard.AgainstNull(formulaContext, "formulaContext");

            AddResult(formulaContext);

            Logger.LogNormal($"[completed] : {formulaContext.FormulaName} ({formulaContext.TotalMilliseconds} ms)");

            if (_stack.Count > 0)
            {
                _stack.Pop();
            }
        }

        private void AddResult(FormulaContext formulaContext)
        {
            Guard.AgainstNull(formulaContext, "formulaContext");

            Logger.LogNormal($"[result] : {formulaContext.Result} ({formulaContext.FormulaName})");
            
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
        public IContextLogger Logger { get; }

        public FormulaContext ActiveFormulaContext()
        {
            return _stack.Count == 0 ? null : _stack.Peek();
        }
    }
}