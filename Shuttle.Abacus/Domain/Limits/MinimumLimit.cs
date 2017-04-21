namespace Shuttle.Abacus.Domain
{
    public class MinimumLimit : Limit
    {
        public MinimumLimit(string name) : base(name)
        {
        }

        public override string Type
        {
            get { return "Minimum"; }
        }

        public override LimitResultBuilder ApplyTo(ICalculationResult result)
        {
            return new MinimumLimitResultBuilder(calculation, result);
        }

        public override Limit Copy()
        {
            return new MinimumLimit(Name);
        }
    }
}
