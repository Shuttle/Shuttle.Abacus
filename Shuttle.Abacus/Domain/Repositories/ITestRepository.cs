namespace Shuttle.Abacus.Domain
{
    public interface ITestRepository : IRepository<Test>
    {
        void Save(Test item);
    }
}
