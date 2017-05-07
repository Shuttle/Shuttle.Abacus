namespace Shuttle.Abacus.Domain
{
    public abstract class AbstractCalculationResult : ICalculationResult
    {
        protected AbstractCalculationResult(Formula formula)
        {
            Formula = formula;
        }

        public Formula Formula { get; private set; }

        public string Name
        {
            get { return Formula.Name; }
        }

        public decimal Value { get; protected set; }

        public abstract string Description();

        public abstract void Add(ICalculationResult result);

        public abstract void Limit(decimal value);
    }
}
