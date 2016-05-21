namespace Shuttle.Abacus
{
    public interface IFormulaOwnerService
    {
        void ProcessCommand(IChangeFormulaOrderCommand command, IFormulaOwner owner);
    }
}
