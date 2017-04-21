namespace Shuttle.Abacus.Domain
{
    public class FormulaOrderChanged
    {
        public FormulaOrderChanged(IFormulaOwner owner)
        {
            Owner = owner;
        }

        public IFormulaOwner Owner { get; private set; }
    }
}
