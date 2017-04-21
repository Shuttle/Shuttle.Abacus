namespace Abacus.Data
{
    public class OperationTypeQueries
    {
        public const string TableName = "OperationType";

        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(OperationTypeColumns.Name)
                .With(OperationTypeColumns.Text)
                .From(TableName);
        }
    }
}
