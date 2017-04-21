using System;

namespace Shuttle.Abacus.Domain
{
    public interface IMethodTestRepository : IRepository<MethodTest>
    {
        void Save(MethodTest item);
        void SetArgumentName(Guid argumentId, string argumentName);
        void SetArgumentAnswerType(Guid argumentId, string answerType);
    }
}
