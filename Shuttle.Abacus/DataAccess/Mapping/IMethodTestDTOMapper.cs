using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMethodTestDTOMapper : IMapper<DataTable, IEnumerable<MethodTestDTO>>
    {
    }
}
