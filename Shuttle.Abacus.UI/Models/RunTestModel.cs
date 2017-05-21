using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Models
{
    public class RunTestModel
    {
        public RunTestModel(Guid testId, string formulaName, IEnumerable<ArgumentValue> arguments)
        {
            TestId = testId;
            FormulaName = formulaName;
            Arguments = arguments ?? new List<ArgumentValue>();
        }

        public Guid TestId { get; }
        public string FormulaName { get; }
        public IEnumerable<ArgumentValue> Arguments { get; }
    }
}