namespace Shuttle.Abacus.Domain
{
    public class FormulaRemoved
    {
        public FormulaRemoved(Formula formula)
        {
            Formula = formula;
        }

        public Formula Formula { get; private set; }
    }
}
