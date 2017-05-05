using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IValueSourceTypeQuery
    {
        IEnumerable<DataRow> All();
    }
}