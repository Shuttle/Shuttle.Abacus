namespace Shuttle.Abacus.Domain
{
    public interface ILimitFactory : IFactory
    {
        Limit Create(string name);
    }
}
