namespace Shuttle.Abacus.Domain
{
    public class CalculationTotalValueSourceFactory : IValueSourceFactory
    {
        public string Name
        {
            get { return "CalculationTotal"; }
        }

        public string Text
        {
            get { return "Calculation Total"; }
        }

        public string Type
        {
            get { return "Derived"; }
        }

        public IValueSource Create(string value)
        {
            return new CalculationTotalValueSource();
        }
    }
}
