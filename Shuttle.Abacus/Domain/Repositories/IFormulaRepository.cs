namespace Shuttle.Abacus.Domain
{
    public interface IFormulaRepository : IRepository<Formula>
    {
        void Save(Formula formula);
    }
}