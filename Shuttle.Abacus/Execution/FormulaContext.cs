using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class FormulaContext : IDisposable
    {
        private readonly ExecutionContext _executionContext;
        public ConstraintViolation ConstraintViolation { get; private set; }

        public FormulaContext(ExecutionContext executionContext, string formulaName)
        {
            FormulaName = formulaName;
            Guard.AgainstNull(executionContext, "executionContext");
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");

            _executionContext = executionContext;
        }

        public string FormulaName { get; }

        public decimal Result { get; private set; }

        public void Dispose()
        {
            _executionContext.FormulaContextCompleted(this);
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

        public FormulaContext Disqualified(string argumentName, string argumentValue, string comparison, string constraintValue)
        {
            ConstraintViolation = new ConstraintViolation(argumentName, argumentValue, comparison, constraintValue);

            return this;
        }
    }
}