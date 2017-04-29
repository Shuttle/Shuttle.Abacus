using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public interface IOperationFactory : IFactory
    {
        FormulaOperation Create(IValueSource valueSource);
        string Text { get; }
    }
}
