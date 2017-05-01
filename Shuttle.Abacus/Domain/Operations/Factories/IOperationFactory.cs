using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public interface IOperationFactory : IFactory
    {
        Operation Create(IValueSource valueSource);
        string Text { get; }
    }
}
