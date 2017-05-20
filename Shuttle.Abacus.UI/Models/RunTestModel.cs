using System;
using System.Collections.Specialized;

namespace Shuttle.Abacus.Shell.Models
{
    public class RunTestModel
    {
        public Guid TestId { get; }
        public string FormulaName { get; }
        public NameValueCollection Arguments { get; }

        public RunTestModel(Guid testId, string formulaName, NameValueCollection arguments)
        {
            TestId = testId;
            FormulaName = formulaName;
            Arguments = arguments ?? new NameValueCollection();
        }
    }
}