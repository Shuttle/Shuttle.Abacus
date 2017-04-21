namespace Shuttle.Abacus.Domain
{
    public interface IDecimalTableRules
    {
        IRuleCollection<object> DecimalTableNameRules();
        IRuleCollection<object> RowArgumentRules();
    }
}
