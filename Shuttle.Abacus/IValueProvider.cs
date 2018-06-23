namespace Shuttle.Abacus
{
    public interface IValueProvider
    {
        string Name { get; }
        string Value(string input);
    }
}