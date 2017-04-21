namespace Shuttle.Abacus.Domain
{
    public interface IMethodRepository : IRepository<Method>
    {
        void Save(Method method);
        Method Get(string methodName);
    }
}
