namespace Abacus.Data
{
    public class AnswerTypeQueries
    {
        public const string TableName = "AnswerType";
        
        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(AnswerTypeColumns.Name)
                .With(AnswerTypeColumns.Text)
                .From(TableName);
        }
    }
}
