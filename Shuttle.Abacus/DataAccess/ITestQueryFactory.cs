using System;
using Shuttle.Abacus.Domain;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ITestQueryFactory
    {
        IQuery All();
        IQuery ArgumentValues(Guid id);
        IQuery Get(Guid id);
        IQuery Remove(Guid id);
        IQuery AddArgumentAnswer(Test test, TestArgumentValue argumentValue);
        IQuery Register(Guid id, string name, string formulaName, string expectedResult, string expectedResultType, string comparison);
        IQuery RemoveArgumentValues(Guid id);
        IQuery Rename(Guid id, string name);
    }
}