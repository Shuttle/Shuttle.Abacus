using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class FormulaContext : IDisposable
    {
        private readonly ExecutionContext _executionContext;
        private readonly string _formulaName;

        public FormulaContext(ExecutionContext executionContext, string formulaName)
        {
            Guard.AgainstNull(executionContext, "executionContext");
            Guard.AgainstNullOrEmptyString(formulaName, "formulaName");

            _executionContext = executionContext;
            _formulaName = formulaName;
        }

        public decimal Result { get; set; }

        public void Dispose()
        {
            _executionContext.FormulaContextCompleted();
        }
    }
}