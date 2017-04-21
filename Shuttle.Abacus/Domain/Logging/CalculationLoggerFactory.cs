namespace Shuttle.Abacus.Domain
{
    public class CalculationLoggerFactory : ICalculationLoggerFactory
    {
        public IPremiumCalculationLogger Create()
        {
            return new TextPremiumCalculationLogger();
        }
    }
}
