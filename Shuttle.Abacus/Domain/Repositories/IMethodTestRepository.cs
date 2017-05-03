namespace Shuttle.Abacus.Domain
{
    public interface IMethodTestRepository : IRepository<MethodTest>
    {
        void Save(MethodTest item);
    }
}
