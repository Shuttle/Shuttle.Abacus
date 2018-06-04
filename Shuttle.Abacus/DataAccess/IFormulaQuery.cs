using System;
using System.Collections.Generic;
using System.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class FormulaSearchSpecification
    {
        public string Name { get; private set; }

        public FormulaSearchSpecification WithName(string name)
        {
            Name = name;

            return this;
        }
    }

    public interface IFormulaQuery
    {
        IEnumerable<DataRow> Operations(Guid formulaId);
        DataRow Get(Guid id);
        IEnumerable<DataRow> Search(FormulaSearchSpecification specification);

        void Registered(Guid formulaId, string name);
        void Remove(Guid formulaId);
        void Rename(Guid formulaId, string name);
        void RemoveOperations(Guid formulaId);

        void AddOperation(Guid formulaId, int sequenceNumber, string operation, string valueSource,
            string valueSelection);

        void RemoveConstraints(Guid formulaId);
        void AddConstraint(Guid formulaId, int sequenceNumber, string argumentName, string comparison, string value);
        IEnumerable<DataRow> Constraints(Guid formulaId);
        
    }
}