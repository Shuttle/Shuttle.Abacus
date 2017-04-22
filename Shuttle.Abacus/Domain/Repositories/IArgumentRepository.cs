namespace Shuttle.Abacus.Domain
{
    public interface IArgumentRepository : IRepository<Argument>
    {
        ArgumentCollection All();
        void Save(Argument argument);
    }
}
