using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public interface IFactoryProvider<T> where T : IFactory
    {
        T Get(string name);

        IEnumerable<T> Factories { get; }
    }
}
