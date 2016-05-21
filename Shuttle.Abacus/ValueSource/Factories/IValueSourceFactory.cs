namespace Shuttle.Abacus
{
    public interface IValueSourceFactory : IFactory
    {
        string Text { get; }
        string Type { get; }

        IValueSource Create(string value);
    }
}
