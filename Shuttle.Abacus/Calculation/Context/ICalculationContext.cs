using System;

namespace Shuttle.Abacus
{
    public interface ICalculationContext : ICalculationDisplay<ICalculationContext>, IDisposable
    {
    }
}
