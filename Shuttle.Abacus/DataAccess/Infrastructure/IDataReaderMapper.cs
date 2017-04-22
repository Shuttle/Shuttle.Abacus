using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public interface IDataReaderMapper<T> : IMapper<IDataReader, IEnumerable<T>>
    {
        
    }
}
