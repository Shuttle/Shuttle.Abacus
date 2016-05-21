namespace Shuttle.Abacus
{
    public interface IRepositoryProvider
    {
        IRepository Get(string name);
        IRepository Get<T>();
    }
}
