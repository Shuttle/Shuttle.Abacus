using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMethodTestQueryFactory
    {
        IQuery All();
        IQuery AllForMethod(Guid id);
        IQuery GetArgumentAnswers(Guid id);
        IQuery AllUsingArgument(Guid argumentId);
    }
}