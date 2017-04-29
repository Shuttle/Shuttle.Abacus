using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public interface IArgumentAnswerFactory : IFactory
    {
        string Text { get; }

        ArgumentAnswer Create(string name, string answer);
    }
}