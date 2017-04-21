namespace Shuttle.Abacus
{
    public interface ICanBeReadOnly<T>
    {
        T AsReadOnly();

        bool ReadOnly { get; }
    }
}
