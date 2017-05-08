using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ITestQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        IEnumerable<DataRow> FetchForMethodId(Guid methodId);
        IEnumerable<DataRow> GetArgumentAnswers(Guid id);
        IEnumerable<DataRow> AllUsingArgument(Guid argumentId);
    }
}
