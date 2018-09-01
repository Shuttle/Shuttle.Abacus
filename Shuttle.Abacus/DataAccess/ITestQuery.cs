using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ITestQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        IEnumerable<DataRow> Arguments(Guid id);

        void Register(Guid id, string name, Guid formulaId, string expectedResult, string expectedResultType,
            string comparison);

        void Remove(Guid id);
        void Rename(Guid id, string name);
        void SetArgumentValue(Guid id, Guid argumentId, string value);

        IEnumerable<DataRow> Search(TestSearchSpecification specification);
    }
}