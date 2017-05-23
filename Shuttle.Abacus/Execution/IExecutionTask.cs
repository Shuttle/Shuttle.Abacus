using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IExecutionTask
    {
        ExecutionContext Execute(string formulaName, IEnumerable<ArgumentValue> values);
        void Flush();
    }
}