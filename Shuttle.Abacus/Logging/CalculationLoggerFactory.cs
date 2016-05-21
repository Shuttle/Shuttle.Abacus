namespace Shuttle.Abacus
{
    public class CalculationLoggerFactory : ICalculationLoggerFactory
    {
        public IPremiumCalculationLogger Create()
        {
            return new TextPremiumCalculationLogger();
        }
    }
}
