using System;

namespace Abacus.Validation
{
    public interface IRule<T> 
    {
        RuleMessage Message { get; }

        void SetMessageArguments(params string[] args);
        void SetException(Exception ex);

        bool IsBrokenBy(T item);
    }
}
