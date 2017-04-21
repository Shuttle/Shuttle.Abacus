using System;

namespace Shuttle.Abacus.Domain
{
    public interface IFormulaOwner : IOwner
    {
        FormulaCollection Formulas { get; }
        Formula AddFormula(Formula formula);

        void ProcessCommand(ChangeFormulaOrderCommand command, IFormulaOwnerService service);

        void AssignFormulas(FormulaCollection collection);
        FormulaRemoved RemoveFormula(Guid formulaId);
    }
}
