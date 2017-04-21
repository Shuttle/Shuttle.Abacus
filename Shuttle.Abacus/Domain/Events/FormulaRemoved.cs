namespace Shuttle.Abacus.Domain
{
    public class FormulaRemoved
    {
        public FormulaRemoved(Formula formula, IFormulaOwner owner)
        {
            Formula = formula;
            Owner = owner;
        }

        public Formula Formula { get; private set; }
        public IFormulaOwner Owner { get; private set; }
    }
}
