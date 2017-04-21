namespace Shuttle.Abacus
{
    public interface IMethodRepository : IRepository<Method>
    {
        void Save(Method method);
        Method Get(string methodName);
    }
}
