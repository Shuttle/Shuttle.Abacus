namespace Shuttle.Abacus.Domain
{
    public interface ILimitRules
    {
        IRuleCollection<object> LimitNameRules();
        IRuleCollection<object> TypeRules();
    }
}
