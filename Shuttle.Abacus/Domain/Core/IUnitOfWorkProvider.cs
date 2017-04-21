namespace Shuttle.Abacus.Domain
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Create();
        IUnitOfWork Current { get; set; }
    }
}
