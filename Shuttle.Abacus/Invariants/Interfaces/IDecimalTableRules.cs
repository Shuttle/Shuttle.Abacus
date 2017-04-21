namespace Abacus.Validation
{
    public interface IDecimalTableRules
    {
        IRuleCollection<object> DecimalTableNameRules();
        IRuleCollection<object> RowArgumentRules();
    }
}
