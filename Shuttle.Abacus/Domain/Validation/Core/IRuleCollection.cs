using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IRuleCollection<T>
    {
        int Count { get; }

        IList<RuleMessage> Messages { get; }
        bool IsEmpty { get; }
        IRuleCollection<T> BrokenBy(T item);
        
        IResult ToResult();
        
        void AssignTo(IList<IRule<T>> list);
        void Enforce(T item);
    }
}
