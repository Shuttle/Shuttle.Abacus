namespace Abacus.Data
{
    public class ConstraintTypeQueries
    {
        public const string TableName = "ConstraintType";

        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(ConstraintTypeColumns.Name)
                .With(ConstraintTypeColumns.Text)
                .With(ConstraintTypeColumns.EnabledForRestrictedAnswers)
                .From(TableName);
        }
    }
}
