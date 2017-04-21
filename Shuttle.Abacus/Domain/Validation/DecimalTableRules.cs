namespace Shuttle.Abacus.Domain
{
    public class DecimalTableRules : IDecimalTableRules
    {
        public IRuleCollection<object> DecimalTableNameRules()
        {
            return Rule.With().Required().MaximumLength(160).Create();
        }

        public IRuleCollection<object> RowArgumentRules()
        {
            return Rule.With().Required().Create();
        }
    }
}
