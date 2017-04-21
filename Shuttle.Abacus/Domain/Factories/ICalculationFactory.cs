namespace Shuttle.Abacus
{
    public interface ICalculationFactory : IFactory
    {
        Calculation Create(string name, bool required);
    }
}
