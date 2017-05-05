using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IOperationTypeQuery
    {
        IEnumerable<DataRow> All();
    }
}