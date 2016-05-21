namespace Shuttle.Abacus
{
    public interface ICalculationResult
    {
        string Name { get; }
        decimal Value { get; }
        string Description();
        void Add(ICalculationResult result);
        void Limit(decimal value);
    }
}
