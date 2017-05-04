using System;

namespace Shuttle.Abacus.Domain
{
    public interface IFormulaOwner : IOwner
    {
        FormulaCollection Formulas { get; }
        Formula AddFormula(Formula formula);
        void AddFormula(OwnedFormula item);

        void AssignFormulas(FormulaCollection collection);
        FormulaRemoved RemoveFormula(Guid formulaId);
    }
}
