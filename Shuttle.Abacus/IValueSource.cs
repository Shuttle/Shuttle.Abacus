namespace Shuttle.Abacus
{
    public interface IValueSource
    {
        string Name { get; }
        string Value(string name);
    }
}