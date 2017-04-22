using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public interface IDataTableMapper<T> : IMapper<DataTable, IEnumerable<T>>
    {
        
    }
}
