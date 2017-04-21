namespace Shuttle.Abacus
{
    public interface ILimitRules
    {
        IRuleCollection<object> LimitNameRules();
        IRuleCollection<object> TypeRules();
    }
}
