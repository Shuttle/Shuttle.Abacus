namespace Shuttle.Abacus.Infrastructure
{
    public interface IPermission : ISpecification<IPermissionCollection>
    {
        string Identifier { get; }
        string Description { get; set; }
    }
}
