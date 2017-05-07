using System;

namespace Shuttle.Abacus.Domain
{
    public interface ICalculationContext : ICalculationDisplay<ICalculationContext>, IDisposable
    {
    }
}
