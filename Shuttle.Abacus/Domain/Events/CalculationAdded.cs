namespace Shuttle.Abacus.Domain
{
    public class CalculationAdded
    {
        public Method Method { get; private set; }
        public ICalculationOwner Owner { get; private set; }
        public Calculation Calculation { get; private set; }

        public CalculationAdded(Method method, ICalculationOwner owner, Calculation calculation)
        {
            Method = method;
            Owner = owner;
            Calculation = calculation;
        }
    }
}
