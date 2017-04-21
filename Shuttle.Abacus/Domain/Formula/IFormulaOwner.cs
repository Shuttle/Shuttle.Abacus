using System;

namespace Shuttle.Abacus
{
    public interface IFormulaOwner : IOwner
    {
        FormulaCollection Formulas { get; }
        Formula AddFormula(Formula formula);

        void ProcessCommand(IChangeFormulaOrderCommand command, IFormulaOwnerService service);

        void AssignFormulas(FormulaCollection collection);
        void RemoveFormula(Guid formulaId);
    }
}
