using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Invariants.Interfaces
{
    public interface IArgumentRules
    {
        IRuleCollection<object> ArgumentNameRules();
        IRuleCollection<object> AnswerTypeRules();
    }
}
