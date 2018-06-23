using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class FormulaContext : IDisposable
    {
        private readonly List<FormulaContext> _containedFormulaContexts = new List<FormulaContext>();

        private readonly ExecutionContext _executionContext;
        private readonly List<ArgumentValue> _usedArgumentValues = new List<ArgumentValue>();

        public FormulaContext(ExecutionContext executionContext, string formulaName)
        {
            FormulaName = formulaName;
            Guard.AgainstNull(executionContext, nameof(executionContext));
            Guard.AgainstNullOrEmptyString(formulaName, nameof(formulaName));

            _executionContext = executionContext;

            DateStarted = DateTime.Now;
        }

        public ConstraintViolation ConstraintViolation { get; private set; }

        public DateTime DateStarted { get; }
        public DateTime DateCompleted { get; private set; }

        public string FormulaName { get; }
        public decimal Result { get; private set; }

        public double TotalMilliseconds => (DateCompleted - DateStarted).TotalMilliseconds;

        public void Dispose()
        {
            DateCompleted = DateTime.Now;

            _executionContext.FormulaContextCompleted(this);
        }

        public IEnumerable<FormulaContext> ContainedFormulaContexts()
        {
            return new ReadOnlyCollection<FormulaContext>(_containedFormulaContexts);
        }

        public IEnumerable<ArgumentValue> UsedArgumentValues()
        {
            return new ReadOnlyCollection<ArgumentValue>(_usedArgumentValues);
        }

        public decimal ZeroResult()
        {
            Result = 0;
            return 0;
        }

        public void SetResult(decimal result)
        {
            Result = result;
        }

        public FormulaContext Disqualified(string argumentName, string argumentValue, string comparison,
            string constraintValue)
        {
            if (_executionContext.Logger.IsNormalEnabled)
            {
                _executionContext.Logger.LogNormal(
                    $"[disqualified] : {argumentName} {comparison} {constraintValue} but was {argumentValue}");
            }

            ConstraintViolation = new ConstraintViolation(argumentName, argumentValue, comparison, constraintValue);

            return this;
        }

        public void Add(FormulaContext formulaContext)
        {
            Guard.AgainstNull(formulaContext, nameof(formulaContext));

            _containedFormulaContexts.Add(formulaContext);
        }

        public void UsedArgumentValue(string name, string value)
        {
            _usedArgumentValues.Add(new ArgumentValue(name, value));
        }
    }
}