namespace Shuttle.Abacus
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Create();
        IUnitOfWork Current { get; set; }
    }
}
