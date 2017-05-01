namespace Shuttle.Abacus.Domain
{
    public interface ICalculationRepository :
        IRepository<Calculation>
    {
        void SaveOrdered(Method method);
        void Add(Method method, ICalculationOwner owner, Calculation entity);
        void Save(Calculation calculation);
        void SaveOwnershipGraph(Method method);
    }
}
