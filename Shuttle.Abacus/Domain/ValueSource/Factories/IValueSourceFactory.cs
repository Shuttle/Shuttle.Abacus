using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public interface IValueSourceFactory : IFactory
    {
        string Text { get; }
        string Type { get; }

        IValueSource Create(string value);
    }
}
