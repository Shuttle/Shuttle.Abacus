namespace Shuttle.Abacus.Domain
{
    public interface ICalculationFactory : IFactory
    {
        Calculation Create(string name, bool required);
    }
}
