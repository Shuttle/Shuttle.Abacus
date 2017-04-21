namespace Shuttle.Abacus.Domain
{
    public interface ISystemUserRules
    {
        IRuleCollection<object> LoginNameRules();
    }
}
