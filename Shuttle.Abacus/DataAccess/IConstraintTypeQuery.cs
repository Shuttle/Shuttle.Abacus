using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IConstraintTypeQuery
    {
        IEnumerable<DataRow> All();
    }
}