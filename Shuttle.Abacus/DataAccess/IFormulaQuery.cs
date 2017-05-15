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

        void Registered(Guid formulaId, string name);
        void Remove(Guid formulaId);
        void Rename(Guid formulaId, string name);
        void RemoveOperations(Guid formulaId);
        void AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource, string valueSelection);
        IEnumerable<DataRow> Constraints(Guid formulaId);
    }
}