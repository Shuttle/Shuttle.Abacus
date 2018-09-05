using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class ExecutionContext
    {
        private readonly List<ExecutionResult> _results = new List<ExecutionResult>();
        private readonly Stack<FormulaContext> _stack = new Stack<FormulaContext>();

        private readonly Dictionary<Guid, string> _values = new Dictionary<Guid, string>();

        public ExecutionContext(IEnumerable<ArgumentValue> values, IContextLogger logger)
        {
            Guard.AgainstNull(values, nameof(values));
            Guard.AgainstNull(logger, nameof(logger));

            Logger = logger;

            foreach (var argumentValue in values)
            {
                _values.Add(argumentValue.Id, argumentValue.Value);

                if (logger.LogLevel == ContextLogLevel.Verbose)
                {
                    logger.LogVerbose($"[inputParameter argument name] {argumentValue.Id} = '{argumentValue.Value}'");
                }
            }
        }

        public bool HasActiveFormulaContext => _stack.Count > 0;

        public FormulaContext RootFormulaContext { get; private set; }

        public Exception Exception { get; private set; }

        public bool HasException => Exception != null;
        public IContextLogger Logger { get; }

        public string GetArgumentValue(Guid id)
        {
            if (!_values.ContainsKey(id))
            {
                throw new InvalidOperationException($"There is no argument value with id '{id}'.");
            }

            var result = _values[id];

            if (HasActiveFormulaContext)
            {
                ActiveFormulaContext().UsedArgumentValue(id, result);
            }

            return result;
        }

        public int Depth()
        {
            return _stack.Count;
        }

        public FormulaContext FormulaContext(string formulaName)
        {
            Guard.AgainstNullOrEmptyString(formulaName, nameof(formulaName));

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

        public void FormulaContextCompleted(FormulaContext formulaContext)
        {
            Guard.AgainstNull(formulaContext, nameof(formulaContext));

            AddResult(formulaContext);

            Logger.LogNormal($"[completed] : {formulaContext.FormulaName} ({formulaContext.TotalMilliseconds} ms)");

            if (_stack.Count > 0)
            {
                _stack.Pop();
            }
        }

        private void AddResult(FormulaContext formulaContext)
        {
            Guard.AgainstNull(formulaContext, nameof(formulaContext));

            Logger.LogNormal($"[result] : {formulaContext.Result} ({formulaContext.FormulaName})");

            AddResult(formulaContext.FormulaName, formulaContext.Result);
        }

        public void AddResult(string formulaName, decimal result)
        {
            Guard.AgainstNullOrEmptyString(formulaName, nameof(formulaName));

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
            Guard.AgainstNullOrEmptyString(formulaName, nameof(formulaName));

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

        public FormulaContext ActiveFormulaContext()
        {
            return _stack.Count == 0 ? null : _stack.Peek();
        }
    }
}