using System;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ITestQueryFactory
    {
        IQuery All();
        IQuery Arguments(Guid id);
        IQuery Get(Guid id);
        IQuery Remove(Guid id);

        IQuery Register(Guid id, string name, Guid formulaId, string expectedResult,
            string expectedResultDataTypeName,
            string comparison);

        IQuery RemoveArgumentValues(Guid id);
        IQuery Rename(Guid id, string name);
        IQuery RemoveArgumentValue(Guid id, Guid argumentId);
        IQuery AddArgumentValue(Guid id, Guid argumentId, string value);
        IQuery Search(TestSearchSpecification specification);
    }
}