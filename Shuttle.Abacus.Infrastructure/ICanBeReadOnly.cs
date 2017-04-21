namespace Shuttle.Abacus.Infrastructure
{
    public interface ICanBeReadOnly<T>
    {
        T AsReadOnly();

        bool ReadOnly { get; }
    }
}
