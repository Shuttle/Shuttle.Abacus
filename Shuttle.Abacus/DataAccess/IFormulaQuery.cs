using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface IFormulaQuery
    {
        IEnumerable<DataRow> Operations(Guid formulaId);
        DataRow Get(Guid id);
        IEnumerable<DataRow> Search(FormulaSearchSpecification specification);
        void Registered(Guid formulaId, string name);
        void Remove(Guid formulaId);
        void Rename(Guid formulaId, string name);
        void RemoveOperation(Guid operationId);
        void AddOperation(Guid operationId, Guid formulaId, int sequenceNumber, string operation, string valueProviderName, string inputParameter);
        void RemoveConstraint(Guid constraintId);
        void AddConstraint(Guid constraintId, Guid formulaId, Guid argumentId, string comparison, string value);
        IEnumerable<DataRow> Constraints(Guid formulaId);
        
    }
}