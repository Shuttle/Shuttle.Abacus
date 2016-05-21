namespace Shuttle.Abacus
{
    public abstract class AbstractCalculationResult : ICalculationResult
    {
        protected AbstractCalculationResult(Calculation calculation)
        {
            Calculation = calculation;
        }

        public Calculation Calculation { get; private set; }

        public string Name
        {
            get { return Calculation.Name; }
        }

        public decimal Value { get; protected set; }

        public abstract string Description();

        public abstract void Add(ICalculationResult result);

        public abstract void Limit(decimal value);
    }
}
