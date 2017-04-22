using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IMethodQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        IEnumerable<MethodDTO> AllDTOs();
    }
}
