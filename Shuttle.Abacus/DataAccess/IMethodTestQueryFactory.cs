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
        IQuery Add(Test item);
        IQuery AddArgumentAnswer(Test test, TestArgumentValue argumentValue);
    }
}