using System;
using System.Collections.Generic;
using System.Data;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Recall;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQuery
    {
        IEnumerable<DataRow> Operations(Guid formulaId);
        DataRow Get(Guid id);
        IEnumerable<DataRow> All();

        void Registered(PrimitiveEvent primitiveEvent, Registered registered);
        void Removed(PrimitiveEvent primitiveEvent, Removed removed);
    }
}