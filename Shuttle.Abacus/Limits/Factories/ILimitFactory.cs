namespace Shuttle.Abacus
{
    public interface ILimitFactory : IFactory
    {
        Limit Create(string name);
    }
}
