namespace Abacus.Validation
{
    public interface ILimitRules
    {
        IRuleCollection<object> LimitNameRules();
        IRuleCollection<object> TypeRules();
    }
}
