namespace Shuttle.Abacus.Domain
{
    public class CalculationLoggerFactory : ICalculationLoggerFactory
    {
        public ICalculationLogger Create()
        {
            return new DefaultCalculationLogger();
        }
    }
}
