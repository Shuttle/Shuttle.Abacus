using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMethodTestQueryFactory
    {
        IQuery All();
        IQuery AllForMethod(Guid id);
        IQuery GetArgumentAnswers(Guid id);
        IQuery AllUsingArgument(Guid argumentId);
        IQuery Get(Guid id);
        IQuery Remove(Guid id);
        IQuery Add(MethodTest item);
        IQuery AddArgumentAnswer(MethodTest test, MethodTestArgumentAnswer argumentAnswer);
        IQuery SetArgumentName(Guid argumentId, string argumentName);
        IQuery SetArgumentAnswerType(Guid argumentId, string answerType);
    }
}