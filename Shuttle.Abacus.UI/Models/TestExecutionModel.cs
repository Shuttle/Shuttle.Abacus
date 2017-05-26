using System;

namespace Shuttle.Abacus.Shell.Models
{
    public class TestExecutionModel
    {
        public TestExecutionModel(Guid testId)
        {
            TestId = testId;
        }

        public Guid TestId { get; }
    }
}