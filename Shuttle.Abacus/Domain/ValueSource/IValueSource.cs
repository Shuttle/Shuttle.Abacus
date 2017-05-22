namespace Shuttle.Abacus.Domain
{
    public interface IValueSource
    {
        string Name { get; }
        string Value(string name);
    }
}