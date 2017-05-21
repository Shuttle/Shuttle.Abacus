using System.Collections.Specialized;

namespace Shuttle.Abacus
{
    public interface IExecutionTask
    {
        ExecutionResult Execute(string formulaName, NameValueCollection arguments);
        void Flush();
    }
}