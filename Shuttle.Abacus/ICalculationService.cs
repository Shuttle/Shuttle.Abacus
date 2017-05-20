using System.Collections.Specialized;

namespace Shuttle.Abacus
{
    public interface ICalculationService
    {
        ExecutionResult Execute(string formulaName, NameValueCollection arguments);
    }
}