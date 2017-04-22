using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMethodTestDTOMapper : IMapper<DataTable, IEnumerable<MethodTestDTO>>
    {
    }
}
