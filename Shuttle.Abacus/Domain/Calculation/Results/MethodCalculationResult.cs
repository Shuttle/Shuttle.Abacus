namespace Shuttle.Abacus.Domain
{
    public class MethodCalculationResult : ICalculationResult
    {
        public MethodCalculationResult(string title, decimal value)
        {
            Value = value;
            Name = title;
        }

        public string Name { get; private set; }
        public decimal Value { get; private set; }

        public string Description()
        {
            return string.Format("{0}: {1}",
                                 Name,
                                 Value.ToString(Resources.FormatDecimal));
        }

        public void Add(ICalculationResult result)
        {
            Value += result.Value;
        }

        public void Limit(decimal value)
        {
            Value = value;
        }
    }
}
