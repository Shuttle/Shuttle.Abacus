namespace Shuttle.Abacus.Domain
{
    public interface ICanBeReadOnly<T>
    {
        T AsReadOnly();

        bool ReadOnly { get; }
    }
}
