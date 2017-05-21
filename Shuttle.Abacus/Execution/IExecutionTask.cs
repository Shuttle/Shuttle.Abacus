using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IExecutionTask
    {
        ExecutionResult Execute(string formulaName, IEnumerable<ArgumentValue> values);
        void Flush();
    }
}