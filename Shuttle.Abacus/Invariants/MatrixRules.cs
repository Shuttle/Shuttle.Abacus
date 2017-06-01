using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
{
    public class MatrixRules : IMatrixRules
    {
        public IRuleCollection<object> NameRules()
        {
            return Rule.With().Required().MaximumLength(160).Create();
        }
    }
}
