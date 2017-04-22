using System.Collections.Generic;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IDataTableRepository<T> 
    {
        IEnumerable<T> FetchAllUsing(IQuery query);
        T FetchItemUsing(IQuery query);
        bool Contains(IQuery query);
    }
}
