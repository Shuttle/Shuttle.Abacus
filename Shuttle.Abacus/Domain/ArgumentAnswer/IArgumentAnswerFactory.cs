namespace Shuttle.Abacus.Domain
{
    public interface IArgumentAnswerFactory
    {
        ArgumentAnswer Create(string type, string name, string answer);
    }
}