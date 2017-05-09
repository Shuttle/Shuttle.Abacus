namespace Shuttle.Abacus.Domain
{
    public interface IMatrixRepository : IRepository<Matrix>
    {
        void Save(Matrix matrix);
    }
}
