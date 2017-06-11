using System.Collections.Generic;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus
{
    public interface IExecutionTask
    {
        ExecutionContext Execute(string formulaName, IEnumerable<ArgumentValue> values);
        void Flush();
    }
}