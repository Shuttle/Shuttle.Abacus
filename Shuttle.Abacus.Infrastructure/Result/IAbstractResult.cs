using System.Collections.Generic;

namespace Shuttle.Abacus.Infrastructure
{
    public interface IAbstractResult
    {
        List<ResultMessage> SuccessMessages { get; }
        List<ResultMessage> FailureMessages { get; }

        bool HasMessages { get; }
        bool HasFailureMessages { get; }
        bool HasSuccessMessages { get; }

        bool OK { get; }
        bool Aborted { get; }
        void SetAbort();

        void Merge(IAbstractResult result);
    }
}
