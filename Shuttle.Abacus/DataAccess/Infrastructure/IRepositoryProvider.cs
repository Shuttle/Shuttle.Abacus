using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess
{
    public interface IRepositoryProvider
    {
        IRepository Get(string name);
        IRepository Get<T>();
    }
}
