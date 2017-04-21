namespace Shuttle.Abacus
{
    public interface IArgumentAnswerFactory : IFactory
    {
        string Text { get; }

        ArgumentAnswer Create(string name, string answer);
    }
}