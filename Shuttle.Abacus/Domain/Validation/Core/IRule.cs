using System;

namespace Shuttle.Abacus.Domain
{
    public interface IRule<T> 
    {
        RuleMessage Message { get; }

        void SetMessageArguments(params string[] args);
        void SetException(Exception ex);

        bool IsBrokenBy(T item);
    }
}
