using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IMethodContextRegister
    {
        IEnumerable<ICalculationResult> Results { get; }
        IEnumerable<ICalculationResult> SubTotals { get; }
        ICalculationResult Total { get; }
        ICalculationResult GetResult(string name);
        bool HasResult(string name);
        SubTotalCalculationResult GetSubTotal(string name);
        void AddResult(ICalculationResult calculationResult);
        void IncrementSubTotal(ICalculationResult calculationResult);
    }
}
