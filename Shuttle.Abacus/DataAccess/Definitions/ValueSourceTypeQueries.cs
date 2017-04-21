namespace Abacus.Data
{
    public class ValueSourceTypeQueries
    {
        public const string TableName = "ValueSourceType";
        
        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(ValueSourceTypeColumns.Name)
                .With(ValueSourceTypeColumns.Text)
                .With(ValueSourceTypeColumns.Type)
                .From(TableName);
        }
    }
}
