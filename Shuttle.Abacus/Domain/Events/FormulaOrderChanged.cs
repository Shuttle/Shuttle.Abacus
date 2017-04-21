namespace Shuttle.Abacus
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
