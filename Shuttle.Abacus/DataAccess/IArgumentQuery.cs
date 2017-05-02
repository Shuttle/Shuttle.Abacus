using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.DataAccess
{
    public interface IArgumentQuery
    {
        IEnumerable<DataRow> All();
        DataRow Get(Guid id);
        IEnumerable<DataRow> Answers(Guid id);
    }
}
