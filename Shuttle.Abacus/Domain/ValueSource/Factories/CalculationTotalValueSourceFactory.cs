namespace Shuttle.Abacus.Domain
{
    public class CalculationTotalValueSourceFactory : IValueSourceFactory
    {
        public string Name => "CalculationTotal";

        public string Text => "Calculation Total";

        public string Type => "Derived";

        public IValueSource Create(string value)
        {
            return new CalculationTotalValueSource();
        }
    }
}
