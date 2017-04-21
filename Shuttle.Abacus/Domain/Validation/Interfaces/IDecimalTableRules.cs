namespace Shuttle.Abacus
{
    public interface IDecimalTableRules
    {
        IRuleCollection<object> DecimalTableNameRules();
        IRuleCollection<object> RowArgumentRules();
    }
}
