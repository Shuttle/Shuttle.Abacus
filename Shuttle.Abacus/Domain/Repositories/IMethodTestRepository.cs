using System;

namespace Shuttle.Abacus
{
    public interface IMethodTestRepository : IRepository<MethodTest>
    {
        void Save(MethodTest item);
        void SetArgumentName(Guid argumentId, string argumentName);
        void SetArgumentAnswerType(Guid argumentId, string answerType);
    }
}
