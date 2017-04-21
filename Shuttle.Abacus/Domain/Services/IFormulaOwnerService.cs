namespace Shuttle.Abacus.Domain
{
    public interface IFormulaOwnerService
    {
        FormulaOrderChanged ProcessCommand(ChangeFormulaOrderCommand command, IFormulaOwner owner);
    }
}
