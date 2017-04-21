using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess.Mapping
{
    public interface IMethodTestDTOMapper : IMapper<DataTable, IEnumerable<MethodTestDTO>>
    {
    }
}
