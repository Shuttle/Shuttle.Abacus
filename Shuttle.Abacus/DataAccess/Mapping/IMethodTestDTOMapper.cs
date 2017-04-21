using System.Collections.Generic;
using System.Data;
using Abacus.DTO;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public interface IMethodTestDTOMapper : IMapper<DataTable, IEnumerable<MethodTestDTO>>
    {
    }
}
