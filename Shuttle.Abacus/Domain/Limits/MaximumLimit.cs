namespace Shuttle.Abacus.Domain
{
    public class MaximumLimit : Limit
    {
        public MaximumLimit(string name) : base(name)
        {
        }

        public override string Type
        {
            get { return "Maximum"; }
        }

        public override LimitResultBuilder ApplyTo(ICalculationResult result)
        {
            return new MaximumLimitResultBuilder(calculation, result);
        }

        public override Limit Copy()
        {
            var result = new MaximumLimit(Name);

            Copy(result);

            return result;
        }
    }
}
