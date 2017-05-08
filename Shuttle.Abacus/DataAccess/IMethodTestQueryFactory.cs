using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ITestQueryFactory
    {
        IQuery All();
        IQuery AllForMethod(Guid id);
        IQuery GetArgumentAnswers(Guid id);
        IQuery AllUsingArgument(Guid argumentId);
        IQuery Get(Guid id);
        IQuery Remove(Guid id);
        IQuery Add(Test item);
        IQuery AddArgumentAnswer(Test test, TestArgumentValue argumentValue);
    }
}