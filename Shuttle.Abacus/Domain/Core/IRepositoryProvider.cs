namespace Shuttle.Abacus.Domain
{
    public interface IRepositoryProvider
    {
        IRepository Get(string name);
        IRepository Get<T>();
    }
}
