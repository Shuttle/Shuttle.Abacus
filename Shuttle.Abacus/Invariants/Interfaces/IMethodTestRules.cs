using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface IMethodTestRules
    {
        IRuleCollection<object> DescriptionRules();
        IRuleCollection<object> ExpectedResultRules();
    }
}
