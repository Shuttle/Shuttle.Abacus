namespace Shuttle.Abacus
{
    public interface IArgumentRepository : IRepository<Argument>
    {
        ArgumentCollection All();
    }
}
