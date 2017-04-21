namespace Shuttle.Abacus.Domain
{
    public interface ICalculationLoggerFactory
    {
        IPremiumCalculationLogger Create();
    }
}
